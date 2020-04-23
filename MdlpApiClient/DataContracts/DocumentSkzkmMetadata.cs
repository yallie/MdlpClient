namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.14. Прослеживание документов по отчёту из СУЗ
    /// </summary>
    [DataContract]
    public class DocumentSkzkmMetadata
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
        /// Тип документа. Соответствует номеру схемы XSD
        /// </summary>
        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        /// <summary>
        /// Статус документа.
        /// </summary>
        [DataMember(Name = "processing_document_status")]
        public string ProcessingDocStatus { get; set; } // "PROCESSING",

        /// <summary>
        /// Дата обработки документа
        /// </summary>
        [DataMember(Name = "processed_date")]
        public DateTime ProcessedDate { get; set; } // "2017-11-23 05:48:15",

        /// <summary>
        /// Количество КИЗ
        /// </summary>
        [DataMember(Name = "sgtin_count")]
        public int SgtinCount { get; set; } // 10
    }
}
