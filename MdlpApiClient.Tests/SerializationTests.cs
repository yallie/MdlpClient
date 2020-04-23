namespace MdlpApiClient.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
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

            var serializer = new XmlSerializer(typeof(Documents));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, doc);

                var xml = stream.GetStringBuilder().ToString();
                Assert.NotNull(xml);

                var xdoc = XDocument.Parse(xml);
                Assert.NotNull(xdoc);
            }
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

            var serializer = new XmlSerializer(typeof(Documents));
            using (var stream = new StringReader(docXml))
            {
                var doc = serializer.Deserialize(stream) as Documents;
                Assert.NotNull(doc);
                Assert.NotNull(doc.Register_End_Packing);
                Assert.AreEqual("11170012610151", doc.Register_End_Packing.Gtin);
                Assert.AreEqual(1, doc.Register_End_Packing.Signs.Count);
                Assert.AreEqual("07091900400001TRANSF2000021", doc.Register_End_Packing.Signs[0]);
            }
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
            var serializer = new XmlSerializer(typeof(Documents));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, doc);

                var xml = stream.GetStringBuilder().ToString();
                Assert.NotNull(xml);
                WriteLine(xml);

                var xdoc = XDocument.Parse(xml);
                Assert.NotNull(xdoc);
            }
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
                // берем зарегистрированный КИЗ, который был в документе 311
                Sgtin = "50754041398745" + "1234567890123",

                // цена единицы продукции
                Cost = 1000,

                // сумма НДС
                Vat_Value = 100,
            });

            // и еще один такой же
            order.Add(new Move_OrderOrder_DetailsUnion
            {
                Sgtin = "50754041398745" + "1234567891123",
                Cost = 1000,
                Vat_Value = 100,
            });

            return doc;
        }

        [Test]
        public void XmlSerializeDocument415()
        {
            // формируем документ для загрузки в ЛК
            var doc = CreateDocument415();
            var serializer = new XmlSerializer(typeof(Documents));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, doc);

                var xml = stream.GetStringBuilder().ToString();
                Assert.NotNull(xml);

                // убедимся, что все отформатировано нормально и добавим комментарий
                var xdoc = XDocument.Parse(xml);
                Assert.NotNull(xdoc);
                xdoc.FirstNode.AddBeforeSelf(new XComment(" Отправка товара из Типографии в Автомойку "));
                WriteLine(ToXmlString(xdoc));
            }
        }

        public string ToXmlString(XDocument xdoc, SaveOptions options = SaveOptions.None)
        {
            var newLine = (options & SaveOptions.DisableFormatting) == SaveOptions.DisableFormatting ? "" : Environment.NewLine;
            return xdoc.Declaration == null ? xdoc.ToString(options) : xdoc.Declaration + newLine + xdoc.ToString(options);
        }
    }
}
