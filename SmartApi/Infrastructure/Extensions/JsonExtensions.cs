using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace SmartApi.Infrastructure
{
    public static class JsonExtensions
    {
        private static JsonSerializerSettings JsonSettings => new JsonSerializerSettings { ContractResolver = new RedactedDataResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public static string JsonSerialize<T>(this T content)
        {
            if (content == null) return string.Empty;
            return JsonConvert.SerializeObject(content, JsonSettings);
        }

        public static string ToJsonString(this HttpContent content)
        {
            if (content == null) return string.Empty;
            var contentJsonString = content.JsonSerialize();
            dynamic data = JObject.Parse(contentJsonString);
            var value = data.Value as object;
            return value == null ? string.Empty : value.JsonSerialize();
        }


        public static string ToJsonString(this IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                dictionary.Add(header.Key, string.Join(", ", header.Value));
            }
            return dictionary.JsonSerialize();
        }
    }
}