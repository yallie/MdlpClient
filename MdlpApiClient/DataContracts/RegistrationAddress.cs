namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.5. Адрес для регистрации места ответственного хранения.
    /// </summary>
    [DataContract]
    public class RegistrationAddress
    {
        [DataMember(Name = "address_id")]
        public string AddressID { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        [DataMember(Name = "resolved_address")]
        public string ResolvedAddress { get; set; }

        [DataMember(Name = "license_type")]
        public string LicenseType { get; set; }

        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }
    }
}
