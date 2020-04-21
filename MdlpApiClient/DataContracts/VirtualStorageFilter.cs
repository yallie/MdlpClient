namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.11.1. Фильтрация по реестру виртуального склада
    /// Структура данных VirtualStorageFilter
    /// </summary>
    [DataContract]
    public class VirtualStorageFilter
    {
        /// <summary>
        /// Идентификатор МД/МОХ (обязательно)
        /// </summary>
        [DataMember(Name = "storage_id", IsRequired = true)]
        public string StorageID { get; set; }

        /// <summary>
        /// Начало выбранного периода
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Конец выбранного периода
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name", IsRequired = false)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Наименование держателя РУ
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }
    }
}
