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
        public void Chapter7_01_1_GetEgrulRegistryEntry()
        {
            var entry = Client.GetEgrulRegistryEntry();
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.Inn);
        }

        [Test]
        public void Chapter7_02_1_GetEgripRegistryEntry()
        {
            // UserStarter1 — не ИП, поэтому запись в ЕГРИП отсутствует
            var ex = Assert.Throws<MdlpException>(() => Client.GetEgripRegistryEntry());
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }

        [Test]
        public void Chapter7_03_1_GetRafpRegistryEntry()
        {
            // UserStarter1 — не филиал и не представительство, поэтому запись в РАФП отсутствует
            var ex = Assert.Throws<MdlpException>(() => Client.GetRafpRegistryEntry());
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }
    }
}
