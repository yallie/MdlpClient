namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.12. Список статусов документа (DocStatusEnum)
    /// </summary>
    public class DocStatusEnum
    {
        /// <summary>
        /// Документ загружается
        /// </summary>
        public const string UPLOADING_DOCUMENT = "UPLOADING_DOCUMENT";

        /// <summary>
        /// Документ принят и обрабатывается трансформатором
        /// </summary>
        public const string PROCESSING_DOCUMENT = "PROCESSING_DOCUMENT";

        /// <summary>
        /// Документ обработан трансформатором и принят на обработку системой
        /// </summary>
        public const string CORE_PROCESSING_DOCUMENT = "CORE_PROCESSING_DOCUMENT";

        /// <summary>
        /// Документ обработан системой и трансформатор подготавливает ответ
        /// </summary>
        public const string CORE_PROCESSED_DOCUMENT = "CORE_PROCESSED_DOCUMENT";

        /// <summary>
        /// Документ обработан трансформатором и готов для загрузки
        /// </summary>
        public const string PROCESSED_DOCUMENT = "PROCESSED_DOCUMENT";

        /// <summary>
        /// Произошла ошибка во время обработки документа
        /// </summary>
        public const string FAILED = "FAILED";

        /// <summary>
        /// Произошла ошибка во время обработки документа.
        /// Квитанция для документа с информацией о причине сбоя 
        /// сформирована и может быть получена по request_id
        /// </summary>
        public const string FAILED_RESULT_READY = "FAILED_RESULT_READY";    }
}
