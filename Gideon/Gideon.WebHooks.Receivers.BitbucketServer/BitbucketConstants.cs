namespace Gideon.WebHooks.Receivers.BitbucketServer
{
    public static class BitbucketConstants
    {
        public static string EventHeaderName => "X-Event-Key";
        public static string ReceiverName => "bitbucket";
        public static string RequestIdHeaderName => "X-Request-Id";
        public static string RequestIdParameterName => "requestId";
        public static string HubSignatureHeaderName => "X-Hub-Signature";
        public static string HubSignatureParameterName => "signature";
    }
}
