namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.45. Статусы кодов маркировки в производственной серии
    /// Таблица 41. Статусы кодов маркировки в производственной серии
    /// </summary>
    public class BatchStatus
    {
        /// <summary>
        /// Производство.
        /// </summary>
        public const string PRODUCTION = "PRODUCTION";

        /// <summary>
        /// Импорт.
        /// </summary>
        public const string IMPORT = "IMPORT";

        /// <summary>
        /// Закупка в России.
        /// </summary>
        public const string PURCHASE_IN_RUSSIA = "PURCHASE_IN_RUSSIA";

        /// <summary>
        /// Розничные продажи.
        /// </summary>
        public const string RETAIL_SALE = "RETAIL_SALE";

        /// <summary>
        /// Отпуск по льготным рецептам.
        /// </summary>
        public const string DISCOUNT_SALE = "DISCOUNT_SALE";

        /// <summary>
        /// Отпуск для оказания мед. помощи.
        /// </summary>
        public const string MEDICAL_USE = "MEDICAL_USE";

        /// <summary>
        /// Оптовые продажи.
        /// </summary>
        public const string WHOLESALE = "WHOLESALE";

        /// <summary>
        /// Прочий вывод.
        /// </summary>
        public const string OTHER = "OTHER";
    }
}
