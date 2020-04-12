namespace MdlpApiClient.Tests
{
    using NUnit.Framework;
    using System.Net;

    [TestFixture]
    public class AuthenticationTests : UnitTestsBase
    {
        [Test]
        public void AuthenticateNonResident1()
        {
            // the client is not connected until the first call is performed
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID1,
                ClientSecret = ClientSecret1,
                UserID = UserStarter1,
                Password = UserPassword1
            })
            {
                Tracer = TestContext.Progress.WriteLine
            };

            // the next line authenticates, then it requests a document
            var md = client.GetDocumentMetadata(TestDocumentID);
            Assert.NotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
        }

        [Test]
        public void AuthenticateNonResident2Error400()
        {
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID1,
                ClientSecret = ClientSecret1,
                UserID = UserStarter2,
                Password = UserPassword2
            })
            {
                Tracer = TestContext.Progress.WriteLine
            };

            // the second user's sysId mismatch => BadRequest, error 400
            var ex = Assert.Throws<MdlpException>(() =>
            {
                client.GetDocumentMetadata(TestDocumentID);
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode); // 400
        }

        [Test]
        public void AuthenticateNonResident2()
        {
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID2,
                ClientSecret = ClientSecret2,
                UserID = UserStarter2,
                Password = UserPassword2
            })
            {
                Tracer = TestContext.Progress.WriteLine
            };

            // the second user doesn't seem to have the DOWNLOAD_DOCUMENT permission => Forbidden, error 403
            var ex = Assert.Throws<MdlpException>(() =>
            {
                client.GetDocumentMetadata(TestDocumentID);
            });

            Assert.AreEqual(HttpStatusCode.Forbidden, ex.StatusCode); // 403
        }

        [Test]
        public void AuthenticateResident()
        {
            var client = new MdlpClient(credentials: new ResidentCredentials
            {
                ClientID = ClientID1,
                ClientSecret = ClientSecret1,
                UserID = TestUserID,
            })
            {
                Tracer = TestContext.Progress.WriteLine
            };

            // the document is available to the test user
            var md = client.GetDocumentMetadata(TestDocumentID);
            Assert.NotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
        }
    }
}