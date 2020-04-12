
namespace MdlpApiClient.Serialization
{
    using RestSharp;
    using RestSharp.Serialization;
    using ServiceStack.Text;

    /// <summary>
    /// ServiceStack.Text-based serializer.
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
            return JsonSerializer.DeserializeFromString<T>(response.Content);
        }

        public string Serialize(Parameter bodyParameter)
        {
            return Serialize(bodyParameter.Value);
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.SerializeToString(obj);
        }
    }
}
