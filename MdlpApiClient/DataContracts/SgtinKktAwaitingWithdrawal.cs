namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. КИЗ, ожидающий вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// </summary>
    [DataContract]
    public class SgtinKktAwaitingWithdrawal
    {
        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Тип реализации
        /// 0 — розничная продажа
        /// 1 — отпуск по льготному рецепту
        /// </summary>
        [DataMember(Name = "sold_type")]
        public int SoldType { get; set; }

        /// <summary>
        /// Статус обработки
        /// 0 — принято
        /// 1 — в обработке
        /// 2 — завершено
        /// 3 — завершено с ошибкой
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Дата операции из чека
        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// ИНН из чека
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// Статус последней проверки
        /// </summary>
        [DataMember(Name = "last_check_status", IsRequired = false)]
        public LastCheckStatus LastCheckStatus { get; set; }

        /// <summary>
        /// Розничная цена, в коп. (обязательно при SoldType = 0)
        /// </summary>
        [DataMember(Name = "price", IsRequired = false)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Сумма НДС (если сделка облагается НДС), в коп.        /// </summary>
        [DataMember(Name = "vat_value", IsRequired = false)]
        public decimal? VatValue { get; set; }

        /// <summary>
        /// Доля от вторичной упаковки (доля вида 1/2)
        /// </summary>
        [DataMember(Name = "sold_part", IsRequired = false)]
        public string SoldPart { get; set; }

        /// <summary>
        /// Сумма скидки, в коп.        /// </summary>
        [DataMember(Name = "discount", IsRequired = false)]
        public decimal? Discount { get; set; }

        /// <summary>
        /// Номер льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_num", IsRequired = false)]
        public string PrescriptionNumber { get; set; }

        /// <summary>
        /// Дата льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_num", IsRequired = false)]
        public DateTime? PrescriptionDate { get; set; }

        /// <summary>
        /// Серия льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_series", IsRequired = false)]
        public string PrescriptionSeries { get; set; }

        /// <summary>
        /// Уникальный идентификатор РЭ или РВ
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Уникальный идентификатор системы, сформировавшей сообщение
        /// </summary>
        [DataMember(Name = "skzkm_origin_msg_id", IsRequired = false)]
        public string SkskmOriginMessageID { get; set; }

        /// <summary>
        /// Идентификатор организации-отправителя
        /// </summary>
        [DataMember(Name = "subject_id", IsRequired = false)]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// Идентификатор XML-документа
        /// </summary>
        [DataMember(Name = "xml_document_id", IsRequired = false)]
        public string XmlDocumentID { get; set; }

        /// <summary>
        /// Дата фактического получения чека в системе
        /// </summary>
        [DataMember(Name = "op_exec_date")]
        public DateTime OperationExecutionDate { get; set; }
    }
}
