namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.32. Формат объекта SGTIN
    /// </summary>
    [DataContract]
    public class Sgtin
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// ИНН владельца
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// SGTIN (КИЗ) 
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string SgtinValue { get; set; }

        /// <summary>
        /// Статус (см. Список возможных статусов КИЗ)
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Дата последней смены статуса
        /// </summary>
        [DataMember(Name = "status_date")]
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Наименование владельца
        /// </summary>
        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Тип эмиссии: 1 — собственное, 2 — контрактное, 3 — иностранное производство
        /// </summary>
        [DataMember(Name = "emission_type")]
        public int EmissionType { get; set; }

        /// <summary>
        /// Дата ввода в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Дата начала периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date")]
        public DateTime EmissionDate { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Местонахождение ЛП — название субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_name")]
        public string FederalSubjectName { get; set; }

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
        /// Полное наименование товара
        /// Например: лиофилизат для приготовления концентрата для приготовления раствора для инфузий "гертикад®" 150 мг, 440 мг
        /// </summary>
        [DataMember(Name = "full_prod_name", IsRequired = false)]
        public string FullProductName { get; set; }

        /// <summary>
        /// Держатель регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Полное наименование товара (что это за чертовщина?)
        /// </summary>
        [DataMember(Name = "pack1_desc", IsRequired = false)]
        public string Pack1Desc { get; set; }

        /// <summary>
        /// SSCC (Идентификатор третичной упаковки)
        /// </summary>
        [DataMember(Name = "pack3_id", IsRequired = false)]
        public string Sscc { get; set; }

        /// <summary>
        /// Дата выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date", IsRequired = false)]
        public DateTime? LastTracingDate { get; set; }

        /// <summary>
        /// Источник финансирования.
        /// Возможные значения см. в XSD описании базовых типов комплекта схем.
        /// </summary>
        [DataMember(Name = "source_type", IsRequired = false)]
        public int? SourceType { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
        public string ProductFormName { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name", IsRequired = false)]
        public string ProductDosageName { get; set; }

        /// <summary>
        /// Идентификатор места нахождения товара в ЗТК (в формате SysID)
        /// </summary>
        [DataMember(Name = "customs_point_id", IsRequired = false)]
        public string CustomsPointID { get; set; }

        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// Информация о биллинге        /// </summary>
        [DataMember(Name = "billing_info", IsRequired = false)]
        public SgtinBillingInformation BillingInfo { get; set; }

        /// <summary>
        /// Состояние оплаты SGTIN
        /// 0 — успешно оплачен
        /// 1 — выбран для перемещения в очередь на оплату
        /// 2 — помещается в очередь на оплату
        /// 3 — помещен в очередь на оплату
        /// 4 — не оплачен в установленные сроки
        /// </summary>
        [DataMember(Name = "billing_state", IsRequired = false)]
        public int? BillingState { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug")]
        public bool VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp")]
        public bool Gnvlp { get; set; }
    }
}
