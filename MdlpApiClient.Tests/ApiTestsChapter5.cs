namespace MdlpApiClient.Tests
{
    using System.Net;
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
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
            }, 
            startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(10, docs.Documents.Count);
            Assert.IsTrue(docs.Total > 10);
        }

        [Test]
        public void GetIncomeDocuments_5_8()
        {
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 311,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
            },
            startFrom: 0, count: 10);

            Assert.IsNotNull(docs);
            Assert.IsNotNull(docs.Documents);
            Assert.AreEqual(10, docs.Documents.Count);
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
    }
}
