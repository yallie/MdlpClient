using System;
using NUnit;
using NUnit.Framework;

namespace MdlpApiClient.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        [Test]
        public void AuthenticateNonResident1()
        {
            var client = new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = "01db16f2-9a4e-4d9f-b5e8-c68f12566fd5",
                ClientSecret = "9199fe04-42c3-4e81-83b5-120eb5f129f2",
                UserID = "starter_resident_1",
                Password = "password"
            });
        }
    }
}