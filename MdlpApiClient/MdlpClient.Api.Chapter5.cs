namespace MdlpApiClient
{
    using DataContracts;
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using MdlpApiClient.Toolbox;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 5: documents.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 5.1. Отправка документа
        /// </summary>
        /// <remarks>
        /// Метод подходит для маленьких документов
        /// </remarks>
        /// <param name="xmlDocument">Документ в формате XML.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendDocument(string xmlDocument)
        {
            RequestRate(0.5); // 1

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
            RequestRate(0.5); // 2, 3, 4

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
            RequestRate(0.5); // 6

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
            RequestRate(0.5); // 5

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
        public DocumentsResponse<OutcomeDocument> GetOutcomeDocuments(DocFilter filter, int startFrom, int count)
        {
            RequestRate(1); // 7

            return Post<DocumentsResponse<OutcomeDocument>>("documents/outcome", new
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
        public DocumentsResponse<IncomeDocument> GetIncomeDocuments(DocFilter filter, int startFrom, int count)
        {
            RequestRate(1); // 8

            return Post<DocumentsResponse<IncomeDocument>>("documents/income", new
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
            RequestRate(0.5); // 9

            return Get<DocumentMetadata>("documents/{document_id}", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 5.10. Получение текста документа по идентификатору
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public string GetDocumentText(string documentId)
        {
            RequestRate(0.5); // 10

            var docLink = Get<GetDocumentResponse>("/documents/download/{document_id}", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });

            return Get(docLink.Link);
        }

        /// <summary>
        /// 5.11. Получение списка документов по идентификатору запроса
        /// </summary>
        /// <param name="requestId">Идентификатор запроса</param>
        public DocumentsResponse<DocumentMetadata> GetDocumentsByRequestID(string requestId)
        {
            RequestRate(0.5); // 11

            return Get<DocumentsResponse<DocumentMetadata>>("documents/request/{request_id}", new[]
            {
                new Parameter("request_id", requestId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 5.12. Получение текста квитанции по номеру исходящего документа
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public string GetTicketText(string documentId)
        {
            RequestRate(0.5); // 12

            var link = Get<GetDocumentResponse>("documents/{document_id}/ticket", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });

            return Get(link.Link);
        }

        /// <summary>
        /// 5.13. Получение электронной подписи исходящего документа
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public string GetSignature(string documentId)
        {
            RequestRate(0.5); // 13

            return Get("documents/{document_id}/signature", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
                new Parameter("Accept", "text/plain", ParameterType.HttpHeader),
            });
        }

        /// <summary>
        /// 5.14. Прослеживание документов по отчёту из СУЗ
        /// </summary>
        /// <param name="reportId">Идентификатор отчета СУЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public ItemsResponse<DocumentSkzkmMetadata> GetDocumentsBySkzkmReportID(string reportId, int startFrom, int count)
        {
            RequestRate(1); // 98

            return Post<ItemsResponse<DocumentSkzkmMetadata>>("documents/skzkm-traces/filter", new
            {
                filter = new
                {
                    skzkm_report_id = reportId,
                },
                start_from = startFrom,
                count = count,
            });
        }
    }
}
