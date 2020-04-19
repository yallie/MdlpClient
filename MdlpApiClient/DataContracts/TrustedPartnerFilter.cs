namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.7.3. Метод фильтрации доверенных контрагентов
    /// Формат объекта TrustedPartnerFilter
    /// </summary>
    [DataContract]
    public class TrustedPartnerFilter
    {
        /// <summary>
        /// Идентификатор доверенного контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "trusted_sys_id", IsRequired = false)]
        public string SystemID { get; set; }

        /// <summary>
        /// ИНН/ITIN доверенного контрагента
        /// </summary>
        [DataMember(Name = "trusted_inn", IsRequired = false)]
        public string Inn { get; set; }
    }
}
