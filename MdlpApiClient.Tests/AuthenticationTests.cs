namespace MdlpApiClient.Tests
{
    using NUnit.Framework;
    using System.Net;
    using MdlpApiClient.DataContracts;

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

        [Test]
        public void ResourceNotFoundDifferentErrorMessages()
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

            // в теле ответа по неверному адресу branches/filter посылается HTML
            var ex = Assert.Throws<MdlpException>(() => client.Get("branches/filter"));
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
            Assert.IsTrue(ex.Message.Contains("404"));
            Assert.IsTrue(ex.Message.Contains("nginx"));

            // а тут по умолчанию RestSharp сообщает ошибку десериализации XML
            ex = Assert.Throws<MdlpException>(() => client.Get<EmptyResponse>("branches/filter"));
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
            Assert.IsTrue(ex.Message.Contains("404"));
            Assert.IsTrue(ex.Message.Contains("nginx"));

            // а здесь ресурс возвращает типизированный объект ErrorResponse
            ex = Assert.Throws<MdlpException>(() => client.Get<EmptyResponse>("reestr/shtuchek/dryuchek"));
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
            Assert.AreEqual("Not Found", ex.Message);

            // только в этом последнем случае у нас есть ErrorResponse
            var error = ex.ErrorResponse;
            Assert.NotNull(error);
            Assert.AreEqual("Not Found", error.Message);
            Assert.AreEqual(404, error.StatusCode);
            Assert.NotNull(error.Path);
            Assert.IsTrue(error.Path.EndsWith("dryuchek"));
        }
    }
}