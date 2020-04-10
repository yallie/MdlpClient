namespace MdlpApiClient
{
    using System;
    using RestSharp;

    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public class MdlpClient
    {
        public const string StageApiUrl = "http://api.stage.mdlp.crpt.ru/api/v1/";

        /// <summary>
        /// Initializes a new instance of the MDLP REST API client.
        /// </summary>
        /// <param name="credentials">Credentials used for authentication.</param>
        /// <param name="baseUrl">Base URL of the API endpoint.</param>
        public MdlpClient(CredentialsBase credentials, string baseUrl = StageApiUrl)
        {
            // make sure BaseUrl ends with a slash
            BaseUrl = baseUrl ?? string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                BaseUrl += "/";
            }

            Client = new RestClient(BaseUrl)
            {
                Authenticator = new CredentialsAuthenticator(this, credentials),
                ThrowOnAnyError = true
            };
        }

        public string BaseUrl { get; private set; }

        private IRestClient Client { get; set; }

        public T Get<T>(string url)
            where T : class, new()
        {
            var request = new RestRequest(BaseUrl + url, DataFormat.Json);
            var response = Client.Get<T>(request);
            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        public T Post<T>(string url, object body)
            where T : class, new()
        {
            var request = new RestRequest(BaseUrl + url, DataFormat.Json);
            request.AddJsonBody(body);

            var response = Client.Post<T>(request);
            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        public MdlpDocumentMetadata GetDocumentMetadata(string documentId)
        {
            return Get<MdlpDocumentMetadata>("documents/" + documentId);
        }
    }
}
