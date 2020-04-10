namespace MdlpApiClient.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class AuthenticationTests : UnitTestsBase
    {
        [Test]
        public void AuthenticateNonResident1()
        {
            // the client is not connected until the first call is performed
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID,
                ClientSecret = ClientSecret,
                UserID = UserStarter1,
                Password = UserPassword1
            });

            // the next line authenticates, then it requests a document
            var md = client.GetDocumentMetadata(TestDocumentID);
            Assert.NotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
        }

        [Test]
        public void AuthenticateNonResident2()
        {
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID,
                ClientSecret = ClientSecret,
                UserID = UserStarter2,
                Password = UserPassword2
            });

            // the second user doesn't seem to have the DOWNLOAD_DOCUMENT permission
            Assert.Throws<MdlpException>(() =>
            {
                var md = client.GetDocumentMetadata(TestDocumentID);
                Assert.NotNull(md);
                Assert.AreEqual(TestDocumentID, md.DocumentID);
            });
        }

        [Test]
        public void AuthenticateResident()
        {
            var client = new MdlpClient(credentials: new ResidentCredentials
            {
                ClientID = ClientID,
                ClientSecret = ClientSecret,
                UserID = TestUserID,
            });

            // the document is available to the test user
            var md = client.GetDocumentMetadata(TestDocumentID);
            Assert.NotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
        }
    }
}