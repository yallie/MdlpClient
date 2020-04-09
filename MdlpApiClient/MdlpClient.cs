using System;
using RestSharp;

namespace MdlpApiClient
{
    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public class MdlpClient
    {
        public const string StageApiUrl = "http://api.stage.mdlp.crpt.ru/api/v1/";

        /// <summary>
        /// Initializes a new instance of the MDLP REST API client.
        /// </summary>
        /// <param name="baseUrl">Base URL of the API endpoint.</param>
        /// <param name="credentials">Credentials used for authentication.</param>
        public MdlpClient(string baseUrl = StageApiUrl, CredentialsBase credentials = null)
        {
            BaseUrl = baseUrl;
            Client = new RestClient(BaseUrl);
        }

        public string BaseUrl { get; }

        public CredentialsBase Credentials { get; set; }

        private IRestClient Client { get; }

        /// <summary>
        /// Authenticates the user.
        /// This method should be called before all other methods.
        /// </summary>
        /// <param name="clientId">Client identity.</param>
        /// <param name="clientSecret">Client secret.</param>
        /// <param name="userId">User identity, user name or GOST certificate thumbprint.</param>
        /// <param name="password">Password (for the non-resident users).</param>
        public void Authenticate(string clientId, string clientSecret, string userId, string password = null)
        {
        }
    }
}
