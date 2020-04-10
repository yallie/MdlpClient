namespace MdlpApiClient.Tests
{
    using System;
    using NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class AuthenticationTests
    {
        private const string ClientID = "01db16f2-9a4e-4d9f-b5e8-c68f12566fd5";
        private const string ClientSecret = "9199fe04-42c3-4e81-83b5-120eb5f129f2";
        private const string UserStarter1 = "starter_resident_1";
        private const string UserPassword1 = "password";
        private const string TestDocumentID = "60786bb4-fcb5-4587-b703-d0147e3f9d1c";

        [Test]
        public void AuthenticateNonResident1()
        {
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID,
                ClientSecret = ClientSecret,
                UserID = UserStarter1,
                Password = UserPassword1
            });

            var md = client.GetDocumentMetadata(TestDocumentID);
        }
    }
}