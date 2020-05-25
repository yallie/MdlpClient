namespace MdlpApiClient.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using MdlpApiClient.DataContracts;
    using MdlpApiClient.Serialization;
    using MdlpApiClient.Toolbox;
    using MdlpApiClient.Xsd;
    using NUnit.Framework;

    [TestFixture] // Ignore("Sandbox server is temporarily down")
    public class SandboxTests : UnitTestsClientBase
    {
        protected override MdlpClient CreateClient()
        {
            // Типография для типографий
            var cred = new ResidentCredentials
            {
                ClientID = "22d12250-6cf3-4a87-b439-f698cfddc498",
                ClientSecret = "3deb0ba1-26f2-4516-b652-931fe832e3ff",
                UserID = "10E4921908D24A0D1AD94A29BD0EF51696C6D8DA"
            };

            // подключаемся на этот раз к песочнице
            return new MdlpClient(cred, MdlpClient.SandboxApiHttps)
            {
                ApplicationName = "SandboxTests v1.0",
                Tracer = WriteLine,
            };
        }

        private MdlpClient CreateSecondClient()
        {
            // Автомойка-Чисто
            var cred = new ResidentCredentials
            {
                ClientID = "2cabd9b7-6042-40d8-97c2-8627f5704aa1",
                ClientSecret = "1713da9a-2042-465c-80ba-4da4dca3323d",
                UserID = "CC5D2B6C6457DED657D7EB7C388585D03ADDCBC8"
            };

            // подключаемся на этот раз к песочнице
            return new MdlpClient(cred, MdlpClient.SandboxApiHttps)
            {
                ApplicationName = "SandboxTests v2.0",
                Tracer = WriteLine,
            };
        }

        [Test]
        public void SandboxAuthorizationWorks()
        {
            Assert.DoesNotThrow(() =>
            {
                var size = Client.GetLargeDocumentSize();
                Assert.IsTrue(size > 100);
                WriteLine("Large doc size = {0} bytes", size);

                Assert.IsTrue(Client.SignatureSize > 0);
                WriteLine("Signature size = {0} bytes", Client.SignatureSize);
            });
        }

        private Documents CreateDocument311()
        {
            // Создаем документ схемы 311 от имени организации Типография для типографий
            // sessionUi — просто Guid, объединяющий документы в смысловую группу
            var sessionUi = "ca9a64ee-cf25-42af-a939-94d98fa16ab6";
            var doc = new Documents
            {
                // Если не указать версию, загрузка документа не срабатывает:
                // пишет, что тип документа не определен
                Version = "1.34",
                Session_Ui = sessionUi,

                // Окончание упаковки = схема 311
                Register_End_Packing = new Register_End_Packing
                {
                    // из личного кабинета тестового участника-Типографии
                    // берем код места деятельности, расположенного по адресу:
                    // край Забайкальский р-н Могойтуйский пгт Могойтуй ул Банзарова
                    Subject_Id = "00000000104494",

                    // в этом месте мы сегодня заканчиваем упаковку препаратов
                    Operation_Date = DateTime.Now,

                    // Тип производственного заказа — собственное производство = 1
                    // В сгенерированных XML-классах для числовых кодов
                    // созданы элементы Item: 1 = Item1, 40 = Item40, и т.д.
                    Order_Type = Order_Type_Enum.Item1,

                    // Номер производственной серии, 1-20 символов
                    Series_Number = "100000003",

                    // срок годности выдается в строковом виде, в формате ДД.ММ.ГГГГ
                    Expiration_Date = "30.03.2025",

                    // Код препарата. GTIN – указывается из реестра ЛП тестового участника
                    // из личного кабинета тестового участника-Типографии берем ЛП: Найзин
                    Gtin = "50754041398745"
                }
            };

            // Перечень идентификационных кодов потребительских упаковок.
            // Идентификаторы SGTIN. – формируются путем добавления к GTIN 
            // 13-значного серийного номера. Для каждой отгрузки 
            // необходимо генерировать уникальный серийный номер
            var gtin = doc.Register_End_Packing.Gtin;
            doc.Register_End_Packing.Signs.Add(gtin + "1234567906123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567907123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567908123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567909123");
            doc.Register_End_Packing.Signs.Add(gtin + "123456790A123");
            doc.Register_End_Packing.Signs.Add(gtin + "123456790B123");

            // Документ загружен, но не обработан: fe4120a9-0485-4b0d-a878-fba3d7a644bd.
            // Похоже, серверу-таки не нравится XML-декларация: <?xml version="1.0" encoding="..." ?>
            // Документ загружен и обработан: 72c55992-83de-4101-919b-20d985f06bb0
            return doc;
        }

        [Test, Explicit("Can't upload the same document more than once")]
        public void SendDocument311ToSandbox()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument311();
            var docId = Client.SendDocument(doc);
            WriteLine("Uploaded document #311: {0}", docId);
        }

        [Test]
        public void GetDocument311FromSandbox()
        {
            // прежние документы схемы 311:
            // "72c55992-83de-4101-919b-20d985f06bb0" — ок, но его удалили
            // "43e26ea9-7f84-4b92-bd94-37d897ed2a45" — после загрузки была ошибка обработки
            var document = Client.GetDocument("e5d3b7c3-a472-44c4-92c8-4feb3c2632a9");
            Assert.NotNull(document);
            Assert.NotNull(document.Register_End_Packing);
            Assert.NotNull(document.Register_End_Packing.Signs);
            Assert.AreEqual(6, document.Register_End_Packing.Signs.Count);
            Assert.AreEqual("00000000104494", document.Register_End_Packing.Subject_Id);
        }

        [Test]
        public void GetTicketForDocument311FromSandbox()
        {
            // квитанция об обработке документа появляется через какое-то время после загрузки
            // прежние документы схемы 311: 
            // "72c55992-83de-4101-919b-20d985f06bb0" — ок, но его удалили
            // "43e26ea9-7f84-4b92-bd94-37d897ed2a45" — после загрузки была ошибка обработки
            var ticket = Client.GetTicket("e5d3b7c3-a472-44c4-92c8-4feb3c2632a9");
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Result);
            Assert.AreEqual("311", ticket.Result.Operation);
            Assert.AreEqual("Успешное завершение операции", ticket.Result.Operation_Comment);
        }

        private Documents CreateDocument313()
        {
            // Создаем документ схемы 313 от имени организации Типография для типографий
            // sessionUi — просто Guid, объединяющий документы в смысловую группу
            // оставляем его прежним, чтобы связать с документом завершения упаковки 311
            var sessionUi = "ca9a64ee-cf25-42af-a939-94d98fa16ab6";
            var doc = new Documents
            {
                // Если не указать версию, загрузка документа не срабатывает:
                // пишет, что тип документа не определен
                Version = "1.34",
                Session_Ui = sessionUi,

                // Регистрация сведений о вводе ЛП в оборот (выпуск продукции) = схема 313
                Register_Product_Emission= new Register_Product_Emission
                {
                    // Идентификатор места деятельности (14 знаков) — 
                    // указывается идентификатор из ранее загруженной схемы 311:
                    // где упаковали, там и вводим ЛП в оборот
                    Subject_Id = "00000000104494",

                    // выпускаем препараты сегодня
                    Operation_Date = DateTime.Now,

                    // Реквизиты сведений о вводе ЛП в оборот
                    Release_Info = new Release_Info_Type
                    {
                        // Регистрационный номер документа подтверждения соответствия
                        Doc_Num = "123а",

                        // Дата регистрации документа подтверждения соответствия
                        Doc_Date = DateTime.Today.ToString(@"dd\.MM\.yyyy"),

                        // Номер документа подтверждения соответствия
                        Confirmation_Num = "123b",
                    },

                    // тут надо создать вложенный пустой объект
                    Signs = new Register_Product_EmissionSigns()
                }
            };

            // Перечень идентификационных кодов потребительских упаковок.
            // Идентификаторы SGTIN – указываются
            // номера из ранее загруженной 311 схемы
            var gtin = "50754041398745";
            var sgtins = doc.Register_Product_Emission.Signs.Sgtin;
            sgtins.Add(gtin + "1234567906123");
            sgtins.Add(gtin + "1234567907123");
            sgtins.Add(gtin + "1234567908123");
            sgtins.Add(gtin + "1234567909123");
            sgtins.Add(gtin + "123456790A123");
            sgtins.Add(gtin + "123456790B123");

            // В песочницу документ загружен через API и обработан,
            // получил код ecff5436-9a5d-408f-8d3b-0dd2eb6cad54
            return doc;
        }

        [Test, Explicit("Can't upload the same document more than once")]
        public void SendDocument313ToSandbox()
        {
            var doc = CreateDocument313();
            var docId = Client.SendDocument(doc);
            WriteLine("Uploaded document #313: {0}", docId);
        }

        [Test]
        public void GetDocument313FromSandbox()
        {
            // прежние документы схемы 313:
            // "ecff5436-9a5d-408f-8d3b-0dd2eb6cad54" — ок, но был удален
            var document = Client.GetDocument("728e315c-ee06-418d-82b0-79357eed8eb0");
            Assert.NotNull(document);
            Assert.NotNull(document.Register_Product_Emission);
            Assert.NotNull(document.Register_Product_Emission.Signs);
            Assert.AreEqual(6, document.Register_Product_Emission.Signs.Sgtin.Count);
            Assert.AreEqual("00000000104494", document.Register_Product_Emission.Subject_Id);
        }

        [Test]
        public void GetTicketForDocument313FromSandbox()
        {
            // прежние документы схемы 313:
            // "ecff5436-9a5d-408f-8d3b-0dd2eb6cad54" — ок, но был удален
            var ticket = Client.GetTicket("728e315c-ee06-418d-82b0-79357eed8eb0");
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Result);
            Assert.AreEqual("313", ticket.Result.Operation);
            Assert.AreEqual("Успешное завершение операции", ticket.Result.Operation_Comment);
        }

        private Documents CreateDocument915()
        {
            // Создаем документ схемы 915 от имени организации Типография для типографий
            // sessionUi оставляем прежним, чтобы связать с документами 
            // завершения упаковки 311 и ввода ЛП в оборот 313
            var sessionUi = "ca9a64ee-cf25-42af-a939-94d98fa16ab6";
            var doc = new Documents
            {
                // Если не указать версию, загрузка документа не срабатывает:
                // пишет, что тип документа не определен
                Version = "1.34",
                Session_Ui = sessionUi,

                // Регистрация в ИС МДЛП сведений об агрегировании во множество
                // третичных (заводских, транспортных) упаковок = схема 915
                Multi_Pack = new Multi_Pack
                {
                    // Идентификатор места деятельности (14 знаков) — 
                    // указывается идентификатор из ранее загруженной схемы 311:
                    // где упаковали и ввели ЛП в оборот, там и пакуем в коробку
                    Subject_Id = "00000000104494",

                    // дата упаковки
                    Operation_Date = DateTime.Now,

                    // вложены только SGTIN
                }
            };

            // в документе схемы 915 можно упаковать либо SGTIN, 
            // либо SSCC (третичную упаковку), но не одновременно
            var sgtinPack = new Multi_PackBy_SgtinDetail
            {
                // Идентификатор SSCC (откуда он берется?)
                // Для тестов: если делать новый документ, номер нужно увеличить на 1
                // Ниже по тексту есть обращение к этому номеру, его тоже поправить
                Sscc = "507540413987451236",
            };

            // Перечень идентификационных кодов потребительских упаковок.
            // Идентификаторы SGTIN – указываем первые четыре номера
            // номера из ранее загруженных схем 311 и 313
            // Первые 4 упакуем, оставшиеся 2 оставим неупакованными
            var gtin = "50754041398745";
            sgtinPack.Content.Add(gtin + "1234567906123");
            sgtinPack.Content.Add(gtin + "1234567907123");
            sgtinPack.Content.Add(gtin + "1234567908123");
            sgtinPack.Content.Add(gtin + "1234567909123");
            doc.Multi_Pack.By_Sgtin.Add(sgtinPack);

            // В песочницу документ загружен через API и обработан,
            // получил код 9534c7a7-7149-466a-aad2-1be19de810d6
            return doc;
        }

        [Test, Explicit("Can't upload the same document more than once")]
        public void SendDocument915ToSandbox()
        {
            var doc = CreateDocument915();
            var docId = Client.SendDocument(doc);
            WriteLine("Uploaded document #915: {0}", docId);
        }

        [Test]
        public void GetDocument915FromSandbox()
        {
            // прежние документы схемы 915:
            // "9534c7a7-7149-466a-aad2-1be19de810d6" — удален
            // "2e41f73b-5310-4cb4-917c-4d7ac0f7ecc3" — отклонен
            var document = Client.GetDocument("423d7e82-62c0-4f23-a421-158ec90f0ee3");
            Assert.NotNull(document);
            Assert.NotNull(document.Multi_Pack);
            Assert.NotNull(document.Multi_Pack.By_Sgtin);
            Assert.AreEqual(1, document.Multi_Pack.By_Sgtin.Count);
            Assert.AreEqual("00000000104494", document.Multi_Pack.Subject_Id);
        }

        [Test]
        public void GetTicketForDocument915FromSandbox()
        {
            // прежние документы схемы 915:
            // "9534c7a7-7149-466a-aad2-1be19de810d6" — удален
            // "2e41f73b-5310-4cb4-917c-4d7ac0f7ecc3" — отклонен
            var ticket = Client.GetTicket("423d7e82-62c0-4f23-a421-158ec90f0ee3");
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Result);
            Assert.AreEqual("915", ticket.Result.Operation);
            Assert.AreEqual("Успешное завершение операции", ticket.Result.Operation_Comment);
        }

        private Documents CreateDocument415()
        {
            // Создаем документ схемы 415 от имени организации Типография для типографий
            // sessionUi — просто Guid, объединяющий документы в смысловую группу
            var sessionUi = "ca9a64ee-cf25-42af-a939-94d98fa16ab6";
            var doc = new Documents
            {
                // Если не указать версию, загрузка документа не срабатывает:
                // пишет, что тип документа не определен
                Version = "1.34",
                Session_Ui = sessionUi,

                // Перемещение на склад получателя = схема 415
                Move_Order = new Move_Order
                {
                    // из личного кабинета тестового участника-Типографии
                    // берем код места деятельности, расположенного по адресу:
                    // край Забайкальский р-н Могойтуйский пгт Могойтуй ул Банзарова
                    // здесь у нас пока хранятся упакованные ЛП, ждущие отправки
                    Subject_Id = "00000000104494",

                    // из ЛК тестового участника-Автомойки
                    // берем код места деятельности, расположенного по адресу:
                    // Респ Адыгея р-н Тахтамукайский пгт Яблоновский ул Гагарина
                    // сюда мы будем отправлять упакованные ЛП
                    Receiver_Id = "00000000104453",

                    // сегодня отправляем препараты
                    Operation_Date = DateTime.Now,

                    // Реквизиты документа отгрузки: номер и дата документа
                    Doc_Num = "123а",
                    Doc_Date = DateTime.Today.ToString(@"dd\.MM\.yyyy"),

                    // Тип операции отгрузки со склада: 1 - продажа, 2 - возврат
                    Turnover_Type = Turnover_Type_Enum.Item1,

                    // Источник финансирования: 1 - собственные средства, 2-3 - бюджет
                    Source = Source_Type.Item1,

                    // Тип договора: 1 - купля-продажа
                    Contract_Type = Contract_Type_Enum.Item1,

                    // Реестровый номер контракта (договора)
                    // в Единой информационной системе в сфере закупок
                    // нам в данном случае не требуется
                    Contract_Num = null,
                },
            };

            // Список отгружаемой продукции
            var order = doc.Move_Order.Order_Details;
            order.Add(new Move_OrderOrder_DetailsUnion
            {
                // берем зарегистрированный КИЗ, который был в документе 311,
                // введен в оборот документом 313, но не упаковам документом 915
                Sgtin = "50754041398745" + "123456790A123",

                // цена единицы продукции
                Cost = 1000,

                // сумма НДС
                Vat_Value = 100,
            });

            // и еще один — упакованный ящик, полученный в документе схемы 915
            // если у нас несколько уровней упаковки, сведения подаются только
            // по коробу самого верхнего уровня
            var ssccItem = new Move_OrderOrder_DetailsUnion
            {
                // Sgtin отсутствует, зато присутствует Sscc_Detail
                Sscc_Detail = new Move_OrderOrder_DetailsUnionSscc_Detail
                {
                    // код третичной упаковки тот же, что был в документе схемы 915
                    Sscc = "507540413987451236"
                },

                // а вот нужны ли здесь Cost и Vat_Value, непонятно,
                // поскольку они есть в самом Sscc_Detail
                Cost = 1000,
                Vat_Value = 100,
            };

            // Здесь может быть и пусто
            // Тут указываются только те препараты, у которых цена не совпадает с той, что указана на коробе
            // Такое бывает, если в короб вложены несколько разных препаратов
            ssccItem.Sscc_Detail.Detail.Add(new Move_OrderOrder_DetailsUnionSscc_DetailDetail
            {
                // GTIN и номер производственной серии — из документа 311
                Gtin = "50754041398745",
                Series_Number = "100000003",

                // стоимость единицы продукции с учетом НДС и сумма НДС, руб
                Cost = 1000,
                Vat_Value = 100,
            });

            // итого в документе перемещения схемы 415 у нас один SGTIN
            // и один ящик SSCC с четырьмя SGTIN-ами,
            // а еще один неупакованный SGTIN остался в типографии
            order.Add(ssccItem);

            // документ загружен через API и обработан,
            // получил код a8528183-b4d9-4e1c-b1ed-c2d6dab97504
            return doc;
        }

        [Test, Explicit("Can't upload the same document more than once")]
        public void SendDocument415ToSandbox()
        {
            var doc = CreateDocument415();
            var docId = Client.SendDocument(doc);
            WriteLine("Uploaded document #415: {0}", docId);
        }

        [Test]
        public void GetDocument415FromSandbox()
        {
            // прежние документы схемы 415:
            // "667f1d2f-0d4f-43c4-9d56-b5d3da29c4ac" — удален
            var document = Client.GetDocument("a8528183-b4d9-4e1c-b1ed-c2d6dab97504");
            Assert.NotNull(document);
            Assert.NotNull(document.Move_Order);
            Assert.NotNull(document.Move_Order.Order_Details);
            Assert.AreEqual(2, document.Move_Order.Order_Details.Count);
            Assert.AreEqual("00000000104494", document.Move_Order.Subject_Id);
        }

        [Test]
        public void GetTicketForDocument415FromSandbox()
        {
            // прежние документы схемы 415:
            // "667f1d2f-0d4f-43c4-9d56-b5d3da29c4ac" — удален
            var ticket = Client.GetTicket("a8528183-b4d9-4e1c-b1ed-c2d6dab97504");
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Result);
            Assert.AreEqual("415", ticket.Result.Operation);
            Assert.AreEqual("Успешное завершение операции", ticket.Result.Operation_Comment);
        }

        private Documents CreateDocument701(Documents doc601)
        {
            var mo = doc601.Move_Order_Notification;
            var doc = new Documents
            {
                Version = doc601.Version,
                Session_Ui = doc601.Session_Ui,
                Accept = new Accept
                {
                    // Отправитель и получатель меняются местами
                    Subject_Id = mo.Receiver_Id,
                    Counterparty_Id = mo.Subject_Id,

                    // Дата и время текущие
                    Operation_Date = DateTime.Now,
                    Order_Details = new AcceptOrder_Details(),
                }
            };

            // Список подтверждаемой продукции:
            // не знаю, можно ли подтвердить не все, что в документе 601?
            var od = doc.Accept.Order_Details;
            foreach (var unit in mo.Order_Details)
            {
                // подтверждаем прием упаковки с несколькими ЛП
                if (unit.Sscc_Detail != null && unit.Sscc_Detail.Sscc != null)
                {
                    // Номер транспортной упаковки
                    od.Sscc.Add(unit.Sscc_Detail.Sscc);
                    continue;
                }

                // подтверждаем прием экземпляра ЛП
                if (unit.Sgtin != null)
                {
                    // Номер SGTIN указывается номер из ранее загруженной 415 схемы
                    od.Sgtin.Add(unit.Sgtin);
                    continue;
                }
            }

            // загружен через API, обработан, код d72a2afc-fddd-43e3-b308-a8c3eece70a4
            return doc;
        }

        [Test]
        public void FindIncomingDocument601()
        {
            // Документы структуры 601 создает сама песочница, мы такой документ загрузить не можем.
            // Чтобы получить документ 601, мы (отправитель) загружаем документ 415,
            // а в ответ песочница посылает нашему контрагенту (получателю) документ 601.
            // Получателем выступает второй тестовый участник.
            // Заходим в песочницу от имени второго участника.
            using (var client = CreateSecondClient())
            {
                // находим уведомление в списке входящих документов
                var docs = client.GetIncomeDocuments(new DocFilter
                {
                    DocType = 601,
                    SenderID = "00000000104494",
                    ProcessedDateFrom = new DateTime(2020, 05, 18, 19, 10, 00),
                    ProcessedDateTo = new DateTime(2020, 05, 18, 19, 18, 00),
                }, 0, 1);

                // оно там будет одно в указанный период
                Assert.AreEqual(1, docs.Total);
                Assert.AreEqual(1, docs.Documents.Length);

                // прежние документы структуры 601:
                // "6faca9fc-5390-406f-b935-03ee4705e4ac" — удален
                var doc = docs.Documents[0];
                Assert.AreEqual("ba494f1d-09e1-4b91-88e5-b37cbbb1be78", doc.DocumentID);

                // скачиваем уведомление по коду
                var doc601 = client.GetDocument(doc.DocumentID).Move_Order_Notification;
                Assert.NotNull(doc601);
                Assert.AreEqual("00000000104494", doc601.Subject_Id);
                Assert.AreEqual("00000000104453", doc601.Receiver_Id);

                // содержимое должно соответствовать отосланному документу схемы 415
                var details = doc601.Order_Details;
                Assert.AreEqual(2, details.Count);

                // тут у нас зарегистрированный КИЗ, который был в документе схемы 311,
                // введен в оборот документом схемы 313, но не упаковам документом схемы 915
                // и отправлен в наш адрес документом схемы 415
                Assert.AreEqual("50754041398745" + "123456790A123", details[0].Sgtin);

                // а тут у нас код третичной упаковки тот же, что был в документе схемы 915
                // это ящик, в котором упакованы 4 КИЗа документом 915
                // этот ящик тоже отправлен в наш адрес документом схемы 415
                Assert.AreEqual("507540413987451236", details[1].Sscc_Detail.Sscc);
            }
        }

        [Test, Explicit("Can't upload the same document more than once")]
        public void SendDocument701ToSandbox()
        {
            // прежние документы структуры 601:
            // "6faca9fc-5390-406f-b935-03ee4705e4ac" — удален
            // скачиваем уведомление 601 по коду
            using (var client = CreateSecondClient())
            {
                var doc601 = client.GetDocument("ba494f1d-09e1-4b91-88e5-b37cbbb1be78");
                Assert.NotNull(doc601.Move_Order_Notification);

                // формируем ответ на него: мол, подтверждаем получение всех ЛП в полном объеме
                var doc701 = CreateDocument701(doc601);
                var docId = client.SendDocument(doc701);
                WriteLine("Uploaded document #701: {0}", docId);
            }
        }

        [Test]
        public void GetDocument701AndTicket701FromSandbox()
        {
            using (var client = CreateSecondClient())
            {
                // прежние документы схемы 701:
                // d72a2afc-fddd-43e3-b308-a8c3eece70a4 — удален
                var document = client.GetDocument("602d156b-514c-46d4-9bc5-e87515f51c16");
                Assert.NotNull(document);
                Assert.NotNull(document.Accept);
                Assert.NotNull(document.Accept.Order_Details);
                Assert.AreEqual(1, document.Accept.Order_Details.Sgtin.Count);
                Assert.AreEqual(1, document.Accept.Order_Details.Sscc.Count);
                Assert.AreEqual("00000000104494", document.Accept.Counterparty_Id);

                var ticket = client.GetTicket("602d156b-514c-46d4-9bc5-e87515f51c16");
                Assert.NotNull(ticket);
                Assert.NotNull(ticket.Result);
                Assert.AreEqual("701", ticket.Result.Operation);
                Assert.AreEqual("Успешное завершение операции", ticket.Result.Operation_Comment);
            }
        }

        [Test]
        public void GetDocument607FromSandbox()
        {
            // находим уведомление в списке входящих документов
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 607,
                SenderID = "00000000104453",
                ProcessedDateFrom = new DateTime(2020, 05, 18, 19, 37, 00),
                ProcessedDateTo = new DateTime(2020, 05, 18, 19, 38, 00),
            }, 0, 1);

            // оно там будет одно в указанный период
            Assert.AreEqual(1, docs.Total);
            Assert.AreEqual(1, docs.Documents.Length);
            var docId = docs.Documents[0].DocumentID;

            // код документа стал известен после первого запуска теста
            Assert.AreEqual("590b3155-ea61-49e4-90a4-19bdda30f3da", docId);
            var text = Client.GetDocumentText(docId);
            WriteLine(text);

            // получим документ подтверждения
            var doc = Client.GetDocument(docId);
            Assert.NotNull(doc.Accept_Notification);
            var details = doc.Accept_Notification.Order_Details;
            Assert.NotNull(details);

            // в нем будут: один КИЗ и упаковка из четырех КИЗ
            Assert.AreEqual(1, details.Sgtin.Count);
            Assert.AreEqual("50754041398745" + "123456790A123", details.Sgtin[0]);
            Assert.AreEqual(1, details.Sscc.Count);
            Assert.AreEqual("507540413987451236", details.Sscc[0]);
        }

        private Documents CreateDocument210(string senderId, string sgtin = null, string ssccUp = null, string ssccDown = null)
        {
            // Схема 210 устарела и будет вскоре удалена
            // создаем запрос содержимого упаковки
            // в этом документе надо указывать одно из трех: либо SGTIN, либо SSCC up, либо SSCC down
            var doc = new Documents();
            doc.Query_Kiz_Info = new Query_Kiz_Info
            {
                Subject_Id = senderId,
                Sgtin = sgtin,
                Sscc_Down = ssccDown,
                Sscc_Up = ssccUp,
            };

            return doc;
        }

        [Test, Explicit("Schema210 is obsolete and will be replaced with schema 220")]
        public void SendDocument210WithSgtinAndSsccToSandbox()
        {
            // Документ 210 возвращает информацию о содержимом короба, либо о КИЗ
            // из личного кабинета тестового участника-Типографии
            // берем код места деятельности, расположенного по адресу:
            // край Забайкальский р-н Могойтуйский пгт Могойтуй ул Банзарова
            // отсюда делалась отправка ЛП
            var senderId = "00000000104494";

            // Код препарата. GTIN – указывается из реестра ЛП тестового участника
            // из личного кабинета тестового участника-Типографии берем ЛП: Найзин
            var gtin = "50754041398745";

            // SGTIN = GTIN + S/N
            var sgtin = gtin + "1234567906123";

            // Идентификатор SSCC из документа 915 (он же был в документах 415 и 601)
            var sscc = "507540413987451236";

            // Пошлем документ, в данном случае получили код:
            // 89219f2a-f1db-4d2d-bcfb-05274c2188cd
            // 734a0898-0c10-487e-af6b-cf7fb3ef050f
            // f1bdc175-3740-4a4e-b7dd-bbb61c140d4c
            var doc210 = CreateDocument210(senderId, sgtin, sscc, sscc);
            var docId = Client.SendDocument(doc210);
            WriteLine("Sent document 210: {0}", docId);
        }

        [Test, Explicit("Schema210 is obsolete and will be replaced with schema 220")]
        public void SendDocument210WithSsccDownToSandbox()
        {
            // Документ 210 возвращает информацию о содержимом короба, либо о КИЗ
            // из личного кабинета тестового участника-Типографии
            // берем код места деятельности, расположенного по адресу:
            // край Забайкальский р-н Могойтуйский пгт Могойтуй ул Банзарова
            // отсюда делалась отправка ЛП
            var senderId = "00000000104494";

            // Идентификатор SSCC из документа 915 (он же был в документах 415 и 601)
            var sscc = "507540413987451236";

            // Пошлем документ, в данном случае получили код:
            var doc210 = CreateDocument210(senderId, ssccDown: sscc);
            var docId = Client.SendDocument(doc210);
            WriteLine("Sent document 210: {0}", docId);
        }

        [Test]
        public void GetDocument210FromSandbox()
        {
            // Код отправленного документа схемы 210:
            // "734a0898-0c10-487e-af6b-cf7fb3ef050f");
            // "f1bdc175 -3740-4a4e-b7dd-bbb61c140d4c");
            // "f44d0b72-7259-499c-859d-a50b4c6232e4"
            // "7db0d364-6577-4699-aa22-a6dbe7f3184c"
            var doc = Client.GetDocumentMetadata("7db0d364-6577-4699-aa22-a6dbe7f3184c");
            WriteLine(doc.DocStatus);

            // ответ на схему 210 — схема 211, получаем ее как квитанцию к документу
            var ticket = Client.GetTicket(doc.DocumentID);
            Assert.NotNull(ticket.Kiz_Info);

            // если был запрос SGTIN, то было бы заполнено следующее:
            //Assert.NotNull(ticket.Kiz_Info.Sgtin);
            //Assert.NotNull(ticket.Kiz_Info.Sgtin.Info_Sgtin);
            //Assert.AreEqual("50754041398745", ticket.Kiz_Info.Sgtin.Info_Sgtin.Gtin);

            // запрос по коду третичной упаковки
            Assert.NotNull(ticket.Kiz_Info.Sscc_Down);
            Assert.IsTrue(ticket.Kiz_Info.Sscc_Down.Any());
            Assert.NotNull(ticket.Kiz_Info.Sscc_Down[0]);
            Assert.NotNull(ticket.Kiz_Info.Sscc_Down[0].Sgtin);
            Assert.IsTrue(ticket.Kiz_Info.Sscc_Down.Any(s => s.Sgtin.Info_Sgtin.Sgtin == "507540413987451234567906123"));
        }

        [Test]
        public void UploadedDocumentIsImmediatelyAvailableForDownload()
        {
            var doc = CreateDocument210(senderId: "00000000104494", sgtin: "50754041398745" + "1234567906123");
            var docId = Client.SendDocument(doc);
            WriteLine("Uploaded document: {0}", docId);

            // may throw 404 NotFound?
            var md = Client.GetDocumentMetadata(docId);
            Assert.NotNull(md);
        }

        [Test]
        public void Sandbox_Issue_SimilarToTest_TestServer_IssueSR00497874()
        {
            // 1. получаем список входящих документов
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 601,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
                ProcessedDateFrom = new DateTime(2020, 01, 08), // new DateTime(2019, 11, 01),
                ProcessedDateTo = new DateTime(2020, 01, 12), // new DateTime(2019, 12, 01)
            }, 0, 1);
            Assert.NotNull(docs);
            Assert.NotNull(docs.Documents);
            Assert.AreEqual(1, docs.Documents.Length);

            // 2. скачиваем первый документ из списка, получаем ошибку
            var docId = docs.Documents[0].DocumentID;
            Assert.IsFalse(string.IsNullOrWhiteSpace(docId));
            var doc = Client.GetDocumentText(docId);
            Assert.NotNull(doc);
        }

        [Test, Explicit]
        public void GetIncomeMoveOrderNotifications()
        {
            Client.Tracer = null;
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 601,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
                ProcessedDateFrom = DateTime.Today.AddDays(-200),
            }, 0, 400);

            foreach (var d in docs.Documents)
            {
                var xml = Client.GetDocumentText(d.DocumentID);
                var md = XmlSerializationHelper.Deserialize(xml);
                Assert.NotNull(md.Move_Order_Notification);
                if (md.Move_Order_Notification.Order_Details.IsNullOrEmpty())
                {
                    continue;
                }

                var od = md.Move_Order_Notification.Order_Details;
                if (od.Where(o => o.Sscc_Detail != null).Any() && od.Where(o => o.Sgtin != null).Any())
                {
                    WriteLine("==== Move order with SGTINs and SSCCs =======");
                    WriteLine(xml);
                }
            }
        }
    }
}
