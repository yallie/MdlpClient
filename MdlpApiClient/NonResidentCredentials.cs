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
        public override MdlpAuthToken Authenticate(MdlpClient apiClient)
        {
            // get authentication code
            var authResponse = apiClient.Post<MdlpAuthResponse>("auth", new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "PASSWORD",
            });

            // get authentication token
            return apiClient.Post<MdlpAuthToken>("token", new
            {
                code = authResponse.Code,
                password = Password,
            });
        }
    }
}
