using System;

namespace SmartApi.Infrastructure
{
    public interface IApiLogger
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(Exception exception);
    }

}