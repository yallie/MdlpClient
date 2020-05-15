namespace MdlpApiClient.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter8 : UnitTestsClientBase
    {
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
                Sgtin = "04607028394287PQ28I2DHQDF1V",
                EmissionDateFrom = DateTime.Now.AddYears(-100),
                EmissionDateTo = DateTime.Now,
                LastTracingDateFrom = DateTime.Now.AddYears(-100),
                LastTracingDateTo = DateTime.Now,
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
            Assert.AreEqual("Гертикад®", sgtin.ProductSellingName);
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
            Assert.AreEqual("Гертикад®", sgtin.ProductSellingName);
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
            Assert.AreEqual("Гертикад®", sgtin.ProductSellingName);
            Assert.AreEqual("ЗАО БИОКАД", sgtin.RegistrationHolder);

            sgtin = sgtins.Entries[1];
            Assert.NotNull(sgtin);
            Assert.AreEqual("611700126101510000000001311", sgtin.Sgtin);
            Assert.AreEqual("Сертикан", sgtin.ProductName);
            Assert.AreEqual("Сертикан", sgtin.ProductSellingName);
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
                Sgtin = "061017000000000000000000006",
                ReleaseDateFrom = DateTime.Now.AddYears(-100),
                ReleaseDateTo = DateTime.Now,
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
            Assert.AreEqual("Калетра", sgtin.ProductSellingName);
            Assert.AreEqual("Калетра® таблетки покрытые пленочной оболочкой, 200 мг+50 мг", sgtin.FullProductName);
            Assert.AreEqual("ЭББВИ ДОЙЧЛАНД ГМБХ И КО. КГ", sgtin.RegistrationHolder);
        }

        [Test]
        public void Chapter8_03_6_GetSgtinsKktAwaitingWithdrawal()
        {
            var sgtins = Client.GetSgtinsKktAwaitingWithdrawal(new SgtinAwaitingWithdrawalFilter
            {
                Sgtin = "061017000000000000000000006",
                StartDate = DateTime.Now.AddYears(-100),
                EndDate = DateTime.Now,
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
                Sgtin = "061017000000000000000000006",
                StartDate = DateTime.Now.AddYears(-100),
                EndDate = DateTime.Now,
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
        public void Chapter8_04_3_GetSsccFullHierarchy()
        {
            // пример из документации не найден: 201902251235570000
            // пример из документации вызывает ошибку: NUEMOESSCC00000001
            // var ssccs = Client.GetSsccFullHierarchy("201902251235570000");
            var ssccs = Client.GetSsccFullHierarchy("201902251235570000");
            Assert.NotNull(ssccs);
            Assert.NotNull(ssccs.Up);
            Assert.NotNull(ssccs.Down);

            Assert.AreEqual(0, ssccs.Up.Length);
            Assert.AreEqual(0, ssccs.Down.Length);
            Assert.AreEqual(2, ssccs.ErrorCode);
            Assert.AreEqual("Запрашиваемые данные не найдены", ssccs.ErrorDescription);
        }

        [Test]
        public void Chapter8_05_1_GetCurrentMedProducts()
        {
            var medProducts = Client.GetCurrentMedProducts(new MedProductsFilter
            {
                Gtin = "04607028394287",
                RegistrationDateFrom = DateTime.Now.AddYears(-100),
                RegistrationDateTo = DateTime.Now
            }, 0, 1);

            Assert.NotNull(medProducts);
            Assert.NotNull(medProducts.Entries);
            Assert.AreEqual(1, medProducts.Entries.Length);

            var prod = medProducts.Entries[0];
            Assert.NotNull(prod);
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.ProductSellingName);
            Assert.AreEqual("ТРАСТУЗУМАБ", prod.ProductName);
            Assert.AreEqual("ЗАО БИОКАД", prod.RegistrationHolder);
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_05_2_GetCurrentMedProduct()
        {
            var prod = Client.GetCurrentMedProduct("04607028394287");
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.ProductSellingName);
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
            Assert.AreEqual("Гертикад®", prod.ProductSellingName);
            Assert.AreEqual("0", prod.ProductPack1Amount);
            Assert.AreEqual("ЛП-003403", prod.RegistrationNumber);
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test]
        public void Chapter8_05_4_GetPublicMedProduct()
        {
            var prod = Client.GetPublicMedProduct("04607028394287");
            Assert.AreEqual("04607028394287", prod.Gtin);
            Assert.AreEqual("Гертикад®", prod.ProductSellingName);
            Assert.AreEqual("0", prod.ProductPack1Amount);
            //Assert.AreEqual("ЛП-003403", prod.RegistrationNumber); // null почему-то
            Assert.AreEqual("150 мг", prod.ProductDosageName);
        }

        [Test, Explicit("Этот метод API всегда создает новые записи, даже когда возвращает ошибку")]
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

        [Test]
        public void Chapter8_06_2_GetForeignCounterparties()
        {
            var counterparties = Client.GetForeignCounterparties(new ForeignCounterpartyFilter
            {
                Inn = "56887455222583",
                RegistrationDateFrom = DateTime.Now.AddYears(-100),
                RegistrationDateTo = DateTime.Now,
            }, 0, 10);

            // похоже, метод возвращает все регистрации: и успешные, и неуспешные
            // и нет способа вернуть только успешные регистрации
            Assert.NotNull(counterparties);
            Assert.NotNull(counterparties.Entries);
            Assert.IsTrue(counterparties.Total > 1);
            Assert.IsTrue(counterparties.Entries.Length > 1);

            var cp = counterparties.Entries[0];
            Assert.AreEqual("56887455222583", cp.Inn);
            Assert.AreEqual("93026c45-f63f-4a93-8b87-8aec5e56b292", cp.SystemSubjectID);
            Assert.AreEqual("GE", cp.CountryCode);
        }

        [Test]
        public void Chapter8_07_1_AddTrustedPartners()
        {
            // повторное добавление партнера к доверенным не приводит к ошибке
            Assert.DoesNotThrow(() => Client.AddTrustedPartners(
                "93026c45-f63f-4a93-8b87-8aec5e56b292",
                "93026c45-f63f-4a93-8b87-8aec5e56b292",
                "93026c45-f63f-4a93-8b87-8aec5e56b292"
            ));

            // поиск по ИТИН иностранного контрагента не работает
            var ex = Assert.Throws<MdlpException>(() => Client.AddTrustedPartners("56887455222583"));
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        }

        [Test]
        public void Chapter8_07_2_DeleteTrustedPartners()
        {
            // удаление из списка доверенных партнера, которого там нет, не приводит к ошибке
            // повторное удаление партнера из списка доверенных тоже не приводит к ошибке
            Assert.DoesNotThrow(() => Client.DeleteTrustedPartners(
                "93026c45-f63f-4a93-8b87-8aec5e56b292",
                "93026c45-f63f-4a93-8b87-8aec5e56b292",
                "93026c45-f63f-4a93-8b87-8aec5e56b292"
            ));

            // поиск по ИТИН иностранного контрагента не работает
            var ex = Assert.Throws<MdlpException>(() => Client.DeleteTrustedPartners("56887455222583"));
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        }

        [Test]
        public void Chapter8_07_3_GetTrustedPartners()
        {
            // вернуть первые 10 партнеров
            var partners = Client.GetTrustedPartners(null, 0, 10);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.IsTrue(partners.Total > 1);
            Assert.IsTrue(partners.Entries.Length > 1);

            // поиск по коду — запросим заведомо отсутствующего
            partners = Client.GetTrustedPartners(new TrustedPartnerFilter
            {
                SystemSubjectID = "93026c45-f63f-4a93-8b87-8aec5e56b292".Replace("b292", "fade")
            }, 0, 1);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.AreEqual(0, partners.Total);
            Assert.AreEqual(0, partners.Entries.Length);
        }

        [Test]
        public void Chapter8_08_1_GetForeignPartners()
        {
            // вернуть первые 10 зарегистрированных иностранных контрагентов
            var partners = Client.GetForeignPartners(new PartnerFilter
            {
                StartDate = DateTime.Now.AddYears(-100),
                EndDate = DateTime.Now,
                OperationStartDate = DateTime.Now.AddYears(-100),
                OperationEndDate = DateTime.Now,
            }, 0, 10);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.IsTrue(partners.Total > 1);
            Assert.IsTrue(partners.Entries.Length > 1);

            // поиск по ИНН — запросим известного
            partners = Client.GetForeignPartners(new PartnerFilter
            {
                Inn = "123456789012345678911234567890123456789",

                // а по коду субъекта почему-то не находится:
                //SystemSubjectID = "1a7c2739-57d9-46da-9363-6272f8b6b55b"
            }, 0, 1);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.AreEqual(1, partners.Total);
            Assert.AreEqual(1, partners.Entries.Length);

            var partner = partners.Entries[0];
            Assert.NotNull(partner);
            Assert.AreEqual("ООО Бритиш Петрол Нашионал Фарма Интертеймент", partner.OrganizationName);
            Assert.AreEqual("1a7c2739-57d9-46da-9363-6272f8b6b55b", partner.SystemSubjectID);
            Assert.AreEqual("fd87213c-f51f-420f-8567-e2cc36213043", partner.ID);
            Assert.AreEqual(new DateTime(2017, 10, 26), partner.OperationDate.Date.Date);

            // поиск по коду — запросим заведомо отсутствующего
            partners = Client.GetForeignPartners(new PartnerFilter
            {
                SystemSubjectID = "1a7c2739-57d9-46da-9363-6272f8b6b55b".Replace("9363", "6393")
            }, 0, 1);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.AreEqual(0, partners.Total);
            Assert.AreEqual(0, partners.Entries.Length);
        }

        [Test]
        public void Chapter8_08_1_GetLocalPartners()
        {
            // вернуть первые 10 зарегистрированных местных контрагентов
            var partners = Client.GetLocalPartners(null, 0, 10);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.IsTrue(partners.Total > 1);
            Assert.IsTrue(partners.Entries.Length > 1);

            // поиск по ИНН и/или по коду субъекта — запросим известного
            partners = Client.GetLocalPartners(new PartnerFilter
            {
                Inn = "7735069192",
                SystemSubjectID = "d2ee5250-3e28-4e5c-896a-00b902e22555" // по коду субъекта тоже находится
            }, 0, 1);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.AreEqual(1, partners.Total);
            Assert.AreEqual(1, partners.Entries.Length);

            var partner = partners.Entries[0];
            Assert.NotNull(partner);
            Assert.AreEqual("ГБУЗ \"ГКБ ИМ. М.П. КОНЧАЛОВСКОГО ДЗМ\"", partner.OrganizationName);
            Assert.AreEqual("d2ee5250-3e28-4e5c-896a-00b902e22555", partner.SystemSubjectID);
            Assert.AreEqual("7735069192", partner.Inn);
            Assert.AreEqual(new DateTime(2017, 06, 02), partner.OperationDate.Date.Date);

            // поиск по коду — запросим заведомо отсутствующего
            partners = Client.GetLocalPartners(new PartnerFilter
            {
                SystemSubjectID = "d2ee5250-3e28-4e5c-896a-00b902e22555".Replace("22555", "55222")
            }, 0, 1);
            Assert.NotNull(partners);
            Assert.NotNull(partners.Entries);
            Assert.AreEqual(0, partners.Total);
            Assert.AreEqual(0, partners.Entries.Length);
        }

        [Test]
        public void Chapter8_09_1_GetCurrentMember()
        {
            var member = Client.GetCurrentMember();
            Assert.NotNull(member);
            Assert.AreEqual("9dedee17-e43a-47f1-910e-3a88ff6bc81b", member.SystemSubjectID);
            Assert.AreEqual("Аптечный1", member.OrganizationName);
            Assert.AreEqual("ru", member.Language);
            Assert.AreEqual("peresok+76@mail.ru", member.Email);
            Assert.IsTrue(member.IsResident);
            Assert.NotNull(member.Chiefs);
            Assert.NotNull(member.AgreementsInfo);
            Assert.AreEqual(RegEntityTypeEnum.RESIDENT, member.EntityType);
        }

        [Test]
        public void Chapter8_09_2_UpdateCurrentMember()
        {
            Client.UpdateCurrentMember(new MemberOptions
            {
                Language = "ru",
                Email = "peresok+76@mail.ru"
            });
        }

        [Test]
        public void Chapter8_09_3_GetCurrentBillingInfo()
        {
            var accounts = Client.GetCurrentBillingInfo();
            Assert.NotNull(accounts);
            Assert.AreEqual(0, accounts.Length);
        }

        [Test]
        public void Chapter8_10_1_GetEmissionDevices()
        {
            var devices = Client.GetEmissionDevices(new EmissionDeviceFilter
            {
                ProvisionStartDate = DateTime.Now.AddYears(-100),
                ProvisionEndDate = DateTime.Now,
            }, 0, 10);

            Assert.NotNull(devices);
            Assert.NotNull(devices.Entries);
            Assert.AreEqual(0, devices.Total);
            Assert.AreEqual(0, devices.Entries.Length);
            //Assert.IsTrue(devices.Total > 1);
            //Assert.IsTrue(devices.Entries.Length > 1);
        }

        [Test]
        public void Chapter8_10_2_GetWithdrawalDevices()
        {
            var devices = Client.GetWithdrawalDevices(new WithdrawalDeviceFilter
            {
                ProvisionStartDate = DateTime.Now.AddYears(-100),
                ProvisionEndDate = DateTime.Now,
            }, 0, 10);
            Assert.NotNull(devices);
            Assert.NotNull(devices.Entries);
            Assert.AreEqual(0, devices.Total);
            Assert.AreEqual(0, devices.Entries.Length);
        }

        [Test]
        public void Chapter8_11_1_GetVirtualStorage()
        {
            var balance = Client.GetVirtualStorage(new VirtualStorageFilter
            {
                // "00000000100930" returns error 500
                // "00000000000551" returns 0 entries
                // 00000000100946 00000000100964
                StorageID = "00000000100931",
                StartDate = DateTime.Now.AddYears(-100),
                EndDate = DateTime.Now,
            }, 0, 10);

            Assert.NotNull(balance);
            Assert.NotNull(balance.Entries);
            Assert.IsTrue(balance.Total > 0);
            Assert.IsTrue(balance.Entries.Length > 0);
            AssertRequiredItems(balance.Entries);
        }

        [Test]
        public void Chapter8_12_1_GetPausedCirculationDecisions()
        {
            var decisions = Client.GetPausedCirculationDecisions(new PausedCirculationDecisionFilter
            {
                Gtin = "04610020540019",
                StartHaltDate = DateTime.Now.AddYears(-100),
                EndHaltDate = DateTime.Now,
                StartHaltDocDate = DateTime.Now.AddYears(-100),
                EndHaltDocDate = DateTime.Now,
            }, 0, 10);
            Assert.NotNull(decisions);
            Assert.NotNull(decisions.Entries);
            Assert.AreEqual(1, decisions.Total);
            Assert.AreEqual(1, decisions.Entries.Length);

            AssertRequiredItems(decisions.Entries);
            var decision = decisions.Entries.Single();
            Assert.AreEqual("04610020540019", decision.Gtin);
            Assert.AreEqual("290734f1-9400-4a91-8a66-9e7f2dee55ab", decision.HaltID);
        }

        [Test]
        public void Chapter8_12_2_GetPausedCirculationSgtins()
        {
            var sgtins = Client.GetPausedCirculationSgtins("290734f1-9400-4a91-8a66-9e7f2dee55ab", 0, 10);
            Assert.NotNull(sgtins);
            Assert.NotNull(sgtins.Entries);
            Assert.AreEqual(2, sgtins.Total);
            Assert.AreEqual(2, sgtins.Entries.Length);
            AssertRequiredItems(sgtins.Entries);

            var list = string.Join(",", sgtins.Entries.Select(e => e.Sgtin));
            Assert.AreEqual("04610020540019SCHEME2600094,04610020540019SCHEME2600095", list);
        }
    }
}
