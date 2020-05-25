namespace MdlpApiClient.Tests
{
    using System;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsMisc : UnitTestsClientBase
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

        /// <summary>
        /// Ошибка скачивания документа по идентификатору
        /// </summary>
        [Test]
        public void TestServer_DocumentIssueSR00497874()
        {
            // 1. получаем список входящих документов
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                DocType = 607,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
                ProcessedDateFrom = DateTime.Now.AddYears(-100)
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

        [Test]
        public void TestServer_GetSgtinsRequestRate()
        {
            Client.GetSgtins("04607028394287PQ28I2DHQDF1V");

            Client.GetSgtins(new SgtinFilter
            {
                Sgtin = "04607028394287PQ28I2DHQDF1V",
            },
            startFrom: 0, count: 1);

            Client.GetSgtins(new SgtinFilter
            {
                Sgtin = "04607028394287PQ28I2DHQDF2V",
            },
            startFrom: 0, count: 1);

            Client.GetSgtins("04607028394287PQ28I2DHQDF1V");
        }

        /// <summary>
        /// Ошибка скачивания тикета по идентификатору
        /// </summary>
        [Test, Ignore("Fails on test stage")]
        public void TestServer_TicketIssueSR00497874()
        {
            // 1. получаем список входящих документов
            var docs = Client.GetIncomeDocuments(new DocFilter
            {
                //DocType = 607,
                DocStatus = DocStatusEnum.PROCESSED_DOCUMENT,
                ProcessedDateFrom = new DateTime(2020, 05, 23, 0, 30, 00)
            }, 0, 1);
            Assert.NotNull(docs);
            Assert.NotNull(docs.Documents);
            Assert.AreEqual(1, docs.Documents.Length);

            // 2. скачиваем первый тикет из списка
            var docId = docs.Documents[0].DocumentID;
            Assert.IsFalse(string.IsNullOrWhiteSpace(docId));
            var doc = Client.GetTicketText(docId);
            Assert.NotNull(doc);
        }

        [Test]
        public void TestStage_UploadedDocumentIsImmediatelyAvailableForDownload()
        {
            var xml = @"<documents 
              xmlns:by=""https://www.nuget.org/packages/MdlpApiClient/1.3.0"" 
              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
              version=""1.34"">
              <query_kiz_info action_id=""210"">
                <subject_id>00000000104494</subject_id>
                <sgtin>507540413987451234567906123</sgtin>
              </query_kiz_info>
            </documents>";

            var docId = Client.SendDocument(xml);
            WriteLine("Uploaded document: {0}", docId);

            // may throw 404 NotFound?
            var md = Client.GetDocumentMetadata(docId);
            Assert.NotNull(md);
        }
    }
}
