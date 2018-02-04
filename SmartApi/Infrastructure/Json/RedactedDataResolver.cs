using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System;

namespace SmartApi.Infrastructure
{
    public class RedactedDataResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (member is PropertyInfo prop)
            {
                var shouldRedact = Attribute.IsDefined(prop, typeof(JsonRedactAttribute));
                if (shouldRedact)
                    property.ValueProvider = new RedactedValueProvider(property.PropertyType);
            }
            return property;
        }
    }
}