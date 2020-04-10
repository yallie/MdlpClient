namespace MdlpApiClient
{
    using RestSharp;
    using RestSharp.Authenticators;

    /// <summary>
    /// MDLP REST API authenticator using credentials.
    /// </summary>
    internal class CredentialsAuthenticator : IAuthenticator
    {
        public CredentialsAuthenticator(string baseUrl, CredentialsBase credentials)
        {
            BaseUrl = baseUrl;
            Credentials = credentials;
        }

        private string BaseUrl { get; set; }

        private CredentialsBase Credentials { get; set; }

        private enum AuthState
        {
            NotAuthenticated, InProgress, Authenticated
        }

        private AuthState State { get; set; } = AuthState.NotAuthenticated;

        private MdlpAuthToken AuthToken { get; set; }

        private string AuthHeader { get; set; }

        public void SetAuthToken(string authToken) =>
            AuthHeader = string.IsNullOrWhiteSpace(authToken) ? null : $"token {authToken}";

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            // perform authentication request
            if (State == AuthState.NotAuthenticated)
            {
                State = AuthState.InProgress;
                AuthToken = Credentials.Authenticate(BaseUrl, client);
                SetAuthToken(AuthToken.Token);
                State = AuthState.Authenticated;
            }

            // add authorization header if any
            if (!string.IsNullOrWhiteSpace(AuthHeader))
            {
               request.AddOrUpdateParameter("Authorization", AuthHeader, ParameterType.HttpHeader);
            }
        }
    }
}
