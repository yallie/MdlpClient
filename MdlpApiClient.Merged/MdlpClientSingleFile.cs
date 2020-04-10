// This is an auto-generated file.
namespace Ultima.Scripting
{
namespace MdlpApiClient
{
    using RestSharp;
    using RestSharp.Authenticators;

    /// <summary>
    /// MDLP REST API authenticator using credentials.
    /// </summary>
    internal class CredentialsAuthenticator : IAuthenticator
    {
        public CredentialsAuthenticator(MdlpClient apiClient, CredentialsBase credentials)
        {
            State = AuthState.NotAuthenticated;
            Client = apiClient;
            Credentials = credentials;
        }

        private MdlpClient Client { get; set; }

        private CredentialsBase Credentials { get; set; }

        private AuthState State { get; set; }

        private enum AuthState
        {
            NotAuthenticated, InProgress, Authenticated
        }

        private MdlpAuthToken AuthToken { get; set; }

        private string AuthHeader { get; set; }

        public void SetAuthToken(string authToken)
        {
            AuthHeader = string.IsNullOrWhiteSpace(authToken) ?
                null : "token " + authToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            // perform authentication request
            if (State == AuthState.NotAuthenticated)
            {
                State = AuthState.InProgress;
                AuthToken = Credentials.Authenticate(Client);
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

namespace MdlpApiClient
{
    /// <summary>
    /// MDLP REST API credentials base class.
    /// </summary>
    public abstract class CredentialsBase
    {
        /// <summary>
        /// Client identifier.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Performs authentication, returns access token with a limited lifetime.
        /// </summary>
        /// <param name="apiClient">MDLP client to perform API calls.</param>
        /// <returns><see cref="MdlpAuthToken"/> instance.</returns>
        public abstract MdlpAuthToken Authenticate(MdlpClient restClient);
    }
}

namespace MdlpApiClient
{
    using GostCryptography.Base;
    using GostCryptography.Pkcs;
    using System;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class GostCryptoHelpers
    {
        /// <summary>
        /// For the unit tests, set this to the StoreLocation.CurrentUser.
        /// For the production code, keep it set to the StoreLocation.LocalMachine.
        /// </summary>
        public static StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

        /// <summary>
        /// Checks if GOST cryptoprovider CryptoPro is installed.
        /// </summary>
        public static bool IsGostCryptoProviderInstalled()
        {
            return
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro) &&
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro_2012_512) &&
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro_2012_1024);
        }

        /// <summary>
        /// Looks for the GOST certificate with a private key using the subject name or a thumbprint.
        /// Returns null, if certificate is not found, the algorithm isn't GOST-compliant, or the private key is not associated with it.
        /// </summary>
        public static X509Certificate2 FindCertificate(string cnameOrThumbprint, StoreName storeName = StoreName.My, StoreLocation? storeLocation = null)
        {
            // avoid returning any certificate
            if (string.IsNullOrWhiteSpace(cnameOrThumbprint))
            {
                return null;
            }

            // a thumbprint is a hexadecimal number, compare it case-insensitive
            using (var store = new X509Store(storeName, storeLocation ?? DefaultStoreLocation))
            {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                foreach (var certificate in store.Certificates)
                {
                    if (certificate.HasPrivateKey && certificate.IsGost())
                    {
                        var nameMatches = certificate.SubjectName.Name.IndexOf(cnameOrThumbprint, StringComparison.OrdinalIgnoreCase) >= 0;
                        var thumbprintMatches = StringComparer.OrdinalIgnoreCase.Equals(certificate.Thumbprint, cnameOrThumbprint);
                        if (nameMatches || thumbprintMatches)
                        {
                            return certificate;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Signs the message with a GOST digital signature and returns the detached signature (CMS format, base64 encoding).
        /// Detached signature is a CMS message, that doesn't contain the original signed data: only the signature and the certificates.
        /// </summary>
        public static string ComputeDetachedSignature(X509Certificate2 certificate, string textToSign)
        {
            // The following line opens the private key.
            // It requires that the current user has permissions to use the private key.
            // Permissions are given using MMC console, Certificates snap-in.
            var privateKey = (GostAsymmetricAlgorithm)certificate.GetPrivateKeyAlgorithm();
            var publicKey = (GostAsymmetricAlgorithm)certificate.GetPublicKeyAlgorithm();
            var message = Encoding.UTF8.GetBytes(textToSign);

            // Create GOST-compliant signature helper
            var signedCms = new GostSignedCms(new ContentInfo(message), true);

            // The object that has the signer information
            var signer = new CmsSigner(certificate);

            // Computing the CMS/PKCS#7 signature
            signedCms.ComputeSignature(signer);

            // Encoding the CMS/PKCS#7 message
            var encoded = signedCms.Encode();
            return Convert.ToBase64String(encoded);
        }
    }
}

namespace MdlpApiClient
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class MdlpAuthResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}

namespace MdlpApiClient
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// MDLP REST API authentication token.
    /// </summary>
    [DataContract]
    public class MdlpAuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdlpAuthToken"/>.
        /// </summary>
        public MdlpAuthToken()
        {
            // make sure we don't expire prematurely
            CreationDate = DateTime.Now.AddSeconds(-30);
        }

        [IgnoreDataMember]
        public DateTime CreationDate { get; private set; }

        [IgnoreDataMember]
        public DateTime ExpirationDate
        {
            get { return CreationDate.AddMinutes(LifeTime); }
        }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "life_time")]
        public int LifeTime { get; set; }
    }
}

namespace MdlpApiClient
{
    using System;
    using RestSharp;

    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public class MdlpClient
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

        public T Get<T>(string url)
            where T : class, new()
        {
            var request = new RestRequest(BaseUrl + url, DataFormat.Json);
            var response = Client.Get<T>(request);
            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        public T Post<T>(string url, object body)
            where T : class, new()
        {
            var request = new RestRequest(BaseUrl + url, DataFormat.Json);
            request.AddJsonBody(body);

            var response = Client.Post<T>(request);
            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        public MdlpDocumentMetadata GetDocumentMetadata(string documentId)
        {
            return Get<MdlpDocumentMetadata>("documents/" + documentId);
        }
    }
}

namespace MdlpApiClient
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class MdlpDocumentMetadata
    {
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "sender")]
        public string SenderID { get; set; } // "935ba7bc-b022-11e7-abc4-cec278b6b50a",

        [DataMember(Name = "sys_id")]
        public string SystemID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        [DataMember(Name = "doc_status")]
        public string DocStatus { get; set; } // "PROCESSED_DOCUMENT",

        [DataMember(Name = "device_id")]
        public string DeviceID { get; set; } // 1230000011111111 (optional)

        [DataMember(Name = "skzkm_origin_msg_id")]
        public string SkzkmOriginMessageID { get; set; } // "e2cb20c1-1d5b-4ab6-b8dd-9297bec23f63" (optional)

        [DataMember(Name = "version")]
        public string Version { get; set; } // API version: "1.28"
    }
}

namespace MdlpApiClient
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class MdlpException : Exception
    {
        public MdlpException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        protected MdlpException(SerializationInfo info, StreamingContext context)
        {
            Code = (HttpStatusCode)info.GetInt32("Code");
        }

        public HttpStatusCode Code { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Code", (int)Code);
        }
    }
}

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

namespace MdlpApiClient
{
    using System.Security;

    /// <summary>
    /// Resident credentials. Uses GOST cryptocertificate with a private key.
    /// </summary>
    public class ResidentCredentials : CredentialsBase
    {
        /// <summary>
        /// GOST Certificate subject name or thumbprint.
        /// </summary>
        public string UserID { get; set; }

        /// </inheritdoc>
        public override MdlpAuthToken Authenticate(MdlpClient apiClient)
        {
            // load the certificate with a private key by userId
            var certificate = GostCryptoHelpers.FindCertificate(UserID);
            if (certificate == null)
            {
                throw new SecurityException("GOST-compliant certificate not found. " +
                    "Make sure that the certificate is properly installed and has the associated private key. " +
                    "Thumbprint or subject name: " + UserID);
            }

            // get authentication code
            var authResponse = apiClient.Post<MdlpAuthResponse>("auth", new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "SIGNED_CODE",
            });

            // get authentication token
            return apiClient.Post<MdlpAuthToken>("token", new
            {
                code = authResponse.Code,
                signature = GostCryptoHelpers.ComputeDetachedSignature(certificate, authResponse.Code),
            });
        }
    }
}

}
