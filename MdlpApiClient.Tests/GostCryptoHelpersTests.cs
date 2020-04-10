namespace MdlpApiClient.Tests
{
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    [TestFixture]
    public class GostCryptoHelpersTests
    {
        static GostCryptoHelpersTests()
        {
            // for unit tests: use current user's certificates
            GostCryptoHelpers.DefaultStoreLocation = StoreLocation.CurrentUser;
        }

        private const string SubjectName = @"Тестовый УКЭП им. Юрия Гагарина";

        private X509Certificate2 GetTestCertificate()
        {
            return GostCryptoHelpers.FindCertificate(SubjectName);
        }

        [Test]
        public void GostCryproProviderIsInstalled()
        {
            Assert.IsTrue(GostCryptoHelpers.IsGostCryptoProviderInstalled());
        }

        [Test]
        public void CertificateWithPrivateKeyIsLoaded()
        {
            var cert = GetTestCertificate();
            Assert.IsNotNull(cert);
            Assert.AreEqual(cert.Thumbprint, "1F9CA1F4DA4BE1A78A260D45376A8F71F5FFBA90");
        }

        [Test]
        public void CertificateCanBeUsedToComputeDetachedCmsSignature()
        {
            var cert = GetTestCertificate();
            var sign = GostCryptoHelpers.ComputeDetachedSignature(cert, "Привет!");
            Assert.IsNotNull(sign);
            Assert.IsTrue(sign.StartsWith("MII"));
            Assert.IsTrue(sign.Length > 1000);
        }
    }
}
