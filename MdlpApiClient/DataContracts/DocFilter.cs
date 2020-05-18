namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.18. Формат объекта DocFilter
    /// Содержит информацию для фильтрации списка документов.
    /// </summary>
    [DataContract]
    public class DocFilter
    {
        /// <summary>
        /// Дата начала периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public CustomDateTimeSpace StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public CustomDateTimeSpace EndDate { get; set; }

        /// <summary>
        /// Дата обработки документа: начало периода
        /// </summary>
        [DataMember(Name = "processed_date_from", IsRequired = false)]
        public CustomDateTimeSpace ProcessedDateFrom { get; set; }

        /// <summary>
        /// Дата обработки документа: окончание периода
        /// </summary>
        [DataMember(Name = "processed_date_to", IsRequired = false)]
        public CustomDateTimeSpace ProcessedDateTo { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        [DataMember(Name = "document_id", IsRequired = false)]
        public string DocumentID { get; set; }

        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [DataMember(Name = "request_id", IsRequired = false)]
        public string RequestID { get; set; }

        /// <summary>
        /// Уникальный идентификатор отправителя.
        /// Идентификатор места осуществления деятельности, места ответственного
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "sender_id", IsRequired = false)]
        public string SenderID { get; set; }

        /// <summary>
        /// Уникальный идентификатор получателя.
        /// Идентификатор места осуществления деятельности, места ответственного
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// Применимо для входящих документов.
        /// </summary>
        [DataMember(Name = "receiver_id", IsRequired = false)]
        public string ReceiverID { get; set; }

        /// <summary>
        /// Тип документа. Соответствует номеру схемы XSD.
        /// </summary>
        [DataMember(Name = "doc_type", IsRequired = false)]
        public int? DocType { get; set; } // тут передается action_id: 311, 601, etc

        /// <summary>
        /// Статус документа. См. <see cref="DocStatusEnum"/>.
        /// </summary>
        [DataMember(Name = "doc_status", IsRequired = false)]
        public string DocStatus { get; set; }

        /// <summary>
        /// Тип загрузки в систему:
        /// 0 — УСО
        /// 1 — ЛК (личный кабинет)
        /// 2 — API
        /// 3 — ОФД (Оператор фискальных данных)
        /// 4 — СКЗКМ/ИС МП
        /// </summary>
        [DataMember(Name = "file_uploadtype", IsRequired = false)]
        public int? FileUploadType { get; set; } // 1 УСО, 2 ЛК, 3 API, 4 ОФД, 5 СКЗКМ

        /// <summary>
        /// Идентификатор отчета СУЗ.
        /// Для документов, полученных от регистраторов событий
        /// </summary>
        [DataMember(Name = "skzkm_report_id", IsRequired = false)]
        public string SkzkmReportID { get; set; }
    }
}
