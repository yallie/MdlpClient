namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.11.1. Фильтрация по реестру виртуального склада
    /// Структура данных VirtualStorageEntry
    /// </summary>
    [DataContract]
    public class VirtualStorageEntry
    {
        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = true)]
        public string Gtin { get; set; }

        /// <summary>
        /// Идентификатор МД/МОХ
        /// </summary>
        [DataMember(Name = "storage_id", IsRequired = true)]
        public string StorageID { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name", IsRequired = true)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = true)]
        public string ProductName { get; set; }

        /// <summary>
        /// Наименование держателя РУ
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = true)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Приход (всего), шт
        /// </summary>
        [DataMember(Name = "total_income", IsRequired = true)]
        public long TotalIncome { get; set; }

        /// <summary>
        /// Выбытие (всего), шт
        /// </summary>
        [DataMember(Name = "total_outcome", IsRequired = true)]
        public long TotalOutcome { get; set; }

        /// <summary>
        /// Розничные продажи (выбытие), шт
        /// </summary>
        [DataMember(Name = "retail_sale", IsRequired = true)]
        public long RetailSale { get; set; }

        /// <summary>
        /// Отпуск по льготному рецепту (выбытие), шт
        /// </summary>
        [DataMember(Name = "discount_sale", IsRequired = true)]
        public long DiscountSale { get; set; }

        /// <summary>
        /// Медицинское применение (выбытие), шт
        /// </summary>
        [DataMember(Name = "medical_use", IsRequired = true)]
        public long MedicalUse { get; set; }

        /// <summary>
        /// Оптовые продажи (выбытие), шт
        /// </summary>
        [DataMember(Name = "wholesale", IsRequired = true)]
        public long Wholesale { get; set; }

        /// <summary>
        /// Прочее (выбытие), шт
        /// </summary>
        [DataMember(Name = "other", IsRequired = true)]
        public long OtherOutcome { get; set; }

        /// <summary>
        /// Производство (приход), шт
        /// </summary>
        [DataMember(Name = "production", IsRequired = true)]
        public long Production { get; set; }

        /// <summary>
        /// Закупка в РФ (приход), шт
        /// </summary>
        [DataMember(Name = "purchase_in_russia", IsRequired = true)]
        public long PurchaseInRussia { get; set; }

        /// <summary>
        /// Импорт (приход), шт
        /// </summary>
        [DataMember(Name = "import", IsRequired = true)]
        public long Import { get; set; }

        /// <summary>
        /// Баланс на входе, шт
        /// </summary>
        [DataMember(Name = "opening_balance", IsRequired = true)]
        public long OpeningBalance { get; set; }

        /// <summary>
        /// Баланс на выходе, шт
        /// </summary>
        [DataMember(Name = "ending_balance", IsRequired = true)]
        public long EndingBalance { get; set; }
    }
}
