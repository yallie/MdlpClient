namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Место осуществления деятельности.
    /// </summary>
    [DataContract]
    public class BranchEntry
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code")]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Название субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_name")]
        public string FederalSubjectName { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrgName { get; set; }

        /// <summary>
        /// Перечень работ/услуг согласно лицензии
        /// </summary>
        [DataMember(Name = "work_list")]
        public string[] WorkList { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности
        /// </summary>
        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [DataMember(Name = "registration_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата приостановления
        /// </summary>
        [DataMember(Name = "suspension_date", IsRequired = false)]
        public DateTime? SuspensionDate { get; set; }
    }
}
