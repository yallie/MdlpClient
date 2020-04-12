namespace MdlpApiClient
{
    using System.Security;
    using DataContracts;
    using MdlpApiClient.Toolbox;

    /// <summary>
    /// Resident credentials. Uses GOST cryptocertificate with a private key.
    /// </summary>
    public class ResidentCredentials : CredentialsBase
    {
        /// </inheritdoc>
        public override AuthToken Authenticate(MdlpClient apiClient)
        {
            // load the certificate with a private key by userId
            var certificate = apiClient.UserCertificate;
            if (certificate == null)
            {
                throw new SecurityException("GOST-compliant certificate not found. " +
                    "Make sure that the certificate is properly installed and has the associated private key. " +
                    "Thumbprint or subject name: " + UserID);
            }

            // get authentication code
            var authResponse = apiClient.Post<AuthResponse>("auth", new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "SIGNED_CODE",
            });

            // get authentication token
            return apiClient.Post<AuthToken>("token", new
            {
                code = authResponse.Code,
                signature = GostCryptoHelpers.ComputeDetachedSignature(certificate, authResponse.Code),
            });
        }
    }
}
