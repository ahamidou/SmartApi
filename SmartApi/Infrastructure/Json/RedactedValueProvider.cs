using Newtonsoft.Json.Serialization;
using System;

namespace SmartApi.Infrastructure
{
    public class RedactedValueProvider : IValueProvider
    {
        private readonly Type _type;
        private readonly bool _isString;
        public RedactedValueProvider(Type type)
        {
            _type = type;
            _isString = Type.GetTypeCode(type) == TypeCode.String;
        }
        public void SetValue(object target, object value)
        {
            throw new NotSupportedException();
        }

        public object GetValue(object target)
        {
            if (_isString) return "redacted";
            return default(Type);
        }
    }

}