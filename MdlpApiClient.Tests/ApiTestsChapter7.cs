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
            AssertRequired(house);
        }

        [Test]
        public void Chapter7_05_3_GetFiasAddress()
        {
            // дом и адрес
            var addr = Client.GetFiasAddress("ba1c2f28-a455-47e2-95e5-000003a0023d", "fce962f2-dff8-4eea-8413-5c94e0e69dec");
            Assert.IsNotNull(addr);
            Assert.AreEqual("обл Рязанская, р-н Путятинский, с Унгор, ул Молодежная, Дом 2", addr.Address);
            AssertRequired(addr);

            // только дом
            addr = Client.GetFiasAddress("5be4355e-e19e-4752-b436-d2efa4af46ec");
            Assert.IsNotNull(addr);
            Assert.AreEqual("Респ Адыгея, р-н Красногвардейский, х Чумаков, ул Широкая, Домовладение 2", addr.Address);
            AssertRequired(addr);

            // комната, похоже, игнорируется
            addr = Client.GetFiasAddress("abc31736-35c1-4443-a061-b67c183b590a", room: "1");
            Assert.IsNotNull(addr);
            Assert.AreEqual("г Москва, ул Сухонская, Дом 11А", addr.Address);
            AssertRequired(addr);
        }

        [Test]
        public void Chapter7_06_1_GetProductionLicenses()
        {
            var licenses = Client.GetProductionLicenses();
            Assert.NotNull(licenses);
            Assert.AreEqual(1, licenses.Length);

            var license = licenses[0];
            Assert.AreEqual("ЛС-000613", license.LicenseNumber);
            AssertRequired(license);
        }

        [Test]
        public void Chapter7_06_2_GetProductionLicenses()
        {
            var licenses = Client.GetProductionLicenses(new LicenseApiFilter
            {
                LicenseNumber = "ЛС-000613"
            }, 0, 10);
            Assert.NotNull(licenses);
            Assert.NotNull(licenses.Entries);
            Assert.AreEqual(1, licenses.Total);
            Assert.AreEqual(1, licenses.Entries.Length);

            var license = licenses.Entries[0];
            Assert.AreEqual("ЛС-000613", license.LicenseNumber);
            AssertRequired(license);
        }

        [Test]
        public void Chapter7_06_3_ResyncProductionLicenses()
        {
            Assert.DoesNotThrow(() => Client.ResyncProductionLicenses());
        }

        [Test]
        public void Chapter7_07_1_GetPharmacyLicenses()
        {
            var licenses = Client.GetPharmacyLicenses();
            Assert.NotNull(licenses);
            Assert.AreEqual(0, licenses.Length);
        }

        [Test]
        public void Chapter7_07_2_GetPharmacyLicenses()
        {
            var licenses = Client.GetPharmacyLicenses(null, 0, 10);
            Assert.NotNull(licenses);
            Assert.NotNull(licenses.Entries);
            Assert.AreEqual(0, licenses.Total);
            Assert.AreEqual(0, licenses.Entries.Length);
        }

        [Test]
        public void Chapter7_07_3_ResyncPharmacyLicenses()
        {
            Assert.DoesNotThrow(() => Client.ResyncPharmacyLicenses());
        }

        [Test]
        public void Chapter7_08_1_GetCurrentAddresses()
        {
            var addresses = Client.GetCurrentAddresses();
            Assert.NotNull(addresses);
            Assert.AreEqual(4, addresses.Total);
            AssertRequired(addresses);

            // обязательные поля заполнены
            foreach (var addr in addresses.Entries)
            {
                AssertRequired(addr);
            }

            // одно место осуществления деятельности
            var branch = addresses.Entries.Single(a => a.EntityType == BranchEntry.EntityType);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", branch.Address.AddressDescription);

            // три склада, один из них по тому же адресу, что и место осуществления деятельности
            var whouse = addresses.Entries.Single(a => a.EntityType == WarehouseEntry.EntityType && a.Address.HouseGuid == branch.Address.HouseGuid);
            Assert.AreEqual(branch.Address.AddressDescription, whouse.Address.AddressDescription);
        }

        [Test]
        public void Chapter7_09_1_GetCountries()
        {
            var countries = Client.GetCountries(0, 10);
            Assert.NotNull(countries);
            Assert.NotNull(countries.Entries);
            Assert.AreEqual(10, countries.Entries.Length);
            Assert.IsTrue(countries.Total > 10);
            AssertRequired(countries);

            // обязательные поля заполнены
            foreach (var country in countries.Entries)
            {
                AssertRequired(country);
            }

            // в первой десятке по алфавиту есть Австралия
            var aus = countries.Entries.Single(c => c.Alpha3 == "AUS");
            Assert.AreEqual("AU", aus.Alpha2);
            Assert.AreEqual("Австралия", aus.Name);
            Assert.AreEqual("Australia", aus.EnglishName);
            Assert.AreEqual("Океания", aus.Location);
            Assert.AreEqual("Австралия и Новая Зеландия", aus.LocationPrecise);
        }
    }
}
