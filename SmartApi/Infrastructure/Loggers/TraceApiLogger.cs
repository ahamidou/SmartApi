using System;
using System.Diagnostics;

namespace SmartApi.Infrastructure
{
    public class TraceApiLogger : IApiLogger
    {
        public void LogDebug(string message)
        {
            throw new System.NotImplementedException();
        }

        public void LogError(string message)
        {
            throw new System.NotImplementedException();
        }

        public void LogError(Exception exception)
        {
            var throwingMethod = new StackTrace(exception).GetFrame(0).GetMethod().Name;
            var message = $"{{\"Method\": \"{throwingMethod}\", \"Message\": \"{exception.Message}\", \"InnerExceptionMessage\": \"{exception.InnerException?.Message}\"}}";
            Trace.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Trace.WriteLine(message);
        }

        public void LogWarning(string message)
        {
            throw new System.NotImplementedException();
        }

    }

}