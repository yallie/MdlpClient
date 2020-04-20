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

        [Test]
        public void Chapter7_05_1_GetFiasAddressObject()
        {
            var addr = Client.GetFiasAddressObject("353b7aed-0f1b-4f44-8ce3-245083e17526");
            Assert.IsNotNull(addr);
            Assert.AreEqual("385336", addr.PostalCode);
            Assert.AreEqual("Широкая", addr.FormalName);
        }

        [Test]
        public void Chapter7_05_2_GetFiasHouseObject()
        {
            var house = Client.GetFiasHouseObject("ba1c2f28-a455-47e2-95e5-000003a0023d");
            Assert.IsNotNull(house);
            Assert.AreEqual("391483", house.PostalCode);
            Assert.AreEqual("61226824016", house.Okato);
            Assert.AreEqual("61626424116", house.Oktmo);
            Assert.AreEqual("fce962f2-dff8-4eea-8413-5c94e0e69dec", house.AoGuid);
        }
    }
}
