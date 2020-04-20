namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.5.1. Получение объекта ФИАС по идентификатору адресного объекта
    /// Формат объекта FiasAddressObject
    /// </summary>
    [DataContract]
    public class FiasAddressObject
    {
        [DataMember(Name = "REGIONCODE")]
        public string RegionCode { get; set; } // "01"

        [DataMember(Name = "IFNSUL")]
        public string IfnsUl { get; set; } // "0101",

        [DataMember(Name = "IFNSFL")]
        public string IfnsFl { get; set; } // "0101",

        [DataMember(Name = "CURRSTATUS")]
        public string CurrStatus { get; set; } // "0",

        [DataMember(Name = "CENTSTATUS")]
        public string CentStatus { get; set; } // "0",

        [DataMember(Name = "OFFNAME")]
        public string OffName { get; set; } // "Широкая",

        [DataMember(Name = "SHORTNAME")]
        public string ShortName { get; set; } // "ул",

        [DataMember(Name = "_id")]
        public string ID { get; set; } // "52ae9761-4b20-4334-9163-949a39485914",

        [DataMember(Name = "AOLEVEL")]
        public string AoLevel { get; set; } // "7",

        [DataMember(Name = "AOGUID")]
        public string AoGuid { get; set; } // "353b7aed-0f1b-4f44-8ce3-245083e17526",

        [DataMember(Name = "AOID")]
        public string AoID { get; set; } // null

        [DataMember(Name = "EXTRCODE")]
        public string ExtrCode { get; set; } // "0000",

        [DataMember(Name = "AREACODE")]
        public string AreaCode { get; set; } // "003",

        [DataMember(Name = "PLACECODE")]
        public string PlaceCode { get; set; } // "024",

        [DataMember(Name = "POSTALCODE")]
        public string PostalCode { get; set; } // "385336",

        [DataMember(Name = "CITYCODE")]
        public string CityCode { get; set; } // "000",

        [DataMember(Name = "AUTOCODE")]
        public string AutoCode { get; set; } // "0",

        [DataMember(Name = "OKATO")]
        public string Okato { get; set; } // "79218000024",

        [DataMember(Name = "OKTMO")]
        public string Oktmo { get; set; } // "79618420111",

        [DataMember(Name = "PREVID")]
        public string PrevID { get; set; } // "9890d854-0056-49cf-a1f2-4410e464ba9e",

        [DataMember(Name = "NEXTID")]
        public string NextID { get; set; } // null,

        [DataMember(Name = "PARENTGUID")]
        public string ParentGuid { get; set; } // "03614edb-f287-4b59-a3b3-056e160d1035",

        [DataMember(Name = "STARTDATE")]
        public DateTime StartDate { get; set; } // "2015-02-02",

        [DataMember(Name = "ENDDATE")]
        public DateTime EndDate { get; set; } // "2079-06-06",

        [DataMember(Name = "UPDATEDATE")]
        public DateTime UpdateDate { get; set; } // "2015-02-03",

        [DataMember(Name = "OPERSTATUS")]
        public string OperStatus { get; set; } // "21",

        [DataMember(Name = "ACTSTATUS")]
        public string ActStatus { get; set; } // "1",

        [DataMember(Name = "LIVESTATUS")]
        public string LiveStatus { get; set; } // "1",

        [DataMember(Name = "SEXTCODE")]
        public string SextCode { get; set; } // "000",

        [DataMember(Name = "CTARCODE")]
        public string CtarCode { get; set; } // "000",

        [DataMember(Name = "PLANCODE")]
        public string PlanCode { get; set; } // "0000",

        [DataMember(Name = "PLAINCODE")]
        public string PlainCode { get; set; } // "010030000240001",

        [DataMember(Name = "STREETCODE")]
        public string StreetCode { get; set; } // "0001",

        [DataMember(Name = "CODE")]
        public string StreeCode { get; set; } // "01003000024000100",

        [DataMember(Name = "FORMALNAME")]
        public string FormalName { get; set; } // "Широкая",
    }
}
