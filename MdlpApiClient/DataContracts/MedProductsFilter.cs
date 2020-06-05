namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.42. Формат объекта MedProductsFilter
    /// Таблица 38. Формат объекта MedProductsFilter
    /// 8.5.1. Метод для получения информации из реестра производимых организацией ЛП
    /// </summary>
    [DataContract]
    public class MedProductsFilter
    {
        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// Дата гос. регистрации, начальная дата
        /// </summary>
        [DataMember(Name = "reg_date_from", IsRequired = false)]
        public DateTime? RegistrationDateFrom { get; set; }

        /// <summary>
        /// Дата гос. регистрации, конечная дата
        /// </summary>
        [DataMember(Name = "reg_date_to", IsRequired = false)]
        public DateTime? RegistrationDateTo { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_id", IsRequired = false)]
        public string RegistrationID { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name", IsRequired = false)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП
        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Наименование держателя РУ
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Производитель готовой ЛФ
        /// </summary>
        [DataMember(Name = "glf_name", IsRequired = false)]
        public string FormProducerName { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug", IsRequired = false)]
        public bool? VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp", IsRequired = false)]
        public bool? Gnvlp { get; set; }
    }
}
