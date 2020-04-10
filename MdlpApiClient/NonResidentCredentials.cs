namespace MdlpApiClient
{
    using RestSharp;

    /// <summary>
    /// Non-resident user credentials (password-based authentication).
    /// </summary>
    public class NonResidentCredentials : CredentialsBase
    {
        /// <summary>
        /// Gets or sets the user identity.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// </inheritdoc>
        public override MdlpAuthToken Authenticate(string baseUrl, IRestClient restClient)
        {
            // get authentication code
            var url = baseUrl + "auth";
            var request = new RestRequest(url, DataFormat.Json);
            request.AddJsonBody(new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "PASSWORD",
            });

            var authResponse = restClient.Post<MdlpAuthResponse>(request);
            var authCode = authResponse.Data.Code;

            // get authentication token
            url = baseUrl + "token";
            request = new RestRequest(url, DataFormat.Json);
            request.AddJsonBody(new
            {
                code = authCode,
                password = Password,
            });

            var tokenResponse = restClient.Post<MdlpAuthToken>(request);
            return tokenResponse.Data;
        }
    }
}
