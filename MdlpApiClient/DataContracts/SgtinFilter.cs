namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.39. Формат объекта SgtinsFilter
    /// Таблица 35. Формат объекта SgtinsFilter
    /// 8.3.1. Метод для поиска по реестру КИЗ.
    /// </summary>
    [DataContract]
    public class SgtinFilter
    {
        /// <summary>
        /// Статус (см. Список возможных статусов КИЗ, <see cref="SgtinStatus"/>)
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public string[] Status { get; set; }

        /// <summary>
        /// Тип эмиссии:
        /// 1 — собственное,
        /// 2 — контрактное,
        /// 3 — иностранное производство,
        /// 4 — маркирован на таможне.
        /// См. <see cref="SgtinEmissionType"/>.
        /// </summary>
        [DataMember(Name = "emission_type", IsRequired = false)]
        public int[] EmissionType { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string ProductSellingName { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = false)]
        public string Sgtin { get; set; }

        /// <summary>
        /// SSCC (Идентификатор третичной упаковки)
        /// </summary>
        [DataMember(Name = "pack3_id", IsRequired = false)]
        public string Sscc { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "sys_id", IsRequired = false)]
        public string SystemSubjectID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        /// <summary>
        /// Дата упаковки, начала временного диапазона — дата ввода в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_from", IsRequired = false)]
        public DateTime? ReleaseDateFrom { get; set; }

        /// <summary>
        /// Дата упаковки, конец временного диапазона — дата окончания в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_to", IsRequired = false)]
        public DateTime? ReleaseDateTo { get; set; }

        /// <summary>
        /// Дата начала периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_from", IsRequired = false)]
        public DateTime? EmissionDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_to", IsRequired = false)]
        public DateTime? EmissionDateTo { get; set; }

        /// <summary>
        /// Дата начала периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_from", IsRequired = false)]
        public DateTime? LastTracingDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_to", IsRequired = false)]
        public DateTime? LastTracingDateTo { get; set; }

        /// <summary>
        /// Источник финансирования.
        /// Возможные значения см. в XSD описании базовых типов комплекта схем:
        /// 1 — собственные средства,
        /// 2 — средства федерального бюджета,
        /// 3 — средства регионального бюджета
        /// </summary>
        [DataMember(Name = "source_type", IsRequired = false)]
        public int[] SourceType { get; set; }

        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid
        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

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
