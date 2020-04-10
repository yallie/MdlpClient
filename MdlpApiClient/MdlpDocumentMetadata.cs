namespace MdlpApiClient
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class MdlpDocumentMetadata
    {
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "sender")]
        public string SenderID { get; set; } // "935ba7bc-b022-11e7-abc4-cec278b6b50a",

        [DataMember(Name = "sys_id")]
        public string SystemID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        [DataMember(Name = "doc_status")]
        public string DocStatus { get; set; } // "PROCESSED_DOCUMENT",

        [DataMember(Name = "device_id")]
        public string DeviceID { get; set; } // 1230000011111111 (optional)

        [DataMember(Name = "skzkm_origin_msg_id")]
        public string SkzkmOriginMessageID { get; set; } // "e2cb20c1-1d5b-4ab6-b8dd-9297bec23f63" (optional)

        [DataMember(Name = "version")]
        public string Version { get; set; } // API version: "1.28"
    }
}
