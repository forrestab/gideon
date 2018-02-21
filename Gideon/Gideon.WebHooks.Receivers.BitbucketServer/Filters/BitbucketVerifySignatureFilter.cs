using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.AspNetCore.WebHooks.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Filters
{
    public class BitbucketVerifySignatureFilter : WebHookVerifySignatureFilter, IAsyncResourceFilter
    {
        public override string ReceiverName => BitbucketConstants.ReceiverName;

        private static readonly char[] PAIR_SEPARATORS = new[] { '=' };

        public BitbucketVerifySignatureFilter(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
            : base(configuration, hostingEnvironment, loggerFactory)
        { }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            RouteData Data = context.RouteData;
            HttpRequest Request = context.HttpContext.Request;

            if(Data.TryGetWebHookReceiverName(out string ReceiverName) && this.IsApplicable(ReceiverName) && HttpMethods.IsPost(Request.Method))
            {
                // confirm secure connection
                var ErrorResult = this.EnsureSecureConnection(ReceiverName, context.HttpContext.Request);
                if(ErrorResult != null)
                {
                    context.Result = ErrorResult;

                    return;
                }

                // get the expected hash from signature header
                var Header = this.GetRequestHeader(Request, BitbucketConstants.SignatureHeaderName, out ErrorResult);
                if(ErrorResult != null)
                {
                    context.Result = ErrorResult;

                    return;
                }

                var Values = new TrimmingTokenizer(Header, PAIR_SEPARATORS);
                var Enumerator = Values.GetEnumerator();

                Enumerator.MoveNext();

                var HeaderKey = Enumerator.Current;
                if(Values.Count != 2 || !StringSegment.Equals(HeaderKey, BitbucketConstants.SignatureHeaderKey, StringComparison.OrdinalIgnoreCase))
                {
                    // TODO, log
                    // TODO, build message
                    return;
                }

                Enumerator.MoveNext();
                var HeaderValue = Enumerator.Current.Value;

                var ExpectedHash = this.FromHex(HeaderValue, BitbucketConstants.SignatureHeaderName);
                if(ExpectedHash == null)
                {
                    context.Result = this.CreateBadHexEncodingResult(BitbucketConstants.SignatureHeaderName);

                    return;
                }

                // get the configured secret key
                var SecretKey = this.GetSecretKey(ReceiverName, Data, 0, 255);
                if(SecretKey == null)
                {
                    context.Result = new NotFoundResult();

                    return;
                }

                var Secret = Encoding.UTF8.GetBytes(SecretKey);

                // get the actual hash of the request body
                var ActualHash = await this.ComputeRequestBodySha256HashAsync(Request, Secret);

                // verify that the actual hash matches the expected hash
                if (!SecretEqual(ExpectedHash, ActualHash))
                {
                    ErrorResult = this.CreateBadSignatureResult(BitbucketConstants.SignatureHeaderName);
                    context.Result = ErrorResult;

                    return;
                }
            }

            await next();
        }
    }
}
