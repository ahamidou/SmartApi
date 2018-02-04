using System.Net.Http;

namespace SmartApi.Infrastructure
{
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static string GetIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }
            if (!request.Properties.ContainsKey(RemoteEndpointMessage)) return null;
            dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
            return remoteEndpoint?.Address;
        }
    }
}