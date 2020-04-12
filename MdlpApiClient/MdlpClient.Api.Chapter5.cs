namespace MdlpApiClient
{
    using DataContracts;

    /// <remarks>
    /// This file contains strongly typed REST API methods.
    /// </remarks>
    partial class MdlpClient
    {
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
