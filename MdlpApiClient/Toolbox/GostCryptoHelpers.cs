namespace MdlpApiClient.Toolbox
{
    using GostCryptography.Base;
    using GostCryptography.Pkcs;
    using System;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Cryptographic utilities for GOST provider.
    /// </summary>
    public class GostCryptoHelpers
    {
        /// <summary>
        /// For the unit tests, set this to the StoreLocation.CurrentUser.
        /// For the production code, keep it set to the StoreLocation.LocalMachine.
        /// Only Administrator or LocalSystem accounts can access the LocalMachine stores.
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
            signedCms.ComputeSignature(signer, true);

            // Encoding the CMS/PKCS#7 message
            var encoded = signedCms.Encode();
            return Convert.ToBase64String(encoded);
        }
    }
}
