using RestSharp;

namespace MdlpApiClient
{
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
        public override MdlpAuthToken Authenticate(IRestClient restClient)
        {
            return new MdlpAuthToken
            {
                Token = "Hello",
                LifeTime = 10
            };
        }
    }
}
