namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.7.1. Метод добавления доверенного контрагента
    /// </summary>
    [DataContract]
    public class AddTrustedPartnersResponse
    {
        [DataMember(Name = "code", IsRequired = true)]
        public string Code { get; set; }

        [DataMember(Name = "failed_partners", IsRequired = false)]
        public string[] FailedPartners { get; set; }
    }
}
