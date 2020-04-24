namespace MdlpApiClient
{
    using MdlpApiClient.Xsd;
    using MdlpApiClient.Serialization;
    using System.Text;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 5: documents.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 5.1. Отправка объекта документа
        /// </summary>
        /// <param name="doc">Объект документа</param>
        /// <returns>Идентификатор документа</returns>
        public string SendDocument(Documents doc)
        {
            if (!LargeDocumentSize.HasValue)
            {
                LargeDocumentSize = GetLargeDocumentSize();
            }

            // serialize the document and estimate data packet size
            var xml = XmlSerializationHelper.Serialize(doc, ApplicationName);
            var xmlBytes = Encoding.UTF8.GetByteCount(xml);
            var xmlBase64 = 4 * xmlBytes / 3;
            var overhead = 1024; // requestId + JSON serialization overhead
            var totalSize = xmlBase64 + SignatureSize + overhead;

            // prefer SendDocument for small documents
            if (totalSize < LargeDocumentSize)
            {
                return SendDocument(xml);
            }

            return SendLargeDocument(xml);
        }

        /// <summary>
        /// Ограничение на размер документа, который можно отсылать методом SendDocument.
        /// </summary>
        public int? LargeDocumentSize { get; set; }

        /// <summary>
        /// Приблизительный размер подписи для оценки размера отсылаемого пакета.
        /// Размер сигнатуры вычисляется при аутентификации резидента.
        /// </summary>
        public int SignatureSize { get; set; }

        /// <summary>
        /// 5.10. Получение объекта документа по идентификатору
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public Documents GetDocument(string documentId)
        {
            var xml = GetDocumentText(documentId);
            return XmlSerializationHelper.Deserialize(xml);
        }

        /// <summary>
        /// 5.12. Получение объекта квитанции по номеру исходящего документа
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public Documents GetTicket(string documentId)
        {
            var xml = GetTicketText(documentId);
            return XmlSerializationHelper.Deserialize(xml);
        }
    }
}
