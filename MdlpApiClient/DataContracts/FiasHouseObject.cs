namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.5.2. Получение объекта ФИАС по идентификатору дома
    /// Формат объекта FiasHouseObject
    /// </summary>
    [DataContract]
    public class FiasHouseObject
    {
        [DataMember(Name = "IFNSUL")]
        public string IfnsUl { get; set; } // "6225",

        [DataMember(Name = "IFNSFL")]
        public string IfnsFl { get; set; } // "6225",

        [DataMember(Name = "TERRIFNSFL")]
        public string TerrIfnsFl { get; set; } // "6212",

        [DataMember(Name = "TERRIFNSUL")]
        public string TerrIfnsUl { get; set; } // "6212",

        [DataMember(Name = "STATSTATUS")]
        public string StatStatus { get; set; } // "0",

        [DataMember(Name = "ESTSTATUS")]
        public string EstStatus { get; set; } // "2",

        [DataMember(Name = "STRSTATUS")]
        public string StrStatus { get; set; } // "0",

        [DataMember(Name = "STARTDATE")]
        public DateTime StartDate { get; set; } // "1900-01-01",

        [DataMember(Name = "ENDDATE")]
        public DateTime EndDate { get; set; } // "2014-01-04",

        [DataMember(Name = "UPDATEDATE")]
        public DateTime UpdateDate { get; set; } // "2012-03-15"",

        [DataMember(Name = "OKATO")]
        public string Okato { get; set; } // "61226824016",

        [DataMember(Name = "OKTMO")]
        public string Oktmo { get; set; } // "61626424116",

        [DataMember(Name = "_id")]
        public string ID { get; set; } // "ba1c2f28-a455-47e2-95e5-000003a0023d",

        [DataMember(Name = "COUNTER")]
        public string Counter { get; set; } // "2",

        [DataMember(Name = "AOGUID")]
        public string AoGuid { get; set; } // "fce962f2-dff8-4eea-8413-5c94e0e69dec",

        [DataMember(Name = "DIVTYPE")]
        public string DivType { get; set; } // "0",

        [DataMember(Name = "POSTALCODE")]
        public string PostalCode { get; set; } // "391483",

        [DataMember(Name = "HOUSEGUID")]
        public string HouseGuid { get; set; } // "ba1c2f28-a455-47e2-95e5-000003a0023d",

        [DataMember(Name = "HOUSENUM")]
        public string HouseNum { get; set; } // "2",

        [DataMember(Name = "HOUSEID")]
        public string HouseID { get; set; } // null
    }
}
