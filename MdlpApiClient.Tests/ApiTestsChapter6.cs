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
        public void Chapter6_01_2_RegisterUser()
        {
            // зарегистрировать можно только один раз
            var ex = Assert.Throws<MdlpException>(() =>
            {
                // Регистрация пользователя-резидента:
                // 1. сертификат надо подготовить заранее
                // 2. выпустить тестовый сертификат-УКЭП можно тут:
                // https://www.cryptopro.ru/ui/Register/RegGetSubject.asp
                // 3. в браузере должен быть установлен плагин КриптоПро ЭЦП
                // 4. ФИО в сертификате и в пользователе должны совпадать
                // 5. для нерезидентов (авторизация по паролю) работает только по http, 
                // для резидентов (авторизация сертификатом) — только по https
                var userId = Client.RegisterUser(SystemID, new ResidentUser
                {
                    PublicCertificate = "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",
                    FirstName = "Юрий",
                    LastName = "Гагарин",
                    Email = "asd@asd.com",
                });

                Assert.IsNotNull(userId); // "31736b85-45d8-4fb0-8130-f0dabce5d491"
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.AreEqual("Ошибка при выполнении операции: пользователь с данным email уже зарегистрирован", ex.Message);
        }

        [Test]
        public void Chapter6_01_3_RegisterUser()
        {
            // зарегистрировать можно только один раз
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var userId = Client.RegisterUser(SystemID, new NonResidentUser
                {
                    FirstName = "Neil",
                    LastName = "Armstrong",
                    Email = "asd1@asd.com",
                    Password = "password"
                });

                Assert.IsNotNull(userId);
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.AreEqual("Ошибка при выполнении операции: попытка зарегистрировать резидента по идентификатору sys_id для нерезидента или наоборот", ex.Message);
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

        [Test]
        public void Chapter6_01_5_GetCurrentUserLanguage()
        {
            var language = Client.GetCurrentUserLanguage();
            Assert.AreEqual("ru", language);
        }

        [Test]
        public void Chapter6_01_6_UpdateUserProfile()
        {
            // Хм, при регистрации ИС проверяет, чтобы ФИО совпадали с сертификатом,
            // а после регистрации — уже не проверяет
            Client.UpdateUserProfile(TestUserID, new UserEditProfileEntry
            {
                FirstName = "Юрий",
                LastName = "Никулин",
                Position = "Артист"
            });

            // действительно, обновляется
            var user = Client.GetUserInfo(TestUserID);
            Assert.AreEqual("Никулин", user.LastName);

            // вернем все на место
            Client.UpdateUserProfile(TestUserID, new UserEditProfileEntry
            {
                FirstName = "Юрий",
                LastName = "Гагарин",
                Position = "Космонавт"
            });
        }

        [Test]
        public void Chapter6_01_7_GetCurrentUserInfo()
        {
            var user = Client.GetCurrentUserInfo();
            Assert.IsNotNull(user);

            AssertRequired(user);
            Assert.AreEqual("Альберт", user.FirstName);
            Assert.AreEqual("Данилов", user.LastName);
            Assert.IsTrue(user.GroupNames.Contains("Системная группа"));
        }

        [Test]
        public void Chapter6_01_8_SetCurrentUserLanguage()
        {
            var lang = Client.GetCurrentUserLanguage();
            try
            {
                Client.SetCurrentUserLanguage("ru");
                Client.SetCurrentUserLanguage("en");

                // неизвестный язык
                var ex = Assert.Throws<MdlpException>(() => Client.SetCurrentUserLanguage("bad"));
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
                Assert.AreEqual("Error during operation: request is missing or incorrect", ex.Message);
            }
            finally
            {
                // вернем как было
                Client.SetCurrentUserLanguage(lang);
            }
        }

        [Test]
        public void Chapter6_01_9_GetCurrentCertificates()
        {
            // non-resident user doesn't have certificates
            var certs = Client.GetCurrentCertificates(0, 10);
            Assert.IsNotNull(certs);
            Assert.IsNotNull(certs.Certificates);
            Assert.AreEqual(0, certs.Total);
            Assert.AreEqual(0, certs.Certificates.Length);

            // resident user does not have access rights?
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var client = new MdlpClient(credentials: new ResidentCredentials
                {
                    ClientID = ClientID1,
                    ClientSecret = ClientSecret1,
                    UserID = TestUserThumbprint,
                })
                {
                    Tracer = TestContext.Progress.WriteLine
                };

                certs = client.GetCurrentCertificates(0, 10);
                Assert.IsNotNull(certs);
                Assert.IsNotNull(certs.Certificates);
                Assert.AreEqual(1, certs.Total);
                Assert.AreEqual(1, certs.Certificates.Length);

                var cert = certs.Certificates[0];
                Assert.AreEqual(TestCertificateThumbprint, cert.PublicCertificateThumbprint);
                AssertRequired(cert);
            });

            Assert.AreEqual(HttpStatusCode.Forbidden, ex.StatusCode);
        }
    }
}
