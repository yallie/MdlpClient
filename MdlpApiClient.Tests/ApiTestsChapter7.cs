namespace MdlpApiClient.Tests
{
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter7 : UnitTestsBase
    {
        private MdlpClient Client = new MdlpClient(credentials: new NonResidentCredentials
        {
            ClientID = ClientID1,
            ClientSecret = ClientSecret1,
            UserID = UserStarter1,
            Password = UserPassword1,
        })
        {
            Tracer = TestContext.Progress.WriteLine
        };

        [Test]
        public void GetEgrulRegistryEntry_7_1_1()
        {
            var entry = Client.GetEgrulRegistryEntry();
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.Inn);
        }

        [Test]
        public void GetEgripRegistryEntry_7_2_1()
        {
            // UserStarter1 — не ИП, поэтому запись в ЕГРИП отсутствует
            var ex = Assert.Throws<MdlpException>(() => Client.GetEgripRegistryEntry());
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }

        [Test]
        public void GetRafpRegistryEntry_7_3_1()
        {
            // UserStarter1 — не филиал и не представительство, поэтому запись в РАФП отсутствует
            var ex = Assert.Throws<MdlpException>(() => Client.GetRafpRegistryEntry());
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }
    }
}
