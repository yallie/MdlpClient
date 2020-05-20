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
        public void TestServer_IssueSR00497874()
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
    }
}
