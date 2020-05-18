
namespace MdlpApiClient.Serialization
{
    using System;
    using MdlpApiClient.DataContracts;
    using RestSharp;
    using RestSharp.Serialization;
    using ServiceStack.Text;

    /// <summary>
    /// ServiceStack.Text.v4.0.33-based serializer.
    /// </summary>
    internal class ServiceStackSerializer : IRestSerializer
    {
        static ServiceStackSerializer()
        {
            // use custom serialization only for our own types
            JsConfig<CustomDate>.SerializeFn = c => c;
            JsConfig<CustomDate>.DeSerializeFn = s => CustomDate.Parse(s);
            JsConfig<CustomDateTime>.SerializeFn = c => c;
            JsConfig<CustomDateTime>.DeSerializeFn = s => CustomDateTime.Parse(s);
            JsConfig<CustomDateTimeSpace>.SerializeFn = c => c;
            JsConfig<CustomDateTimeSpace>.DeSerializeFn = s => CustomDateTimeSpace.Parse(s);
        }

        public string[] SupportedContentTypes
        {
            get
            {
                return new[]
                {
                    "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
                };
            }
        }

        public DataFormat DataFormat
        {
            get { return DataFormat.Json; }
        }

        private string contentType = "application/json";

        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        internal T Deserialize<T>(string content)
        {
            using (var scope = JsConfig.BeginScope())
            {
                scope.AlwaysUseUtc = false;
                scope.AssumeUtc = false;
                scope.AppendUtcOffset = false;
                return JsonSerializer.DeserializeFromString<T>(content);
            }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return Deserialize<T>(response.Content);
        }

        public string Serialize(Parameter bodyParameter)
        {
            return Serialize(bodyParameter.Value);
        }

        public string Serialize(object obj)
        {
            using (var scope = JsConfig.BeginScope())
            {
                scope.IncludeTypeInfo = false;

                // ISO8601DateTime: "2019-04-24 20:43:44"
                // ISO8601DateOnly: "2018-12-12"
                // ISO8601: "2018-12-12T00:00:00.0000000"
                scope.DateHandler = DateHandler.ISO8601;
                return JsonSerializer.SerializeToString(obj);
            }
        }
    }
}
