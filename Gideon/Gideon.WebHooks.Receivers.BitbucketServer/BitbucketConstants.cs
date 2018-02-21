namespace Gideon.WebHooks.Receivers.BitbucketServer
{
    public static class BitbucketConstants
    {
        public static string ReceiverName => "bitbucket";
        public static string EventHeaderName => "X-Event-Key";
        public static string RequestIdHeaderName => "X-Request-Id";
        public static string RequestIdParameterName => "requestId";
        public static string SignatureHeaderKey => "sha256";
        public static string SignatureHeaderName => "X-Hub-Signature";
    }
}
