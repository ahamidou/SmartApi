using System;

namespace SmartApi.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class JsonRedactAttribute : Attribute { }

}