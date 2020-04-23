namespace MdlpApiClient.Tests
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using MdlpApiClient.Serialization;
    using MdlpApiClient.Toolbox;
    using MdlpApiClient.Xsd;
    using NUnit.Framework;
    using RestSharp;

    [TestFixture]
    public class SerializationTests : UnitTestsBase
    {
        #region JSON иерархия — нет, такое мы использовать не будем:

        [DataContract]
        public class Base
        {
            [DataMember(Name = "sys_id")]
            public string SystemSubjectID { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }

        [DataContract]
        public class Native : Base
        {
            [DataMember(Name = "inn")]
            public string Inn { get; set; }
        }

        [DataContract]
        public class Foreign : Base
        {
            [DataMember(Name = "itin")]
            public string Itin { get; set; }
        }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "items")]
            public Base[] Items { get; set; }
        }

        [Test]
        public void JsonTest()
        {
            var r = new Response
            {
                Items = new Base[]
                {
                    new Base { SystemSubjectID = "1", Name = "Base1" },
                    new Native { SystemSubjectID = "2", Name = "Native2", Inn = "3121" },
                    new Foreign { SystemSubjectID = "3", Name = "Foreign3", Itin = "1231" },
                }
            };

            var s = new ServiceStackSerializer();
            var json = s.Serialize(r);
            Assert.NotNull(json);
            WriteLine(JsonFormatter.FormatJson(json));

            var obj = s.Deserialize<Response>(new RestResponse() { Content = json });
            Assert.NotNull(obj);
        }

        #endregion

        [Test]
        public void XmlSerializationTest()
        {
            var doc = new Documents();
            doc.Register_End_Packing = new Register_End_Packing();
            doc.Register_End_Packing.Gtin = "12345";
            doc.Register_End_Packing.Signs.Add("43232424");
            doc.Register_End_Packing.Signs.Add("654o6u45");
            doc.Register_End_Packing.Signs.Add("fstkjwtk");

            var xml = XmlSerializationHelper.SerializeDocument(doc);
            Assert.NotNull(xml);
        }

        [Test]
        public void XmlDeserializationTest()
        {
            var docXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<documents xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""1.34"">
  <register_end_packing action_id=""311"">
    <subject_id>00000000100930</subject_id>
    <operation_date>2020-04-08T16:14:05.8168969+03:00</operation_date>
    <order_type>1</order_type>
    <series_number>100000001</series_number>
    <expiration_date>22.08.2020</expiration_date>
    <gtin>11170012610151</gtin>
    <signs>
      <sgtin>07091900400001TRANSF2000021</sgtin>
    </signs>
  </register_end_packing>
</documents>";

            var doc = XmlSerializationHelper.DeserializeDocument(docXml);
            Assert.NotNull(doc);
            Assert.NotNull(doc.Register_End_Packing);
            Assert.AreEqual("11170012610151", doc.Register_End_Packing.Gtin);
            Assert.AreEqual(1, doc.Register_End_Packing.Signs.Count);
            Assert.AreEqual("07091900400001TRANSF2000021", doc.Register_End_Packing.Signs[0]);
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
            doc.Register_End_Packing.Signs.Add(gtin + "1234567890123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567891123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567892123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567893123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567894123");
            doc.Register_End_Packing.Signs.Add(gtin + "1234567895123");

            // В песочницу документ успешно загружен через ЛК и обработан,
            // код документа a711f795-123b-486b-a2f9-590124733a5e
            return doc;
        }

        [Test]
        public void XmlSerializeDocument311()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument311();
            var xml = XmlSerializationHelper.SerializeDocument(doc);
            WriteLine(xml);
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
            sgtins.Add(gtin + "1234567890123");
            sgtins.Add(gtin + "1234567891123");
            sgtins.Add(gtin + "1234567892123");
            sgtins.Add(gtin + "1234567893123");
            sgtins.Add(gtin + "1234567894123");
            sgtins.Add(gtin + "1234567895123");

            // В песочницу документ успешно загружен через ЛК и обработан,
            // код документа 68a653f5-fc90-4db5-a797-f261e108b083
            return doc;
        }

        [Test]
        public void XmlSerializeDocument313()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument313();
            var xml = XmlSerializationHelper.SerializeDocument(doc, " Типография вводит в оборот свежеупакованные ЛП ");
            WriteLine(xml);
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
                Sscc = "507540413987451234",
            };

            // Перечень идентификационных кодов потребительских упаковок.
            // Идентификаторы SGTIN – указываем первые четыре номера
            // номера из ранее загруженных схем 311 и 313
            // Первые 4 упакуем, оставшиеся 2 оставим неупакованными
            var gtin = "50754041398745";
            sgtinPack.Content.Add(gtin + "1234567890123");
            sgtinPack.Content.Add(gtin + "1234567891123");
            sgtinPack.Content.Add(gtin + "1234567892123");
            sgtinPack.Content.Add(gtin + "1234567893123");
            doc.Multi_Pack.By_Sgtin.Add(sgtinPack);

            // В песочницу документ успешно загружен через ЛК и обработан,
            // код документа ae627fb0-f6f4-4967-9c22-26072f15e90c
            return doc;
        }

        [Test]
        public void XmlSerializeDocument915()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument915();
            var xml = XmlSerializationHelper.SerializeDocument(doc, " Упаковка товара в Типографии ");
            WriteLine(xml);
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
                Sgtin = "50754041398745" + "1234567894123",

                // цена единицы продукции
                Cost = 1000,

                // сумма НДС
                Vat_Value = 100,
            });

            // и еще один — упакованный ящик, полученный в документе схемы 915
            var ssccItem = new Move_OrderOrder_DetailsUnion
            {
                // Sgtin отсутствует, зато присутствует Sscc_Detail
                Sscc_Detail = new Move_OrderOrder_DetailsUnionSscc_Detail
                {
                    // код третичной упаковки тот же, что был в документе схемы 915
                    Sscc = "507540413987451234"
                },

                // а вот нужны ли здесь Cost и Vat_Value, непонятно,
                // поскольку они есть в самом Sscc_Detail
                Cost = 1000,
                Vat_Value = 100,
            };

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

            // документ загружен в ЛК и обработан,
            // получил код c78eb0bd-c70b-47b4-8f8e-535abcb4bd40
            return doc;
        }

        [Test]
        public void XmlSerializeDocument415()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument415();
            var xml = XmlSerializationHelper.SerializeDocument(doc, " Отправка товара из Типографии в Автомойку: один ящик с 4 препаратами и 1 препарат ");
            WriteLine(xml);
        }

        // из ЛК загружен документ: 3ad8d361-0044-48fc-b0ee-aeed97df3f8e
        private const string Doc601xml = @"<!-- Отправка товара из Типографии в Автомойку: один ящик с 4 препаратами и 1 препарат --><documents xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" version=""1.34"" session_ui=""ca9a64ee-cf25-42af-a939-94d98fa16ab6"">
    <move_order_notification action_id=""601"">
        <subject_id>00000000104494</subject_id>
        <receiver_id>00000000104453</receiver_id>
        <operation_date>2020-04-23T21:10:13.9733733+03:00</operation_date>
        <doc_num>123а</doc_num>
        <doc_date>23.04.2020</doc_date>
        <turnover_type>1</turnover_type>
        <source>1</source>
        <contract_type>1</contract_type>
        <order_details>
            <union>
                <sgtin>507540413987451234567894123</sgtin>
                <cost>1000</cost>
                <vat_value>100</vat_value>
            </union>
            <union>
                <sscc_detail>
                    <sscc>507540413987451234</sscc>
                    <detail>
                        <gtin>50754041398745</gtin>
                        <series_number>100000003</series_number>
                        <cost>1000</cost>
                        <vat_value>100</vat_value>
                    </detail>
                </sscc_detail>
                <cost>1000</cost>
                <vat_value>100</vat_value>
            </union>
        </order_details>
    </move_order_notification>
</documents>
";

        [Test]
        public void XmlDeserializeDocument601()
        {
            // из ЛК загружен документ схемы 601 с кодом 3ad8d361-0044-48fc-b0ee-aeed97df3f8e
            var doc = XmlSerializationHelper.DeserializeDocument(Doc601xml);
            var mo = doc.Move_Order_Notification;
            Assert.NotNull(mo);
            Assert.AreEqual("00000000104494", mo.Subject_Id); // типография в Могойтуй
            Assert.AreEqual("00000000104453", mo.Receiver_Id); // автомойка в пгт Яблоновский
            Assert.NotNull(mo.Order_Details);
            Assert.AreEqual(2, mo.Order_Details.Count);

            var sgtin = mo.Order_Details[0];
            Assert.AreEqual("507540413987451234567894123", sgtin.Sgtin);
            Assert.IsNull(sgtin.Sscc_Detail);

            var sscc = mo.Order_Details[1];
            Assert.IsNull(sscc.Sgtin);
            Assert.IsNotNull(sscc.Sscc_Detail);
            Assert.AreEqual(1, sscc.Sscc_Detail.Detail.Count);

            var ssccSgtin = sscc.Sscc_Detail.Detail[0];
            Assert.IsNotNull(ssccSgtin);
            Assert.AreEqual("50754041398745", ssccSgtin.Gtin);
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

            // загружен через ЛК, обработан, код 20b0f54b-0528-4081-b815-38f376c6ab3c
            return doc;
        }

        [Test]
        public void XmlSerializeDocument701()
        {
            // получили 601 => создали на его основе 701
            var doc601 = XmlSerializationHelper.DeserializeDocument(Doc601xml);
            var doc701 = CreateDocument701(doc601);
            var xml = XmlSerializationHelper.SerializeDocument(doc701, " Подтверждение из Автомойки ");
            WriteLine(xml);
        }
    }
}
