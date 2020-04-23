namespace MdlpApiClient.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter6 : UnitTestsClientBase
    {
        [Test]
        public void Chapter6_01_01_RegisterAccountSystem()
        {
            // имена УС должны быть уникальными, повторное создание выдает ошибку BadRequest (400)
            var ex = Assert.Throws<MdlpException>(() =>
            {
                // SystemID — это код субъекта обращения, которому 
                // будут принадлежать все созданные учетные системы
                var system = Client.RegisterAccountSystem(SystemID, "TestSystem");
                Assert.IsNotNull(system);
                Assert.IsNotNull(system.ClientSecret);
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
        public void Chapter6_01_02_RegisterUser()
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
                    PublicCertificate = "MIIIdTCCCCSgAwIBAgIKJ9/JVAAEAAN9cTAIBgYqhQMCAgMwggFIMRgwFgYFKoUDZAESDTEwMzc3MDAwODU0NDQxGjAYBggqhQMDgQMBARIMMDA3NzE3MTA3OTkxMTkwNwYDVQQJHjAEQwQ7AC4AIAQhBEMESQRRBDIEQQQ6BDgEOQAgBDIEMAQ7ACwAIAQ0AC4AIAAxADgxITAfBgNVBAgeGAA3ADcAIAQzAC4AIAQcBD4EQQQ6BDIEMDEVMBMGA1UEBx4MBBwEPgRBBDoEMgQwMSAwHgYJKoZIhvcNAQkBFhFpbmZvQGNyeXB0b3Byby5ydTELMAkGA1UEBhMCUlUxKTAnBgNVBAoeIAQeBB4EHgAgACIEGgQgBBgEHwQiBB4ALQQfBCAEHgAiMUEwPwYDVQQDHjgEIgQ1BEEEQgQ+BDIESwQ5ACAEIwQmACAEHgQeBB4AIAAiBBoEIAQYBB8EIgQeAC0EHwQgBB4AIjAeFw0yMDAzMzEyMjEwMDBaFw0yMDA2MzAyMjIwMDBaMIIBtDEYMBYGBSqFA2QBEg0xMjM0NTY3ODkwMTIzMRowGAYIKoUDA4EDAQESDDEyMzQ1Njc4OTAxMjEaMBgGCSqGSIb3DQEJARYLYXNkQGFzZC5jb20xCzAJBgNVBAYTAlJVMRcwFQYDVQQIDA7QntCx0LvQsNGB0YLRjDEVMBMGA1UEBwwM0JzQvtGB0LrQstCwMR8wHQYDVQQKDBbQntGA0LPQsNC90LjQt9Cw0YbQuNGPMSMwIQYDVQQLDBrQn9C+0LTRgNCw0LfQtNC10LvQtdC90LjQtTFCMEAGA1UEAww50KLQtdGB0YLQvtCy0YvQuSDQo9Ca0K3QnyDQuNC8LiDQrtGA0LjRjyDQk9Cw0LPQsNGA0LjQvdCwMRUwEwYDVQQJDAzQnNC+0YHQutCy0LAxOTA3BgkqhkiG9w0BCQIMKtCa0L7RgdC80L7QvdCw0LLRgiDQrtGA0LjQuSDQk9Cw0LPQsNGA0LjQvTEbMBkGA1UEDAwS0JrQvtGB0LzQvtC90LDQstGCMREwDwYDVQQqDAjQrtGA0LjQuTEXMBUGA1UEBAwO0JPQsNCz0LDRgNC40L0wZjAfBggqhQMHAQEBATATBgcqhQMCAiQABggqhQMHAQECAgNDAARAhX7GCDR2aDYD7tB0sHS2rvJ7egzdGD8+DebOnKJe7jXkxrcG26cINDUWLnn8wlns6Hx7rhn56LHK03qv2nvTJ6OCBHkwggR1MA4GA1UdDwEB/wQEAwIE8DAmBgNVHSUEHzAdBggrBgEFBQcDBAYHKoUDAgIiBgYIKwYBBQUHAwIwHQYDVR0OBBYEFHeK1wKTd2R6VBFvl9IB73SVlG46MIIBiQYDVR0jBIIBgDCCAXyAFHplou1Prm4wEO7EA8tb2lbE2uSxoYIBUKSCAUwwggFIMRgwFgYFKoUDZAESDTEwMzc3MDAwODU0NDQxGjAYBggqhQMDgQMBARIMMDA3NzE3MTA3OTkxMTkwNwYDVQQJHjAEQwQ7AC4AIAQhBEMESQRRBDIEQQQ6BDgEOQAgBDIEMAQ7ACwAIAQ0AC4AIAAxADgxITAfBgNVBAgeGAA3ADcAIAQzAC4AIAQcBD4EQQQ6BDIEMDEVMBMGA1UEBx4MBBwEPgRBBDoEMgQwMSAwHgYJKoZIhvcNAQkBFhFpbmZvQGNyeXB0b3Byby5ydTELMAkGA1UEBhMCUlUxKTAnBgNVBAoeIAQeBB4EHgAgACIEGgQgBBgEHwQiBB4ALQQfBCAEHgAiMUEwPwYDVQQDHjgEIgQ1BEEEQgQ+BDIESwQ5ACAEIwQmACAEHgQeBB4AIAAiBBoEIAQYBB8EIgQeAC0EHwQgBB4AIoIQTpjz80+VRJ1NixxSrES8JzBcBgNVHR8EVTBTMFGgT6BNhktodHRwOi8vd3d3LmNyeXB0b3Byby5ydS9yYS9jZHAvN2E2NWEyZWQ0ZmFlNmUzMDEwZWVjNDAzY2I1YmRhNTZjNGRhZTRiMS5jcmwweAYIKwYBBQUHAQEEbDBqMDQGCCsGAQUFBzABhihodHRwOi8vd3d3LmNyeXB0b3Byby5ydS9vY3NwbmMyL29jc3Auc3JmMDIGCCsGAQUFBzABhiZodHRwOi8vd3d3LmNyeXB0b3Byby5ydS9vY3NwMi9vY3NwLnNyZjArBgNVHRAEJDAigA8yMDIwMDMzMTIyMTAwMFqBDzIwMjAwNjMwMjIxMDAwWjAdBgNVHSAEFjAUMAgGBiqFA2RxATAIBgYqhQNkcQIwNAYFKoUDZG8EKwwp0JrRgNC40L/RgtC+0J/RgNC+IENTUCAo0LLQtdGA0YHQuNGPIDMuNikwggEzBgUqhQNkcASCASgwggEkDCsi0JrRgNC40L/RgtC+0J/RgNC+IENTUCIgKNCy0LXRgNGB0LjRjyAzLjYpDFMi0KPQtNC+0YHRgtC+0LLQtdGA0Y/RjtGJ0LjQuSDRhtC10L3RgtGAICLQmtGA0LjQv9GC0L7Qn9GA0L4g0KPQpiIg0LLQtdGA0YHQuNC4IDEuNQxP0KHQtdGA0YLQuNGE0LjQutCw0YIg0YHQvtC+0YLQstC10YLRgdGC0LLQuNGPIOKEliDQodCkLzEyNC0yNzM4INC+0YIgMDEuMDcuMjAxNQxP0KHQtdGA0YLQuNGE0LjQutCw0YIg0YHQvtC+0YLQstC10YLRgdGC0LLQuNGPIOKEliDQodCkLzEyOC0yNzY4INC+0YIgMzEuMTIuMjAxNTAIBgYqhQMCAgMDQQDyX5hVIdkCFKWT6hWPFJt1sDYU/pwcX6xjLPb2p5m7auOTH0rPqgovyoIt6wVs+bzFjC4WYDP+Ly3UUF2FC/zy",
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
        public void Chapter6_01_03_RegisterUser()
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
        public void Chapter6_01_04_GetUserInfo()
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
        public void Chapter6_01_05_GetCurrentLanguage()
        {
            var language = Client.GetCurrentLanguage();
            Assert.AreEqual("ru", language);
        }

        [Test]
        public void Chapter6_01_06_UpdateUserProfile()
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
        public void Chapter6_01_07_GetCurrentUserInfo()
        {
            var user = Client.GetCurrentUserInfo();
            Assert.IsNotNull(user);

            AssertRequired(user);
            Assert.AreEqual("Альберт", user.FirstName);
            Assert.AreEqual("Данилов", user.LastName);
            Assert.IsTrue(user.GroupNames.Contains("Системная группа"));
        }

        [Test]
        public void Chapter6_01_08_SetCurrentLanguage()
        {
            var lang = Client.GetCurrentLanguage();
            try
            {
                Client.SetCurrentLanguage("ru");
                Client.SetCurrentLanguage("en");

                // неизвестный язык
                var ex = Assert.Throws<MdlpException>(() => Client.SetCurrentLanguage("bad"));
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
                Assert.AreEqual("Error during operation: request is missing or incorrect", ex.Message);
            }
            finally
            {
                // вернем как было
                Client.SetCurrentLanguage(lang);
            }
        }

        [Test]
        public void Chapter6_01_09_GetCurrentCertificates()
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
                    Tracer = WriteLine
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

        [Test]
        public void Chapter6_01_10_GetUserCertificates()
        {
            // non-resident user doesn't have certificates
            var certs = Client.GetUserCertificates(TestUserID, 0, 10);
            Assert.IsNotNull(certs);
            Assert.IsNotNull(certs.Certificates);
            Assert.AreEqual(1, certs.Total);
            Assert.AreEqual(1, certs.Certificates.Length);

            var cert = certs.Certificates[0];
            Assert.AreEqual(TestCertificateThumbprint, cert.PublicCertificateThumbprint);
            AssertRequired(cert);
        }

        [Test]
        public void Chapter6_01_11_GetAccountSystem()
        {
            var accSystem = Client.GetAccountSystem("eb8e4564-d1d7-4c00-8fc8-5e834a649c43");
            Assert.IsNull(accSystem.ClientSecret);
            AssertRequired(accSystem);
            Assert.AreEqual("TestSystem", accSystem.Name);

            // other known account systems
            Assert.AreEqual("TestSystem1", Client.GetAccountSystem("b3963a8f-ce92-4f23-8c5c-585b013c4422").Name);
            Assert.AreEqual("TestSystem2", Client.GetAccountSystem("f01079ed-6516-41dd-b440-a95e19eed7a7").Name);
        }

        [Test]
        public void Chapter6_03_01_DeleteUser()
        {
            // user not found
            var ex = Assert.Throws<MdlpException>(() => Client.DeleteUser("5b5540c4-fbb0-4ad7-a038-c8222affab3f"));
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
            Assert.AreEqual("Ошибка при выполнении операции: запись не найдена", ex.Message);
        }

        [Test]
        public void Chapter6_03_02_DeleteAccountSystem()
        {
            // register and then delete
            var system = Client.RegisterAccountSystem(SystemID, "TestAccountSystem123");
            Client.DeleteAccountSystem(system.AccountSystemID);
        }

        [Test]
        public void Chapter6_04_01_AddUserCertificate()
        {
            var ex = Assert.Throws<MdlpException>(() => Client.AddUserCertificate(TestUserID, @"MIIBjjCCAT2gAwIBAgIEWWJzHzAIBgYqhQMCAgMwMTELMAkGA1UEBhMCUlUxEjAQBgNVBAoMCUNyeXB0b1BybzEOMAwGA1UEAwwFQWxpYXMwHhcNMTcxMTEzMTczMjI4WhcNMTgxMTEzMTczMjI4WjAxMQswCQYDVQQGEwJSVTESMBAGA1UECgwJQ3J5cHRvUHJvMQ4wDAYDVQQDDAVBbGlhczBjMBwGBiqFAwICEzASBgcqhQMCAiQABgcqhQMCAh4BA0MABEAIWARzAiI81k4i4Gz8EC7Ic01653JX5PCUfvgCBTpLduYtbTwLOwmGFcZzw9bwsxQpALqhcdRHxtx1UEeNKJuMozswOTAOBgNVHQ8BAf8EBAMCA+gwEwYDVR0lBAwwCgYIKwYBBQUHAwIwEgYDVR0TAQH/BAgwBgEB/wIBBTAIBgYqhQMCAgMDQQBL9CrIk0EgnMVr1J5dKbfXVFrhJxGxztFkTdmGkGJ6gHywB5Y9KpP67pv7I2bP1m1ej9hu+C17GSJrWgMgq+UZ"));
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.AreEqual("Ошибка при выполнении операции: срок действия сертификата истек", ex.Message);
        }

        [Test]
        public void Chapter6_04_02_DeleteUserCertificate()
        {
            var ex = Assert.Throws<MdlpException>(() => Client.DeleteUserCertificate(TestUserID, @"MIIBjjCCAT2gAwIBAgIEWWJzHzAIBgYqhQMCAgMwMTELMAkGA1UEBhMCUlUxEjAQBgNVBAoMCUNyeXB0b1BybzEOMAwGA1UEAwwFQWxpYXMwHhcNMTcxMTEzMTczMjI4WhcNMTgxMTEzMTczMjI4WjAxMQswCQYDVQQGEwJSVTESMBAGA1UECgwJQ3J5cHRvUHJvMQ4wDAYDVQQDDAVBbGlhczBjMBwGBiqFAwICEzASBgcqhQMCAiQABgcqhQMCAh4BA0MABEAIWARzAiI81k4i4Gz8EC7Ic01653JX5PCUfvgCBTpLduYtbTwLOwmGFcZzw9bwsxQpALqhcdRHxtx1UEeNKJuMozswOTAOBgNVHQ8BAf8EBAMCA+gwEwYDVR0lBAwwCgYIKwYBBQUHAwIwEgYDVR0TAQH/BAgwBgEB/wIBBTAIBgYqhQMCAgMDQQBL9CrIk0EgnMVr1J5dKbfXVFrhJxGxztFkTdmGkGJ6gHywB5Y9KpP67pv7I2bP1m1ej9hu+C17GSJrWgMgq+UZ"));
            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.AreEqual("Ошибка при выполнении операции: сертификат принадлежит другому пользователю", ex.Message);
        }

        [Test]
        public void Chapter6_05_01_ChangeUserPassword()
        {
            var ex = Assert.Throws<MdlpException>(() => Client.ChangeUserPassword(TestUserID, @"password"));
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, ex.StatusCode);
            Assert.AreEqual("MethodNotAllowed", ex.Message);
        }

        [Test]
        public void Chapter6_06_01_GetRights()
        {
            var rights = Client.GetRights();
            AssertRequiredItems(rights);

            // generate RightsEnum.cs
            var enumMemberQuery =
                from r in rights
                let name = r.Right
                let words = r.Description.Split(' ').Select(r => r.Trim())
                let descr = string.Join(" ", words.Where(w => w.Any()))
                orderby name
                let items = new[]
                {
                    "/// <summary>",
                    "/// " + descr,
                    "/// </summary>",
                    "public const string " + name + " = \"" + name + "\";",
                    string.Empty
                }
                let member = string.Join(Environment.NewLine, items)
                select member;

            // display the generated code:
            // WriteLine(string.Join(Environment.NewLine, enumMemberQuery));
        }

        [Test]
        public void Chapter6_06_02_GetCurrentRights()
        {
            var rights = Client.GetCurrentRights();
            AssertRequiredItems(rights);
        }

        [Test]
        public void Chapter6_06_03456789_CreateUpdateDeleteRightsGroup()
        {
            // extract all public constants from RightsEnum
            var rightsQuery =
                from constant in typeof(RightsEnum).GetFields()
                let value = constant.GetValue(null) as string
                where value != null
                orderby value
                select value;

            // create group (unique name is required)
            var rights = rightsQuery.ToArray();
            var groupName = "TestGroup " + Guid.NewGuid();
            var groupId = Client.CreateRightsGroup(groupName, rights);
            Assert.NotNull(groupId);

            // get group properties
            var group = Client.GetRightsGroup(groupId);
            AssertRequired(group);
            Assert.AreEqual(groupId, group.GroupID);
            Assert.AreEqual(groupName, group.GroupName);

            // compare the list of rights
            var actualRights = string.Join(":", group.Rights.OrderBy(r => r));
            var expectedRights = string.Join(":", rights);
            Assert.AreEqual(expectedRights, actualRights);

            // update group (note: GroupID property is ignored)
            group.GroupName += " Updated";
            group.Rights = group.Rights.Take(10).ToArray();
            Client.UpdateRightsGroup(groupId, group);

            // make sure the group is empty
            var users = Client.GetGroupUsers(groupId);
            Assert.NotNull(users);
            Assert.IsFalse(users.Any());

            // add user to the rights group
            Client.AddUserToRightsGroup(TestUserID, groupId);

            // make sure the group is not empty
            users = Client.GetGroupUsers(groupId);
            Assert.NotNull(users);
            Assert.AreEqual(1, users.Length);
            Assert.AreEqual(TestUserID, users[0].UserID);

            // delete user from the rights group
            Client.DeleteUserFromRightsGroup(TestUserID, groupId);

            // make sure the group is empty again
            users = Client.GetGroupUsers(groupId);
            Assert.NotNull(users);
            Assert.IsFalse(users.Any());

            // delete created group
            Client.DeleteRightsGroup(groupId);
        }

        [Test]
        public void Chapter6_06_11_GetRightsGroups()
        {
            var rights = Client.GetRightsGroups(new GroupFilter
            {
                UserID = TestUserID,
                Rights = new[]
                {
                    RightsEnum.VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG
                },
            }, 0, 10);
            AssertRequired(rights);
            AssertRequiredItems(rights.Groups);

            Assert.AreEqual(1, rights.Total);
            Assert.AreEqual(1, rights.Groups.Length);
            Assert.AreEqual("8344bacd-c415-4694-a9a4-b75e741f4eed", rights.Groups[0].GroupID);
        }

        [Test]
        public void Chapter6_07_02_GetUsers()
        {
            var users = Client.GetUsers(new UserFilter
            {
                Login = UserStarter1
            }, 0, 10);
            AssertRequired(users);
            AssertRequiredItems(users.Users);

            Assert.AreEqual(1, users.Total);
            Assert.AreEqual(1, users.Users.Length);
            Assert.AreEqual("5f0b90ef-76fe-49cc-8c8a-1029928effcc", users.Users[0].UserID);
        }

        [Test]
        public void Chapter6_08_02_GetAccountSystems()
        {
            var accSystems = Client.GetAccountSystems("starter_resident_1", 0, 10);
            AssertRequired(accSystems);
            AssertRequiredItems(accSystems.AccountSystems);

            Assert.AreEqual(1, accSystems.Total);
            Assert.AreEqual(1, accSystems.AccountSystems.Length);
            Assert.AreEqual("2f95f1cc-b337-4ff9-a62b-e67dbac21d43", accSystems.AccountSystems[0].AccountSystemID);
        }
    }
}
