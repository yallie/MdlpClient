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
        [DataMember(Name = "IFNSUL", IsRequired = true)]
        public string IfnsUl { get; set; } // "6225",

        [DataMember(Name = "IFNSFL", IsRequired = true)]
        public string IfnsFl { get; set; } // "6225",

        [DataMember(Name = "TERRIFNSFL", IsRequired = true)]
        public string TerrIfnsFl { get; set; } // "6212",

        [DataMember(Name = "TERRIFNSUL", IsRequired = true)]
        public string TerrIfnsUl { get; set; } // "6212",

        [DataMember(Name = "STATSTATUS", IsRequired = true)]
        public string StatStatus { get; set; } // "0",

        [DataMember(Name = "ESTSTATUS", IsRequired = true)]
        public string EstStatus { get; set; } // "2",

        [DataMember(Name = "STRSTATUS", IsRequired = true)]
        public string StrStatus { get; set; } // "0",

        [DataMember(Name = "STARTDATE", IsRequired = false)]
        public DateTime StartDate { get; set; } // "1900-01-01",

        [DataMember(Name = "ENDDATE", IsRequired = false)]
        public DateTime EndDate { get; set; } // "2014-01-04",

        [DataMember(Name = "UPDATEDATE", IsRequired = false)]
        public DateTime UpdateDate { get; set; } // "2012-03-15"",

        [DataMember(Name = "OKATO", IsRequired = true)]
        public string Okato { get; set; } // "61226824016",

        [DataMember(Name = "OKTMO", IsRequired = true)]
        public string Oktmo { get; set; } // "61626424116",

        [DataMember(Name = "_id", IsRequired = true)]
        public string ID { get; set; } // "ba1c2f28-a455-47e2-95e5-000003a0023d",

        [DataMember(Name = "COUNTER", IsRequired = true)]
        public string Counter { get; set; } // "2",

        [DataMember(Name = "AOGUID", IsRequired = true)]
        public string AoGuid { get; set; } // "fce962f2-dff8-4eea-8413-5c94e0e69dec",

        [DataMember(Name = "DIVTYPE", IsRequired = false)]
        public string DivType { get; set; } // "0",

        [DataMember(Name = "POSTALCODE", IsRequired = true)]
        public string PostalCode { get; set; } // "391483",

        [DataMember(Name = "HOUSEGUID", IsRequired = true)]
        public string HouseGuid { get; set; } // "ba1c2f28-a455-47e2-95e5-000003a0023d",

        [DataMember(Name = "HOUSENUM", IsRequired = false)]
        public string HouseNum { get; set; } // "2",

        [DataMember(Name = "HOUSEID", IsRequired = false)]
        public string HouseID { get; set; } // null
    }
}
