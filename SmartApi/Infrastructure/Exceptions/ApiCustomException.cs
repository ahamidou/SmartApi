using System;

namespace SmartApi.Infrastructure
{
    public class ApiCustomException : Exception
    {
        public ApiCustomException(string message) : base(message) { }
    }
}