namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.6.2. Метод для просмотра заявок на регистрацию иностранных контрагентов
    /// Структура данных ForeignCounterpartyFilter
    /// </summary>
    [DataContract]
    public class ForeignCounterpartyFilter
    {
        /// <summary>
        /// Дата регистрации, начало периода
        /// </summary>
        [DataMember(Name = "reg_date_from", IsRequired = false)]
        public DateTime? RegistrationDateFrom { get; set; }

        /// <summary>
        /// Дата регистрации, окончание периода
        /// </summary>
        [DataMember(Name = "reg_date_to", IsRequired = false)]
        public DateTime? RegistrationDateTo { get; set; }

        /// <summary>
        /// ИНН/ITIN
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember(Name = "org_name", IsRequired = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Страна регистрации
        /// </summary>
        [DataMember(Name = "country_code", IsRequired = false)]
        public string CountryCode { get; set; }
    }
}
