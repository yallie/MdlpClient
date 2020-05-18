namespace MdlpApiClient.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter5 : UnitTestsClientBase
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
        public void Chapter5_01_SendDocumentText()
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
        public void Chapter5_0234_SendLargeDocument()
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
        public void Chapter5_05_GetLargeDocumentSize()
        {
            var size = Client.GetLargeDocumentSize();
            Assert.NotZero(size);
        }

        [Test]
        public void Chapter5_06_CancelSendDocument()
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
        public void Chapter5_07_GetOutcomeDocuments()
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
        public void Chapter5_08_GetIncomeDocuments()
        {
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 607,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
                ProcessedDateFrom = DateTime.Now.AddYears(-100),
                StartDate = DateTime.Now.AddYears(-100)
            },
            startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(10, docs.Documents.Length);
            Assert.IsTrue(docs.Total > 10);
        }

        [Test]
        public void Chapter5_09_GetDocumentMetadata()
        {
            var md = Client.GetDocumentMetadata(TestDocumentID);
            Assert.IsNotNull(md);
            Assert.AreEqual(TestDocumentID, md.DocumentID);
            Assert.AreEqual(TestDocRequestID, md.RequestID);
        }

        [Test]
        public void Chapter5_10_GetDocumentText()
        {
            var doc = Client.GetDocumentText(TestDocumentID);
            Assert.IsNotNull(doc);

            WriteLine("Downloaded document: {0}", TestDocumentID);
            WriteLine("{0}", doc);
        }

        [Test]
        public void Chapter5_11_GetDocumentsByRequestID()
        {
            var md = Client.GetDocumentsByRequestID(TestDocRequestID);
            Assert.IsNotNull(md);
            Assert.AreEqual(1, md.Total);
            Assert.AreEqual(1, md.Documents.Length);

            var doc = md.Documents.Single();
            Assert.AreEqual(TestDocumentID, doc.DocumentID);
            Assert.AreEqual(TestDocRequestID, doc.RequestID);
        }

        [Test]
        public void Chapter5_12_GetTicketText()
        {
            var ticket = Client.GetTicketText(TestTicketID);
            Assert.IsNotNull(ticket);

            WriteLine("Downloaded TicketID: {0}", TestTicketID);
            WriteLine("{0}", XDocument.Parse(ticket).ToString());
        }

        [Test]
        public void Chapter5_13_GetSignature()
        {
            // GetSignature doesn't seem to work, always returns error 406
            var ex = Assert.Throws<MdlpException>(() =>
            {
                var outDocId = "64037f8a-c816-4555-88ab-a00f74f7b222";
                var signature = Client.GetSignature(outDocId);
                Assert.IsNotNull(signature);

                WriteLine("Signature for DocumentID: {0}", outDocId);
                WriteLine("{0}", signature);
            });

            Assert.AreEqual(HttpStatusCode.NotAcceptable, ex.StatusCode); // 406
        }

        [Test]
        public void Chapter5_14_GetDocumentsBySkzkmReportID()
        {
            var docs = Client.GetDocumentsBySkzkmReportID(
                "434bc499-4b85-4775-8c19-bf6dbf730e93",
                startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Items);
            Assert.AreEqual(0, docs.Items.Length);
            Assert.AreEqual(0, docs.Total);
        }
    }
}
