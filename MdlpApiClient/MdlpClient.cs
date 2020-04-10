namespace MdlpApiClient
{
    using System;
    using DataContracts;
    using RestSharp;

    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public partial class MdlpClient
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

        public T Execute<T>(IRestRequest request)
            where T : class, new()
        {
            Trace(request);
            var response = Client.Execute<T>(request);
            Trace(response);

            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        public T Get<T>(string url)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            return Execute<T>(request);
        }

        public T Post<T>(string url, object body)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);

            return Execute<T>(request);
        }

        public DocumentMetadata GetDocumentMetadata(string documentId)
        {
            return Get<DocumentMetadata>("documents/" + documentId);
        }

        public string RegisterResidentUser(string sysId, string publicCertificate,
            string firstName, string lastName, string middleName, string email)
        {
            var user = Post<RegisterResidentUserResponse>("registration/user_resident", new
            {
                sys_id = sysId,
                public_cert = publicCertificate,
                first_name = firstName,
                last_name = lastName,
                middle_name = middleName,
                email = email
            });

            return user.UserID;
        }
    }
}
