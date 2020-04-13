namespace MdlpApiClient.Tests
{
    using System;
    using System.Net;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter8 : UnitTestsBase
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
    }
}
