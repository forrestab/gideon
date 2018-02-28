using Gideon.WebHooks.Receivers.BitbucketServer.Properties;
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
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Filters
{
    public class BitbucketVerifySignatureFilter : WebHookVerifySignatureFilter, IAsyncResourceFilter
    {
        public override string ReceiverName => BitbucketConstants.ReceiverName;

        private static readonly char[] SEPARATORS = new[] { '=' };

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
            
            if (!this.IsRequestApplicable(context.RouteData) && HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                await next();

                return;
            }

            IActionResult ErrorResult = base.EnsureSecureConnection(this.ReceiverName, context.HttpContext.Request);
            if (ErrorResult != null)
            {
                context.Result = ErrorResult;

                return;
            }
            
            string Header = base.GetRequestHeader(context.HttpContext.Request, BitbucketConstants.SignatureHeaderName, out ErrorResult);
            if (ErrorResult != null)
            {
                context.Result = ErrorResult;

                return;
            }

            TrimmingTokenizer Values = new TrimmingTokenizer(Header, SEPARATORS);
            TrimmingTokenizer.Enumerator Enumerator = Values.GetEnumerator();

            Enumerator.MoveNext();

            StringSegment HeaderKey = Enumerator.Current;
            if (Values.Count != 2 || !StringSegment.Equals(HeaderKey, BitbucketConstants.SignatureHeaderKey, StringComparison.OrdinalIgnoreCase))
            {
                string ErrorMessage = string.Format(CultureInfo.CurrentCulture, Resources.SignatureFilter_BadHeaderValue,
                    BitbucketConstants.SignatureHeaderName, BitbucketConstants.SignatureHeaderKey, "<value>");

                base.Logger.LogError(1, ErrorMessage);
                context.Result = new BadRequestObjectResult(ErrorMessage);

                return;
            }

            Enumerator.MoveNext();
            string HeaderValue = Enumerator.Current.Value;

            byte[] ExpectedHash = base.FromHex(HeaderValue, BitbucketConstants.SignatureHeaderName);
            if (ExpectedHash == null)
            {
                context.Result = base.CreateBadHexEncodingResult(BitbucketConstants.SignatureHeaderName);

                return;
            }
            
            byte[] Secret = this.GetSecret(this.ReceiverName, context.RouteData);
            if (Secret == null)
            {
                context.Result = new NotFoundResult();

                return;
            }
            
            byte[] ActualHash = await base.ComputeRequestBodySha256HashAsync(context.HttpContext.Request, Secret);
            if (!BitbucketVerifySignatureFilter.SecretEqual(ExpectedHash, ActualHash))
            {
                context.Result = base.CreateBadSignatureResult(BitbucketConstants.SignatureHeaderName);

                return;
            }

            await next();
        }

        private bool IsRequestApplicable(RouteData routeData)
        {
            return routeData.TryGetWebHookReceiverName(out string RequestReceiverName) && this.IsApplicable(RequestReceiverName);
        }

        private byte[] GetSecret(string receiverName, RouteData routeData)
        {
            string SecretKey = base.GetSecretKey(receiverName, routeData, 
                BitbucketConstants.SecretKeyMinLength, BitbucketConstants.SecretKeyMaxLength);

            return SecretKey != null ? Encoding.UTF8.GetBytes(SecretKey) : null;
        }
    }
}
