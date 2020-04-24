namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.10.1. Фильтрация по реестру ЕСКЛП (Единый справочник-каталог лекарственных препаратов)
    /// Структура данных EsklpFilter, немного похоже на <see cref="MedProductsFilter"/>.
    /// </summary>
    [DataContract]
    public class EsklpFilter
    {
        /// <summary>
        /// Дата гос. регистрации, начальная дата
        /// </summary>
        [DataMember(Name = "REG_DATE", IsRequired = false)]
        public CustomDateTime RegistrationDateFrom { get; set; }

        /// <summary>
        /// Дата гос. регистрации, конечная дата
        /// </summary>
        [DataMember(Name = "REG_END_DATE", IsRequired = false)]
        public CustomDateTime RegistrationDateTo { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "reg_id", IsRequired = false)]
        public string RegistrationID { get; set; }

        /// <summary>
        /// Наименование держателя регистрационного удостоверения
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "REG_HOLDER", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Код налогоплательщика держателя держателя регистрационного удостоверения для резидентов РФ
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "REG_HOLDER_CODE", IsRequired = false)]
        public string RegistrationHolderCode { get; set; }

        /// <summary>
        /// Статус действия регистрационного удостоверения
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "REG_STATUS", IsRequired = false)]
        public string RegistrationStatus { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "PROD_SELL_NAME", IsRequired = false)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        /// <remarks>
        /// Частичное вхождение, без учета регистра
        /// </remarks>
        [DataMember(Name = "PROD_NAME", IsRequired = false)]
        public string ProductName { get; set; }
    }
}
