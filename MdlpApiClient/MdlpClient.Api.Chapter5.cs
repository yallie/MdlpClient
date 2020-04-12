namespace MdlpApiClient
{
    using DataContracts;
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using MdlpApiClient.Toolbox;

    /// <remarks>
    /// This file contains strongly typed REST API methods.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 5.1. Отправка документа
        /// </summary>
        /// <param name="xmlDocument">Документ в формате XML.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendDocument(string xmlDocument)
        {
            // 5.1
            var requestId = Guid.NewGuid().ToString();
            var result = Post<SendDocumentResponse>("documents/send", new
            {
                document = Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlDocument)),
                sign = ComputeSignature(xmlDocument),
                request_id = requestId,
            });

            return result.DocumentID;
        }

        /// <summary>
        /// 5.2. Отправка документа большого объема
        /// 5.3. Загрузка документа большого объема
        /// 5.4. Завершение отправки документа
        /// </summary>
        /// <param name="xmlDocument">Документ в формате XML.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendLargeDocument(string xmlDocument)
        {
            // 5.2
            var requestId = Guid.NewGuid().ToString();
            var link = Post<SendLargeDocumentResponse>("documents/send_large", new
            {
                sign = ComputeSignature(xmlDocument),
                hash_sum = xmlDocument.ComputeHash<SHA256>(),
                request_id = requestId,
            });

            // 5.3
            Put(link.Link, xmlDocument);

            // 5.4
            var result = Post<SendLargeDocumentResponse>("documents/send_finished", new
            {
                document_id = link.DocumentID
            });

            return link.DocumentID;
        }

        /// <summary>
        /// 5.5. Получение информации об ограничении размера небольших документов
        /// </summary>
        /// <returns>Максимальный размер документа в байтах.</returns>
        public int GetLargeDocumentSize()
        {
            // 5.5
            var result = Get<GetLargeDocumentSizeResponse>("documents/doc_size");
            return result.DocSize;
        }

        /// <summary>
        /// 5.6. Отмена отправки документа
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="requestId"></param>
        public void CancelSendDocument(string docId, string requestId)
        {
            Post("documents/cancel", new
            {
                document_id = docId,
                request_id = requestId,
            });
        }

        /// <summary>
        /// 5.9. Получение метаданных документа
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Метаданные документа</returns>
        public DocumentMetadata GetDocumentMetadata(string documentId)
        {
            return Get<DocumentMetadata>("documents/" + documentId);
        }
    }
}
