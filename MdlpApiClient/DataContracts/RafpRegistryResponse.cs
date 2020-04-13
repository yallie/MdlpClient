namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RafpRegistryResponse
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        [DataMember(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [DataMember(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [DataMember(Name = "KPP")]
        public string Kpp { get; set; }
    }
}
