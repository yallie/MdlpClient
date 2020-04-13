namespace MdlpApiClient.Tests
{
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter5 : UnitTestsBase
    {
        private MdlpClient Client = new MdlpClient(credentials: new ResidentCredentials
        {
            ClientID = ClientID1,
            ClientSecret = ClientSecret1,
            UserID = TestUserID,
        })
        {
            Tracer = TestContext.Progress.WriteLine
        };

        [Test]
        public void SendDocumentTest_5_1()
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

            var docId = Client.SendDocument(docXml);
            Assert.NotNull(docId);
        }

        [Test]
        public void SendLargeDocumentTest_5_234()
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

            var docId = Client.SendLargeDocument(docXml);
            Assert.NotNull(docId);
        }

        [Test]
        public void GetLargeDocumentSizeTest_5_5()
        {
            var size = Client.GetLargeDocumentSize();
            Assert.NotZero(size);
        }

        [Test]
        public void CancelSendDocumentTest_5_6()
        {
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var badDocId = "123";
                var badReqId = "321";
                Client.CancelSendDocument(badDocId, badReqId);
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        }

        [Test]
        public void GetOutcomeDocuments_5_7()
        {
            var docs = Client.GetOutcomeDocuments(new DocFilter
            {
                DocType = 311,
                DocStatus = DocStatusEnum.FAILED_RESULT_READY,
            }, 
            startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(10, docs.Documents.Length);
            Assert.IsTrue(docs.Total > 10);
        }

        [Test]
        public void GetIncomeDocuments_5_8()
        {
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 607,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
            },
            startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(10, docs.Documents.Length);
            Assert.IsTrue(docs.Total > 10);
        }

        [Test]
        public void GetDocumentMetadata_5_9()
        {
            var md = Client.GetDocumentMetadata(TestDocumentID);
            Assert.IsNotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
            Assert.AreEqual(TestDocRequestID, md.RequestID);
        }

        [Test]
        public void GetDocument_5_10()
        {
            var doc = Client.GetDocument(TestDocumentID);
            Assert.IsNotNull(doc);

            TestContext.Progress.WriteLine("Downloaded document: {0}", TestDocumentID);
            TestContext.Progress.WriteLine("{0}", doc);
        }

        [Test]
        public void GetDocuments_5_11()
        {
            var md = Client.GetDocuments(TestDocRequestID);
            Assert.IsNotNull(md);
            Assert.AreEqual(1, md.Total);
            Assert.AreEqual(1, md.Documents.Length);

            var doc = md.Documents.Single();
            Assert.AreEqual(TestDocumentID, doc.DocumentID);
            Assert.AreEqual(TestDocRequestID, doc.RequestID);
        }

        [Test]
        public void GetTicket_5_12()
        {
            var ticket = Client.GetTicket(TestTicketID);
            Assert.IsNotNull(ticket);

            TestContext.Progress.WriteLine("Downloaded TicketID: {0}", TestTicketID);
            TestContext.Progress.WriteLine("{0}", XDocument.Parse(ticket).ToString());
        }

        [Test]
        public void GetSignature_5_13()
        {
            // GetSignature doesn't seem to work, always returns error 406
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var outDocId = "64037f8a-c816-4555-88ab-a00f74f7b222";
                var signature = Client.GetSignature(outDocId);
                Assert.IsNotNull(signature);

                TestContext.Progress.WriteLine("Signature for DocumentID: {0}", outDocId);
                TestContext.Progress.WriteLine("{0}", signature);
            });

            Assert.AreEqual(HttpStatusCode.NotAcceptable, ex.StatusCode); // 406
        }

        [Test]
        public void GetDocumentsBySkzkmReportID_5_14()
        {
            var docs = Client.GetDocumentsBySkzkmReportID(
                "434bc499-4b85-4775-8c19-bf6dbf730e93",
                startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(0, docs.Documents.Length);
            Assert.AreEqual(0, docs.Total);
        }
    }
}
