using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Collections.Generic;

namespace SmartApi.Infrastructure
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        private static IApiLogger logger => new TraceApiLogger();
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            LogRequestAsync(actionContext.Request, actionContext.ActionArguments);

            if (actionContext.ActionArguments.ContainsValue(null))
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, "The argument cannot be null");
            }

            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            LogResponseAsync(actionExecutedContext.Response);
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        private void LogRequestAsync(HttpRequestMessage request, Dictionary<string, object> actionArguments)
        {
            if (request == null) return;
            var requestInfo = $"({request.Method}) {request.RequestUri.PathAndQuery}";
            var requestHeaders = request.Headers.ToJsonString();
            var ipAddress = request.GetIpAddress();
            logger.LogInfo($"{{\"IpAddress\":\"{ipAddress}\", \"Request\": \"{requestInfo}\", \"Headers\":{requestHeaders}, \"Content\":{actionArguments.JsonSerialize()}}}");
        }

        private void LogResponseAsync(HttpResponseMessage response)
        {
            if (response == null) return;
            var responseContent = "{}";
            if (response.IsSuccessStatusCode)
                responseContent = response.Content.ToJsonString();
            var responseHeaders = response.Headers.ToJsonString();
            logger.LogInfo($"{{\"Status\": \"{response.StatusCode}\",\"Headers\":{responseHeaders},\"Content\":{responseContent}}}");
        }

    }
}