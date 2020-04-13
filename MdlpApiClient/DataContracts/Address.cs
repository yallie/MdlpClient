namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Адрес места осуществления деятельности.
    /// </summary>
    [DataContract]
    public class Address
    {
        [DataMember(Name = "aoguid")]
        public string AoGuid { get; set; }

        [DataMember(Name = "houseguid")]
        public string HouseGuid { get; set; }

        [DataMember(Name = "address_description")]
        public string AddressDescription { get; set; }
    }
}
