namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.32. Формат объекта PublicSGTIN
    /// 8.3.3. Метод поиска по общедоступному реестру КИЗ по списку значений
    /// </summary>
    [DataContract]
    public class PublicSgtin
    {
        /// <summary>
        /// SGTIN (КИЗ) 
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        [DataMember(Name = "expiration_date", IsRequired = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Название препарата.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
        public string ProdFormName { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name", IsRequired = false)]
        public string ProductDosageName { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        [DataMember(Name = "reg_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Держатель регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }
    }
}
