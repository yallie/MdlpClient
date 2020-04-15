namespace MdlpApiClient.Tests
{
    using System;
    using System.Net;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter8 : UnitTestsBase
    {
        private MdlpClient Client = new MdlpClient(new NonResidentCredentials
        {
            ClientID = ClientID1,
            ClientSecret = ClientSecret1,
            UserID = UserStarter1,
            Password = UserPassword1,
        })
        {
            Tracer = TestContext.Progress.WriteLine
        };

        private MdlpClient TestClient = new MdlpClient(new ResidentCredentials
        {
            ClientID = ClientID1,
            ClientSecret = ClientSecret1,
            UserID = TestUserID,
        })
        {
            Tracer = TestContext.Progress.WriteLine
        };

        [Test]
        public void GetBranches_8_1_2()
        {
            // пример из документации с кодом 00000000000464 — не находится
            Client.GetBranches(new BranchFilter
            {
                BranchID = "00000000100930", // "00000000000464",
                HouseGuid = "986f2934-be05-438f-a30e-c15b90e15dbc", // "3e311a10-3d0c-438e-a013-7c5fd3ea66a6",
                Status = 1,
                StartDate = new DateTime(2018, 12, 12), // 2019-11-01
                EndDate = new DateTime(2019, 1, 1), // 2019-12-01
            },
            startFrom: 0, count: 10);

            // попробуем найти хоть что-нибудь
            var branches = Client.GetBranches(null, startFrom: 0, count: 10);
            Assert.IsNotNull(branches);
            Assert.IsNotNull(branches.Entries);
            Assert.AreEqual(1, branches.Total);

            var branch = branches.Entries[0];
            Assert.NotNull(branch);
            Assert.AreEqual("Аптечный1", branch.OrgName);
            Assert.NotNull(branch.WorkList);
            Assert.AreEqual(1, branch.WorkList.Length);
            Assert.NotNull(branch.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", branch.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", branch.Address.AddressDescription);
        }

        [Test]
        public void GetBranch_8_1_3()
        {
            var branch = Client.GetBranch("00000000100930");
            Assert.NotNull(branch);
            Assert.NotNull(branch.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", branch.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", branch.Address.AddressDescription);
        }

        [Test]
        public void RegisterBranch_8_1_4()
        {
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var branchId = Client.RegisterBranch(new Address
                {
                    AoGuid = "00000000-0000-0000-0000-000000000000",
                    HouseGuid = "986f2934-be05-438f-a30e-c15b90e15dbc" // "3e311a10-3d0c-438e-a013-7c5fd3ea66a6"
                });

                Assert.NotNull(branchId);
            });

            // "Ошибка при выполнении операции: указанные данные уже зарегистрированы в системе"
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode); // 400
        }

        [Test]
        public void GetWarehouses_8_2_2()
        {
            // пример из документации с кодом 00000000000561 — не находится
            var whouses = Client.GetWarehouses(new WarehouseFilter
            {
                WarehouseID = "00000000100931", // "00000000000561"
                HouseGuid = "986f2934-be05-438f-a30e-c15b90e15dbc", // "3e311a10-3d0c-438e-a013-7c5fd3ea66a6", // "3e311a10-3d0c-438e-a013-7c5fd3ea66a6",
                Status = 1,
                StartDate = new DateTime(2018, 11, 1), // 2019-11-01
                EndDate = new DateTime(2019, 1, 1), // 2019-12-01
            },
            startFrom: 0, count: 10);

            Assert.IsNotNull(whouses);
            Assert.AreEqual(1, whouses.Entries.Length);

            // попробуем найти хоть что-нибудь
            whouses = Client.GetWarehouses(null, startFrom: 0, count: 10);
            Assert.IsNotNull(whouses);
            Assert.IsNotNull(whouses.Entries);
            Assert.AreEqual(3, whouses.Total);

            var whouse = whouses.Entries[0];
            Assert.NotNull(whouse);
            Assert.AreEqual("Аптечный1", whouse.OrgName);
            Assert.NotNull(whouse.WorkList);
            Assert.AreEqual(1, whouse.WorkList.Length);
            Assert.NotNull(whouse.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", whouse.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", whouse.Address.AddressDescription);
        }

        [Test]
        public void GetWarehouse_8_2_3()
        {
            var warehouse = Client.GetWarehouses("00000000100931");
            Assert.NotNull(warehouse);
            Assert.NotNull(warehouse.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", warehouse.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", warehouse.Address.AddressDescription);
        }

        [Test]
        public void GetSgtins_8_2_2()
        {
            // пример из документации с кодом 611700126101510000000001311 — не находится
            // если искать без фильтра — ищет прям долго
            // походу там в ИС МДЛП реально поднимаются все КИЗ, а уж потом отсекается лимит
            // поиск по фильтру Sgtin:  elapsed: 00:00:00.8176175
            // поиск без фильтра Sgtin: elapsed: 00:00:09.8758950
            var sgtins = Client.GetSgtins(new SgtinFilter
            {
                Sgtin = "04607028394287PQ28I2DHQDF1V"
            },
            startFrom: 0, count: 10);

            Assert.IsNotNull(sgtins);
            Assert.AreEqual(1, sgtins.Entries.Length);

            var sgtin = sgtins.Entries[0];
            Assert.NotNull(sgtin);
            Assert.AreEqual("Аптечный1", sgtin.Owner);
            Assert.AreEqual("77", sgtin.FederalSubjectCode);
            Assert.AreEqual("Москва", sgtin.FederalSubjectName);

            Assert.AreEqual("ТРАСТУЗУМАБ", sgtin.ProductName);
            Assert.AreEqual("Гертикад®", sgtin.SellingName);
            Assert.AreEqual("лиофилизат для приготовления концентрата для приготовления раствора для инфузий \"гертикад®\" 150 мг, 440 мг", sgtin.FullProductName);
            Assert.AreEqual("ЗАО БИОКАД", sgtin.RegistrationHolder);
        }

        [Test]
        public void GetSgtins_8_2_3()
        {
            var sgtins = Client.GetSgtins(new[]
            {
                "04607028394287PQ28I2DHQDF1V", // найдется
                "611700126101510000000001311" // не найдется
            });

            Assert.IsNotNull(sgtins);
            Assert.AreEqual(2, sgtins.Total);
            Assert.AreEqual(1, sgtins.Entries.Length);
            Assert.AreEqual(1, sgtins.Failed);
            Assert.AreEqual(1, sgtins.FailedEntries.Length);

            var sgtin = sgtins.Entries[0];
            Assert.NotNull(sgtin);
            Assert.AreEqual("04607028394287PQ28I2DHQDF1V", sgtin.SgtinValue);
            Assert.AreEqual("Аптечный1", sgtin.Owner);
            Assert.AreEqual("77", sgtin.FederalSubjectCode);
            Assert.AreEqual("Москва", sgtin.FederalSubjectName);

            Assert.AreEqual("ТРАСТУЗУМАБ", sgtin.ProductName);
            Assert.AreEqual("Гертикад®", sgtin.SellingName);
            Assert.AreEqual("лиофилизат для приготовления концентрата для приготовления раствора для инфузий \"гертикад®\" 150 мг, 440 мг", sgtin.FullProductName);
            Assert.AreEqual("ЗАО БИОКАД", sgtin.RegistrationHolder);

            var failed = sgtins.FailedEntries[0];
            Assert.AreEqual("611700126101510000000001311", failed.Sgtin);
            Assert.AreEqual(4, failed.ErrorCode);
            Assert.AreEqual("Запрашиваемые данные доступны только текущему владельцу или контрагенту по операции", failed.ErrorDescription);
        }

        [Test]
        public void GetPublicSgtins_8_2_3()
        {
            var sgtins = Client.GetPublicSgtins(new[]
            {
                "04607028394287PQ28I2DHQDF1V", // найдется
                "611700126101510000000001311", // найдется
                "61170012610151000000000131V" // не найдется
            });

            Assert.IsNotNull(sgtins);
            Assert.AreEqual(3, sgtins.Total);
            Assert.AreEqual(2, sgtins.Entries.Length);
            Assert.AreEqual(1, sgtins.Failed);
            Assert.AreEqual(1, sgtins.FailedEntries.Length);

            var sgtin = sgtins.Entries[0];
            Assert.NotNull(sgtin);
            Assert.AreEqual("04607028394287PQ28I2DHQDF1V", sgtin.Sgtin);
            Assert.AreEqual("ТРАСТУЗУМАБ", sgtin.ProductName);
            Assert.AreEqual("Гертикад®", sgtin.SellingName);
            Assert.AreEqual("ЗАО БИОКАД", sgtin.RegistrationHolder);

            sgtin = sgtins.Entries[1];
            Assert.NotNull(sgtin);
            Assert.AreEqual("611700126101510000000001311", sgtin.Sgtin);
            Assert.AreEqual("Сертикан", sgtin.ProductName);
            Assert.AreEqual("Сертикан", sgtin.SellingName);
            Assert.AreEqual("ACG", sgtin.RegistrationHolder);

            var failed = sgtins.FailedEntries[0];
            Assert.AreEqual("61170012610151000000000131V", failed);
        }
    }
}
