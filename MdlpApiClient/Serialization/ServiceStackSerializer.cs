
namespace MdlpApiClient.Serialization
{
    using System;
    using RestSharp;
    using RestSharp.Serialization;
    using ServiceStack.Text;

    /// <summary>
    /// ServiceStack.Text.v4.0.33-based serializer.
    /// </summary>
    internal class ServiceStackSerializer : IRestSerializer
    {
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

        public T Deserialize<T>(IRestResponse response)
        {
            using (var scope = JsConfig.BeginScope())
            {
                scope.AlwaysUseUtc = false;
                scope.AssumeUtc = false;
                scope.AppendUtcOffset = false;
                return JsonSerializer.DeserializeFromString<T>(response.Content);
            }
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
                scope.DateHandler = DateHandler.UnixTime;
                return JsonSerializer.SerializeToString(obj);
            }
        }
    }
}
