namespace MdlpApiClient.Tests
{
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter6 : UnitTestsBase
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
        public void Chapter6_01_1_RegisterAccountingSystem()
        {
            // имена УС должны быть уникальными, повторное создание выдает ошибку BadRequest (400)
            var ex = Assert.Throws<MdlpException>(() =>
            {
                // SystemID — это код субъекта обращения, которому 
                // будут принадлежать все созданные учетные системы
                var system = Client.RegisterAccountingSystem(SystemID, "TestSystem");
                Assert.IsNotNull(system);
                AssertRequired(system);

                // создалось вот такое:
                // "TestSystem" = {
                //   "client_secret": "b6dee6c6-bbfb-4a47-bddc-7f7929a3c17b",
                //   "client_id": "da21ba9a-f242-4f60-9519-99cd625d80e1",
                //   "account_system_id": "eb8e4564-d1d7-4c00-8fc8-5e834a649c43"
                // },
                // "TestSystem1" = {
                //   "client_secret": "312539bc-ea1a-43bb-8f4e-471167e3a70f",
                //   "client_id": "c2e0174c-f403-437f-8ea6-023c4e5e7112",
                //   "account_system_id": "b3963a8f-ce92-4f23-8c5c-585b013c4422"
                // },
                // "TestSystem2 = {
                //   "client_secret": "1e53f12c-ab32-4a3f-88ca-a99c710f19e5",
                //   "client_id": "5b6b505c-f44d-4187-a78e-4ad118035104",
                //   "account_system_id": "f01079ed-6516-41dd-b440-a95e19eed7a7"
                // }
            });
        }

        [Test]
        public void Chapter6_01_4_GetUserInfo()
        {
            // пример из документации: "5b5540c4-fbb0-4ad7-a038-c8222affab3f" — запись не найдена (404) 
            var user = Client.GetUserInfo(TestUserID);
            Assert.IsNotNull(user);

            AssertRequired(user);
            Assert.AreEqual("Юрий", user.FirstName);
            Assert.AreEqual("Гагарин", user.LastName);
            Assert.IsTrue(user.GroupNames.Contains("Test group 2f869fdc-2985-4730-a409-04dd73929df5"));
        }
    }
}
