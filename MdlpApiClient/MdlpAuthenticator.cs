using RestSharp;
using RestSharp.Authenticators;

namespace MdlpApiClient
{
    /// <summary>
    /// MDLP REST API authenticator.
    /// </summary>
    internal class MdlpAuthenticator : IAuthenticator
    {
        private string AuthHeader { get; set; }

        public MdlpAuthenticator(string authToken) =>
            SetAuthToken(authToken);

        public void SetAuthToken(string authToken) =>
            AuthHeader = string.IsNullOrWhiteSpace(authToken) ? null : $"token {authToken}";

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!string.IsNullOrWhiteSpace(AuthHeader))
            {
               request.AddOrUpdateParameter("Authorization", AuthHeader, ParameterType.HttpHeader);
            }
        }
    }
}
