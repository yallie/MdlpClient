namespace MdlpApiClient
{
    using DataContracts;

    /// <summary>
    /// Non-resident user credentials (password-based authentication).
    /// </summary>
    public class NonResidentCredentials : CredentialsBase
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <inheritdoc/>
        public override AuthToken Authenticate(MdlpClient apiClient)
        {
            // get authentication code
            var authCode = apiClient.Authenticate(ClientID, ClientSecret, UserID, AuthTypeEnum.PASSWORD);

            // get authentication token
            return apiClient.GetToken(authCode, password: Password);
        }
    }
}
