namespace MdlpApiClient.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using MdlpApiClient.Serialization;
    using MdlpApiClient.Xsd;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsDocuments : UnitTestsClientBase
    {
        protected override MdlpClient CreateClient()
        {
            return new MdlpClient(credentials: new ResidentCredentials
            {
                ClientID = ClientID1,
                ClientSecret = ClientSecret1,
                UserID = TestUserThumbprint,
            },
            baseUrl: MdlpClient.StageApiHttps)
            {
                Tracer = WriteLine
            };
        }

        [Test]
        public void Chapter5_01_SendDocument()
        {
            var doc = new Documents
            {
                Version = "1.34",
                Register_End_Packing = new Register_End_Packing
                {
                    Subject_Id = "00000000100930",
                    Operation_Date = DateTime.Now,
                    Order_Type = Order_Type_Enum.Item1,
                    Series_Number = "100000001",
                    Expiration_Date = "22.08.2020",
                    Gtin = "11170012610151"
                }
            };

            doc.Register_End_Packing.Signs.Add("07091900400001TRANSF2000021");

            var docId = Client.SendDocument(doc);
            Assert.NotNull(docId);
        }

        [Test]
        public void Chapter5_10_GetDocument()
        {
            var doc = Client.GetDocument(TestDocumentID);
            Assert.IsNotNull(doc);

            WriteLine("Downloaded document: {0}", TestDocumentID);
            WriteLine("Document version: {0}", doc.Version);

            var an = doc.Accept_Notification;
            Assert.NotNull(an);
            WriteLine("SubjectID: {0}", an.Subject_Id);
            WriteLine("CounterpartyID: {0}", an.Counterparty_Id);
            WriteLine(XmlSerializationHelper.Serialize(doc));
        }

        [Test]
        public void Chapter5_12_GetTicket()
        {
            // ticket not found
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var ticket = Client.GetTicket(TestTicketID);
                Assert.IsNotNull(ticket);

                WriteLine("Downloaded TicketID: {0}", TestTicketID);
                WriteLine("Ticket version: {0}", ticket.Version);
                WriteLine("Operation: {0}", ticket.Result.Operation);
                WriteLine("Result: {0}", ticket.Result.Operation_Result);
                WriteLine("Comments: {0}", ticket.Result.Operation_Comment);
                WriteLine(XmlSerializationHelper.Serialize(ticket));
            });

            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }
    }
}
