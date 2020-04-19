namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.7.3. Метод фильтрации доверенных контрагентов
    /// Формат объекта TrustedPartnerEntry
    /// </summary>
    [DataContract]
    public class TrustedPartnerEntry
    {
        /// <summary>
        /// Идентификатор доверенного контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "system_subj_id")]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// ИНН/ITIN доверенного контрагента
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// Наименование доверенного контрагента
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrganizationName { get; set; }
    }
}
