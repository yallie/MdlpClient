namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.5. Адрес для регистрации места ответственного хранения.
    /// </summary>
    [DataContract]
    public class RegistrationAddress
    {
        /// <summary>
        /// Идентификатор адреса
        /// </summary>
        [DataMember(Name = "address_id")]
        public string AddressID { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Адрес из лицензии
        /// </summary>
        [DataMember(Name = "resolved_address")]
        public string ResolvedAddress { get; set; }

        /// <summary>
        /// Тип лицензии
        /// 1 — лицензия на фарм. деятельность;
        /// 2 — лицензия на производство
        /// </summary>
        [DataMember(Name = "license_type")]
        public string LicenseType { get; set; }

        /// <summary>
        /// Идентификационный номер налогоплательщика (ИНН)
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }
    }
}
