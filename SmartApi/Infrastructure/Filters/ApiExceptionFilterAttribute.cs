using System.Net.Http;
using System.Web.Http.Filters;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartApi.Infrastructure
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static IApiLogger logger => new TraceApiLogger();
        public override void OnException(HttpActionExecutedContext context)
        {
            var errorMessage = ProcessException(context.Exception);
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(errorMessage) };
        }

        private string ProcessException(Exception exception)
        {
            if (exception == null) return string.Empty;
            logger.LogError(exception);
            if (exception is AggregateException)
                return ProcessAggregateException(exception);
            if (exception is ApiCustomException)
                return exception.Message;
            return string.Empty;
        }

        private string ProcessAggregateException(Exception exception)
        {
            var aggregateException = exception as AggregateException;
            var exceptions = aggregateException.Flatten();
            var errorMessages = new List<string>();
            foreach (var innerException in exceptions.InnerExceptions)
            {
                var errorMessage = ProcessException(exception);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errorMessages.Add(errorMessage);
            }
            return errorMessages.Any() ? string.Join("\n", errorMessages) : string.Empty;
        }
    }
}