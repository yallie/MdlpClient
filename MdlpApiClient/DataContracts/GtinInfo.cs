namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Подробности о ЛП (GTIN)
    /// </summary>
    [DataContract]
    public class GtinInfo
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

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
        /// Дата окончания рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_end_date", IsRequired = false)]
        public DateTime? RegistrationEndDate { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// </summary>
        [DataMember(Name = "type_form")]
        public string TypeForm { get; set; }

        /// <summary>
        /// Количество массы/объема в первичной упаковке
        /// </summary>
        [DataMember(Name = "prod_pack_1_ed")]
        public string ProductPack1Amount { get; set; }

        /// <summary>
        /// Количество (мера, ед.измерения) массы/объема в первичной упаковке (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack1_ed_name")]
        public string ProductPack1AmountName { get; set; }

        /// <summary>
        /// Адрес упаковщика
        /// </summary>
        [DataMember(Name = "packer_address")]
        public string PackerAddress { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name", IsRequired = false)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Содержимое лекарственного препарата
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_content", IsRequired = false)]
        public string ProductContent { get; set; }

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
        /// Первичная упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack_1_name")]
        public string ProductPack1Name { get; set; }

        /// <summary>
        /// Вторичная (потребительская) упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack_2_name")]
        public string ProductPack2Name { get; set; }

        /// <summary>
        /// Количество первичной упаковки в потребительской упаковке
        /// </summary>
        [DataMember(Name = "prod_pack_1_2")]
        public string ProductPack1InPack2 { get; set; }

        /// <summary>
        /// Код ТНВЭД ЕАЭС
        /// </summary>
        [DataMember(Name = "tn_ved")]
        public string Tnved { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp")]
        public bool Gnvlp { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена (для ЖНВЛП) (руб)
        /// </summary>
        [DataMember(Name = "max_gnvlp", IsRequired = false)]
        public string MaxGnvlpPrice { get; set; }

        /// <summary>
        /// Дата регистрации предельной цены
        /// </summary>
        [DataMember(Name = "max_gnvlp_reg_date", IsRequired = false)]
        public DateTime? MaxGnvlpPriceRegistrationDate { get; set; }

        /// <summary>
        /// Наименование держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder")]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Страна регистрации держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_country")]
        public string RegistrationHolderCountry { get; set; }

        /// <summary>
        /// Статус лекарственного препарата
        /// </summary>
        [DataMember(Name = "prod_status")]
        public string ProductStatus { get; set; }

        /// <summary>
        /// Признак регистрации в Минздраве
        /// </summary>
        [DataMember(Name = "min_zdrav")]
        public bool MinZdrav { get; set; }

        /// <summary>
        /// Признак регистрации в ГС1
        /// </summary>
        [DataMember(Name = "gs1")]
        public bool Gs1 { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена
        /// </summary>
        [DataMember(Name = "cost_limit", IsRequired = false)]
        public string CostLimit { get; set; }

        /// <summary>
        /// ИНН держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_inn")]
        public string RegistrationHolderInn { get; set; }

        /// <summary>
        /// Комплектность
        /// </summary>
        [DataMember(Name = "completeness", IsRequired = false)]
        public string Сompleteness { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
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
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП
        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Версия внутреннего уникального идентификатора лекарственного препарата в реестре ЕСКЛП
        /// 1 — устаревшие, 2 — актуальные данные
        /// </summary>
        [DataMember(Name = "drug_code_version", IsRequired = false)]
        public int? DrugCodeVersion { get; set; }
    }
}
