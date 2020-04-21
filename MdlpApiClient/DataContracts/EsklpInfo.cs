namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.10.1. Фильтрация по реестру ЕСКЛП (Единый справочник-каталог лекарственных препаратов)
    /// Структура данных EsklpInfo, много пересечений с <see cref="MedProduct"/> и <see cref="GtinInfo"/>.
    /// </summary>
    [DataContract]
    public class EsklpInfo
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public string ID { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_id", IsRequired = true)]
        public string RegistrationID { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "PROD_NAME", IsRequired = true)]
        public string ProductName { get; set; }

        /// <summary>
        /// Какой-то код продукта?
        /// Например: "1379"
        /// </summary>
        /// <remarks>
        /// Ошибка в документации. Там сказано, что это международное непатентованное наименование
        /// </remarks>
        [DataMember(Name = "PROD_ID", IsRequired = true)]
        public string ProductID { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "PROD_SELL_NAME", IsRequired = true)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// Страна регистрации держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "REG_COUNTRY", IsRequired = true)]
        public string RegistrationCountry { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        [DataMember(Name = "REG_DATE", IsRequired = true)]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Наименование держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "REG_HOLDER", IsRequired = true)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Код налогоплательщика держателя держателя 
        /// регистрационного удостоверения для резидентов РФ
        /// </summary>
        [DataMember(Name = "REG_HOLDER_CODE", IsRequired = false)]
        public string RegistrationHolderCode { get; set; }

        /// <summary>
        /// Код налогоплательщика держателя держателя 
        /// регистрационного удостоверения в стране регистрации или его аналог
        /// </summary>
        [DataMember(Name = "REG_HOLDER_CODE_F", IsRequired = false)]
        public string RegistrationHolderForeignCode { get; set; }

        /// <summary>
        /// Наименование производителя стадии выпускающего контроля
        /// </summary>
        [DataMember(Name = "PROD_PACK_1", IsRequired = true)]
        public string ProductPack1ProducerName { get; set; }

        /// <summary>
        /// Первичная упаковка
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_ID", IsRequired = true)]
        public string ProductPack1ID { get; set; }

        /// <summary>
        /// Первичная упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_NAME", IsRequired = true)]
        public string ProductPack1Name { get; set; }

        /// <summary>
        /// Масса/объем в первичной упаковке
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_SIZE")]
        public string ProductPack1Size { get; set; }

        /// <summary>
        /// Количество массы/объема в первичной упаковке
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_ED")]
        public string ProductPack1Amount { get; set; }

        /// <summary>
        /// Количество (мера, ед.измерения) массы/объема в первичной упаковке (строковое представление)
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_ED_NAME")]
        public string ProductPack1AmountName { get; set; }

        /// <summary>
        /// Вторичная (потребительская) упаковка
        /// </summary>
        [DataMember(Name = "PROD_PACK_2_ID", IsRequired = true)]
        public string ProductPack2ID { get; set; }

        /// <summary>
        /// Вторичная (потребительская) упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "PROD_PACK_2_NAME", IsRequired = true)]
        public string ProductPack2Name { get; set; }

        /// <summary>
        /// Количество первичной упаковки в потребительской упаковке
        /// </summary>
        [DataMember(Name = "PROD_PACK_1_2")]
        public string ProductPack1InPack2 { get; set; }

        /// <summary>
        /// Код лекарственной формы
        /// </summary>
        [DataMember(Name = "PROD_D", IsRequired = false)]
        public string ProductFormID { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "PROD_D_NAME", IsRequired = false)]
        public string ProductDosageName { get; set; }

        /// <summary>
        /// Лекарственная форма (строковое представление)
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "PROD_FORM_NAME")]
        public string ProductFormName { get; set; }

        /// <summary>
        /// Код ТНВЭД ЕАЭС
        /// </summary>
        [DataMember(Name = "TN_VED")]
        public string Tnved { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "GNVLP")]
        public bool Gnvlp { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена (для ЖНВЛП) (руб)
        /// </summary>
        [DataMember(Name = "MAX_GNVLP", IsRequired = false)]
        public string MaxGnvlpPrice { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП
        /// </summary>
        [DataMember(Name = "DRUG_CODE", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Версия внутреннего уникального идентификатора лекарственного препарата в реестре ЕСКЛП
        /// 1 — устаревшие,
        /// 2 — актуальные данные
        /// </summary>
        [DataMember(Name = "DRUG_CODE_VERSION", IsRequired = false)]
        public int? DrugCodeVersion { get; set; }

        /// <summary>
        /// Статус действия регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "REG_STATUS", IsRequired = false)]
        public string RegistrationStatus { get; set; }

        /// <summary>
        /// Комплектность
        /// </summary>
        [DataMember(Name = "COMPLETENESS", IsRequired = false)]
        public string Сompleteness { get; set; }

        /// <summary>
        /// Производитель готовой ЛФ
        /// </summary>
        [DataMember(Name = "GLF_NAME", IsRequired = false)]
        public string FormProducerName { get; set; }

        /// <summary>
        /// Страна регистрации производителя готовой ЛФ
        /// </summary>
        [DataMember(Name = "GLF_COUNTRY", IsRequired = false)]
        public string FormProducerCountry { get; set; }
    }
}
