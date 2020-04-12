namespace MdlpApiClient
{
    using System.Text;
    using RestSharp;
    using System.Security.Cryptography.X509Certificates;
    using MdlpApiClient.Toolbox;

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

            Credentials = credentials;
            Client = new RestClient(BaseUrl)
            {
                Authenticator = new CredentialsAuthenticator(this, credentials),
                Encoding = Encoding.UTF8,
                ThrowOnAnyError = true
            };
        }

        public string BaseUrl { get; private set; }

        private IRestClient Client { get; set; }

        private CredentialsBase Credentials { get; set; }

        private X509Certificate2 userCertificate;

        /// <summary>
        /// X.509 certificate of the resident user (if applicable).
        /// </summary>
        internal X509Certificate2 UserCertificate
        {
            set { userCertificate = value; }
            get
            {
                if (userCertificate == null)
                {
                    userCertificate = GostCryptoHelpers.FindCertificate(Credentials.UserID);
                }

                return userCertificate;
            }
        }

        /// <summary>
        /// Computes the detached digital signature of the given text.
        /// </summary>
        /// <param name="textToSign">Text to sign.</param>
        /// <returns>Detached signature in CMS format and base64 encoding.</returns>
        private string ComputeSignature(string textToSign)
        {
            if (UserCertificate == null)
            {
                return null;
            }

            return GostCryptoHelpers.ComputeDetachedSignature(UserCertificate, textToSign);
        }

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="request">The request to execute.</param>
        internal T Execute<T>(IRestRequest request)
            where T : class, new()
        {
            // authenticator traces all outgoing requests
            var response = Client.Execute<T>(request);
            Trace(response);

            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        /// <summary>
        /// Performs GET request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        public T Get<T>(string url)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            return Execute<T>(request);
        }

        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        public T Post<T>(string url, object body)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            return Execute<T>(request);
        }
    }
}
