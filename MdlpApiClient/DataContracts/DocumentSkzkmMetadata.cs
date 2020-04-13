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
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        [DataMember(Name = "processing_document_status")]
        public string ProcessingDocStatus { get; set; } // "PROCESSING",

        [DataMember(Name = "processed_date")]
        public DateTime ProcessedDate { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "sgtin_count")]
        public int SgtinCount { get; set; } // 10
    }
}
