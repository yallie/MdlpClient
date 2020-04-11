namespace MdlpApiClient.Tests
{
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    [TestFixture]
    public class GostCryptoHelpersTests : UnitTestsBase
    {
        private X509Certificate2 GetTestCertificate()
        {
            return GostCryptoHelpers.FindCertificate(TestCertificateSubjectName);
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
            Assert.AreEqual(cert.Thumbprint, TestCertificateThumbprint);
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
