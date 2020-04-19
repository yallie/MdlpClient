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
        public void Chapter8_01_2_GetBranches()
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
        public void Chapter8_01_3_GetBranch()
        {
            var branch = Client.GetBranch("00000000100930");
            Assert.NotNull(branch);
            Assert.NotNull(branch.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", branch.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", branch.Address.AddressDescription);
        }

        [Test]
        public void Chapter8_01_4_RegisterBranch()
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
        public void Chapter8_02_2_GetWarehouses()
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
        public void Chapter8_02_3_GetWarehouse()
        {
            var warehouse = Client.GetWarehouses("00000000100931");
            Assert.NotNull(warehouse);
            Assert.NotNull(warehouse.Address);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", warehouse.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, Дом 9/26, Строение 3", warehouse.Address.AddressDescription);
        }

        [Test]
        public void Chapter8_02_4_RegisterWarehouse()
        {
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var inn = "7720672100";
                var warehouseId = Client.RegisterWarehouse(inn, new Address
                {
                    AoGuid = "00000000-0000-0000-0000-000000000000",
                    HouseGuid = "986f2934-be05-438f-a30e-c15b90e15dbc" // "3e311a10-3d0c-438e-a013-7c5fd3ea66a6"
                });

                Assert.NotNull(warehouseId);
            });

            // "Ошибка при выполнении операции: указанные данные не могут быть идентифицированы (не зарегистрированы)"
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode); // 400
        }

        [Test]
        public void Chapter8_02_5_GetAvailableAddresses()
        {
            var addresses = Client.GetAvailableAddresses("7720672100");
            Assert.NotNull(addresses);
            Assert.AreEqual(1, addresses.Total);
            Assert.NotNull(addresses.Entries);
            Assert.AreEqual(1, addresses.Entries.Length);

            var address = addresses.Entries[0];
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", address.AddressID);
            Assert.AreEqual("986f2934-be05-438f-a30e-c15b90e15dbc", address.Address.HouseGuid);
            Assert.AreEqual("г Москва, ул Щипок, д. 9/26 стр. 3", address.ResolvedAddress);
        }

        [Test]
        public void Chapter8_03_1_GetSgtins()
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
        public void Chapter8_03_2_GetSgtins()
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
        public void Chapter8_03_3_GetPublicSgtins()
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

        [Test]
        public void Chapter8_03_4_GetSgtin()
        {
            // пример из документации 046065560030TRACKING0000000 приводит к ошибке 400 BadRequest
            var info = Client.GetSgtin("04607028394287PQ28I2DHQDF1V");
            Assert.NotNull(info);
            Assert.NotNull(info.SgtinInfo);
            Assert.NotNull(info.GtinInfo);

            Assert.AreEqual("04607028394287PQ28I2DHQDF1V", info.SgtinInfo.SgtinValue);
            Assert.AreEqual("04607028394287", info.SgtinInfo.Gtin);
            Assert.AreEqual("marked", info.SgtinInfo.Status);
            Assert.AreEqual(new DateTime(2018, 12, 20, 11, 31, 21), info.SgtinInfo.StatusDate);
            Assert.AreEqual("0007770000", info.SgtinInfo.BatchNumber);
            Assert.AreEqual(true, info.SgtinInfo.Gnvlp);

            Assert.AreEqual("ФЛАКОН", info.GtinInfo.TypeForm);
            Assert.AreEqual("ФЛАКОН", info.GtinInfo.ProductPack1Name);
            Assert.AreEqual("КАРТОННАЯ ПАЧКА", info.GtinInfo.ProductPack2Name);
            Assert.AreEqual("1", info.GtinInfo.ProductPack1InPack2);
        }

        [Test]
        public void Chapter8_03_5_GetSgtinsOnHold()
        {
            // поискал с фильтром null, чтобы найти хоть какой-нибудь
            var sgtins = Client.GetSgtinsOnHold(new SgtinOnHoldFilter
            {
                Sgtin = "061017000000000000000000006"
            }, 0, 1);

            Assert.IsNotNull(sgtins);
            Assert.AreEqual(1, sgtins.Total);
            Assert.AreEqual(1, sgtins.Entries.Length);

            var sgtin = sgtins.Entries[0];
            Assert.NotNull(sgtin);

            Assert.AreEqual("061017000000000000000000006", sgtin.SgtinValue);
            Assert.AreEqual("ООО \"СЕВАСТОПОЛЬСКАЯ ГЕО-ПАРТИЯ\"", sgtin.Owner);
            Assert.AreEqual("78", sgtin.FederalSubjectCode);
            Assert.AreEqual("Санкт-Петербург", sgtin.FederalSubjectName);

            Assert.AreEqual("ЛОПИНАВИР+РИТОНАВИР", sgtin.ProductName);
            Assert.AreEqual("Калетра", sgtin.SellingName);
            Assert.AreEqual("Калетра® таблетки покрытые пленочной оболочкой, 200 мг+50 мг", sgtin.FullProductName);
            Assert.AreEqual("ЭББВИ ДОЙЧЛАНД ГМБХ И КО. КГ", sgtin.RegistrationHolder);
        }

        [Test]
        public void Chapter8_03_6_GetSgtinsKktAwaitingWithdrawal()
        {
            var sgtins = Client.GetSgtinsKktAwaitingWithdrawal(new SgtinAwaitingWithdrawalFilter
            {
                Sgtin = "061017000000000000000000006"
            }, 0, 1);
            Assert.NotNull(sgtins);

            Assert.AreEqual(0, sgtins.Total);
            Assert.NotNull(sgtins.Entries);
            Assert.AreEqual(0, sgtins.Entries.Length);
        }

        [Test]
        public void Chapter8_03_7_GetSgtinsDeviceAwaitingWithdrawal()
        {
            var sgtins = Client.GetSgtinsDeviceAwaitingWithdrawal(new SgtinAwaitingWithdrawalFilter
            {
                Sgtin = "061017000000000000000000006"
            }, 0, 1);
            Assert.NotNull(sgtins);

            Assert.AreEqual(0, sgtins.Total);
            Assert.NotNull(sgtins.Entries);
            Assert.AreEqual(0, sgtins.Entries.Length);
        }

        [Test]
        public void Chapter8_04_1_GetSsccHierarchy()
        {
            // пример из документации не найден: 201902251235570000
            // пример из документации вызывает ошибку: NUEMOESSCC00000001
            var ssccs = Client.GetSsccHierarchy("201902251235570000");
            Assert.NotNull(ssccs);
            Assert.NotNull(ssccs.Up);
            Assert.NotNull(ssccs.Down);

            Assert.AreEqual(0, ssccs.Up.Length);
            Assert.AreEqual(0, ssccs.Down.Length);
            Assert.AreEqual(2, ssccs.ErrorCode);
            Assert.AreEqual("Запрашиваемые данные не найдены", ssccs.ErrorDescription);
        }

        [Test]
        public void Chapter8_04_2_GetSsccSgtins()
        {
            // пример из документации не найден: 201902251235570000
            // пример из документации вызывает ошибку: NUEMOESSCC00000001
            var ssccs = Client.GetSsccSgtins("201902251235570000", null, 0, 1);
            Assert.NotNull(ssccs);
            Assert.NotNull(ssccs.Entries);

            Assert.AreEqual(0, ssccs.Entries.Length);
            Assert.AreEqual(2, ssccs.ErrorCode);
            Assert.AreEqual("Запрашиваемые данные не найдены", ssccs.ErrorDescription);
        }

        [Test]
        public void Chapter8_05_1_GetCurrentMedProducts()
        {
            var medProducts = Client.GetCurrentMedProducts(new MedProductsFilter
            {
                Gtin = "04607028394287"
            }, 0, 1);

            Assert.NotNull(medProducts);
            Assert.NotNull(medProducts.Entries);
            Assert.AreEqual(1, medProducts.Entries.Length);

            var prod = medProducts.Entries[0];
            Assert.NotNull(prod);
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.SellingName);
            Assert.AreEqual("ТРАСТУЗУМАБ", prod.ProductName);
            Assert.AreEqual("ЗАО БИОКАД", prod.RegistrationHolder);
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_05_2_GetCurrentMedProduct()
        {
            var prod = Client.GetCurrentMedProduct("04607028394287");
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.SellingName);
            Assert.AreEqual("ТРАСТУЗУМАБ", prod.ProductName);
            Assert.AreEqual("ЗАО БИОКАД", prod.RegistrationHolder);
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_05_3_GetPublicMedProducts()
        {
            var medProducts = Client.GetPublicMedProducts(new MedProductsFilter
            {
                Gtin = "04607028394287"
            }, 0, 1);

            Assert.NotNull(medProducts);
            Assert.NotNull(medProducts.Entries);
            Assert.AreEqual(1, medProducts.Entries.Length);

            var prod = medProducts.Entries[0];
            Assert.NotNull(prod);
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.SellingName);
            Assert.AreEqual("0", prod.ProductPack1Amount);
            Assert.AreEqual("ЛП-003403", prod.RegistrationNumber);
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_05_4_GetPublicMedProduct()
        {
            var prod = Client.GetPublicMedProduct("04607028394287");
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.SellingName);
            Assert.AreEqual("0", prod.ProductPack1Amount);
            //Assert.AreEqual("ЛП-003403", prod.RegistrationNumber); // null почему-то
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_06_1_RegisterForeignCounterparty()
        {
            var ex = Assert.Throws<MdlpException>(() =>
            {
                // этот тест можно выполнить только один раз с указанными данными
                var partyId = Client.RegisterForeignCounterparty(
                    "56887455222583",
                    "ГМ ПХАРМАЦЕУТИЦАЛС",
                    new ForeignAddress
                    {
                        City = "city",
                        Region = "region",
                        Locality = "locality",
                        Street = "street",
                        House = "house",
                        Corpus = "corpus",
                        Litera = "litera",
                        Room = "room",
                        CountryCode = "GE",
                        PostalCode = "148000",
                    });

                // "56887455222583" зарегистрирован с кодом "93026c45-f63f-4a93-8b87-8aec5e56b292"
                Assert.NotNull(partyId);
            });

            // "Ошибка при выполнении операции: указанные данные уже зарегистрированы в системе"
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode); // 400
        }
    }
}
