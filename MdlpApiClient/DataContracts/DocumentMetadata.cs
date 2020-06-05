namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.17. Формат объекта Document
    /// Таблица 13. Формат объекта Document
    /// 5.9. Получение метаданных документа
    /// </summary>
    [DataContract]
    public class DocumentMetadata
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        /// <summary>
        /// Дата получения документа
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        /// <summary>
        /// Дата обработки документа
        /// </summary>
        [DataMember(Name = "processed_date")]
        public DateTime ProcessedDate { get; set; } // "2017-11-23 05:48:15",

        /// <summary>
        /// Отправитель документа
        /// </summary>
        [DataMember(Name = "sender")]
        public string SenderID { get; set; } // "935ba7bc-b022-11e7-abc4-cec278b6b50a",

        /// <summary>
        /// Получатель документа
        /// </summary>
        [DataMember(Name = "receiver")]
        public string ReceiverID { get; set; } // "935ba7bc-b022-11e7-abc4-cec278b6b50a",

        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "sys_id")]
        public string SystemSubjectID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        /// <summary>
        /// Тип документа. Соответствует номеру схемы XSD
        /// </summary>
        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 311,

        /// <summary>
        /// Статус документа. См. <see cref="DocStatusEnum"/>.
        /// </summary>
        [DataMember(Name = "doc_status")]
        public string DocStatus { get; set; } // "PROCESSED_DOCUMENT",

        /// <summary>
        /// Версия документа
        /// </summary>
        [DataMember(Name = "version")]
        public string Version { get; set; } // API version: "1.28"
    }
}
