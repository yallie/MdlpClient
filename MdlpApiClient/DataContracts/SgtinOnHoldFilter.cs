namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Метод для поиска по реестру КИЗ всех записей со статусом 'Оборот приостановлен'
    /// </summary>
    [DataContract]
    public class SgtinOnHoldFilter
    {
        /// <summary>
        /// ИНН владельца
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Тип эмиссии: 1 — собственное, 2 — контрактное, 3 — иностранное производство
        /// </summary>
        [DataMember(Name = "emission_type", IsRequired = false)]
        public int[] EmissionType { get; set; }

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
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»        /// </summary>
        [DataMember(Name = "sys_id", IsRequired = false)]
        public string SystemID { get; set; }

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
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// Информация о биллинге        /// </summary>
        [DataMember(Name = "billing_info", IsRequired = false)]
        public SgtinBillingInformation BillingInfo { get; set; }

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
