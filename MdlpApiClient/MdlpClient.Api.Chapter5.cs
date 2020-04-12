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
            var result = Post<SendDocumentResponse>("documents/send", new
            {
                document = Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlDocument)),
                sign = ComputeSignature(xmlDocument),
                request_id = Guid.NewGuid().ToString(),
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
            var link = Post<SendLargeDocumentResponse>("documents/send_large", new
            {
                sign = ComputeSignature(xmlDocument),
                hash_sum = xmlDocument.ComputeHash<SHA256>(),
                request_id = Guid.NewGuid().ToString(),
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
        /// 5.7. Получение списка исходящих документов
        /// </summary>
        /// <param name="filter">Фильтр <see cref="DocFilter"/> списка документов.</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public GetDocumentsResponse GetOutcomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<GetDocumentsResponse>("documents/outcome", new
            {
                filter = filter,
                start_from = startFrom,
                count = count
            });
        }

        /// <summary>
        /// 5.8. Получение списка входящих документов
        /// </summary>
        /// <param name="filter">Фильтр <see cref="DocFilter"/> списка документов.</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public GetDocumentsResponse GetIncomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<GetDocumentsResponse>("documents/income", new
            {
                filter = filter,
                start_from = startFrom,
                count = count
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

        /// <summary>
        /// 5.10. Получение документа по идентификатору
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public string GetDocument(string documentId)
        {
            var docLink = Get<GetDocumentResponse>("/documents/download/" + documentId);
            return Get(docLink.Link);
        }

        /// <summary>
        /// 5.11. Получение списка документов по идентификатору запроса
        /// </summary>
        /// <param name="requestId">Идентификатор запроса</param>
        public GetDocumentsResponse GetDocuments(string requestId)
        {
            return Get<GetDocumentsResponse>("documents/request/" + requestId);
        }

        /// <summary>
        /// 5.12. Получение квитанции по номеру исходящего документа
        /// </summary>
        /// <param name="requestId">Идентификатор документа</param>
        public string GetTicket(string documentId)
        {
            var link = Get<GetDocumentResponse>("documents/" + documentId + "/ticket");
            return Get(link.Link);
        }

        /// <summary>
        /// 5.13. Получение электронной подписи исходящего документа
        /// </summary>
        /// <param name="requestId">Идентификатор документа</param>
        public string GetSignature(string documentId)
        {
            return Get("documents/" + documentId + "/signature", accept: "text/plain");
        }
    }
}
