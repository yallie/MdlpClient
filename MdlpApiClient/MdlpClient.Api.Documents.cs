namespace MdlpApiClient
{
    using MdlpApiClient.Xsd;
    using MdlpApiClient.Serialization;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 5: documents.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 5.1. Отправка объекта документа
        /// </summary>
        /// <param name="doc">Объект документа.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendDocument(Documents doc)
        {
            return SendDocument(XmlSerializationHelper.Serialize(doc));
        }

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
