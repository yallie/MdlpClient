namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.5.3. Публичная информация о производимых ЛП
    /// 8.5.4. Публичная информация о производимом ЛП
    /// </summary>
    /// <remarks>
    /// Содержит подмножество полей структур <see cref="GtinInfo"/> и <see cref="MedProduct"/>.
    /// </remarks>
    [DataContract]
    public class MedProductPublic
    {
        /// <summary>
        /// Статус рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_status")]
        public string RegistrationStatus { get; set; }

        /// <summary>
        /// Номер рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        [DataMember(Name = "reg_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name")]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП
        /// </summary>
        [DataMember(Name = "drug_code")]
        public string DrugCode { get; set; }

        /// <summary>
        /// Версия внутреннего уникального идентификатора лекарственного препарата в реестре ЕСКЛП
        /// 1 — устаревшие, 2 — актуальные данные
        /// </summary>
        [DataMember(Name = "drug_code_version", IsRequired = false)]
        public int? DrugCodeVersion { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// </summary>
        [DataMember(Name = "type_form")]
        public string TypeForm { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp")]
        public bool Gnvlp { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug")]
        public bool VznDrug { get; set; }

        /// <summary>
        /// Наименование товара на этикетке
        /// Например: лиофилизат для приготовления концентрата для приготовления раствора для инфузий "гертикад®" 150 мг, 440 мг
        /// </summary>
        [DataMember(Name = "prod_desc", IsRequired = false)]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name")]
        public string ProductDosageName { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена
        /// </summary>
        [DataMember(Name = "cost_limit", IsRequired = false)]
        public string CostLimit { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name")]
        public string ProductFormName { get; set; }

        /// <summary>
        /// Производитель готовой ЛФ
        /// </summary>
        [DataMember(Name = "glf_name", IsRequired = false)]
        public string FormProducerName { get; set; }

        /// <summary>
        /// Страна регистрации производителя готовой ЛФ
        /// </summary>
        [DataMember(Name = "glf_country", IsRequired = false)]
        public string FormProducerCountry { get; set; }

        /// <summary>
        /// Первичная упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack_1_name")]
        public string ProductPack1Name { get; set; }

        /// <summary>
        /// Количество массы/объема в первичной упаковке
        /// </summary>
        [DataMember(Name = "prod_pack_1_ed")]
        public string ProductPack1Amount { get; set; }

        /// <summary>
        /// Количество (мера, ед.измерения) массы/объема в первичной упаковке
        /// </summary>
        [DataMember(Name = "prod_pack1_ed_name")]
        public string ProductPack1AmountName { get; set; }
    }
}
