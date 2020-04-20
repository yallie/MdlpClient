namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта RegistrationEntry
    /// </summary>
    [DataContract]
    public class RegistrationEntry
    {
        /// <summary>
        /// Идентификатор контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "system_subj_id")]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// Список мест осуществления деятельности
        /// </summary>
        [DataMember(Name = "branches")]
        public ResolvedFiasAddress[] Branches { get; set; }

        /// <summary>
        /// Список мест ответственного хранения
        /// </summary>
        [DataMember(Name = "safe_warehouses")]
        public ResolvedFiasAddress[] Warehouses { get; set; }

        /// <summary>
        /// ИНН субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// ИТИН
        /// </summary>
        [DataMember(Name = "itin")]
        public string Itin { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [DataMember(Name = "KPP")]
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        [DataMember(Name = "OGRN")]
        public string Ogrn { get; set; }

        /// <summary>
        /// Наименование субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "ORG_NAME")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Имя руководителя организации
        /// </summary>
        [DataMember(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество руководителя организации
        /// </summary>
        [DataMember(Name = "MIDDLE_NAME")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия руководителя организации
        /// </summary>
        [DataMember(Name = "LAST_NAME")]
        public string LastName { get; set; }

        /// <summary>
        /// Тип участника:
        /// 1 — резидент РФ
        /// 2 — представительство иностранного держателя регистрационного удостоверения
        /// 3 — иностранный держатель регистрационного удостоверения
        /// 8 — иностранный контрагент
        /// </summary>
        /// <remarks>
        /// См. <see cref="RegEntityTypeEnum"/>.
        /// </remarks>
        [DataMember(Name = "reg_entity_type", IsRequired = false)]
        public int? RegEntityType { get; set; }

        /// <summary>
        /// Дата заявки на регистрацию
        /// </summary>
        [DataMember(Name = "op_date")]
        public OperationDate OperationDate { get; set; }

        /// <summary>
        /// Дата фактической регистрации в системе
        /// </summary>
        [DataMember(Name = "op_exec_date")]
        public DateTime OperationExecutionDate { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code")]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [DataMember(Name = "regNum")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [DataMember(Name = "regDate", IsRequired = false)]
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// Адрес организации
        /// </summary>
        [DataMember(Name = "org_address")]
        public ForeignAddress Address { get; set; }

        /// <summary>
        /// КПП (дубликат реквизита, косяк проектирования API)
        /// </summary>
        [DataMember(Name = "kpp")]
        public string kpp { get; set; }

        /// <summary>
        /// ОГРН (дубликат реквизита, косяк проектирования API)
        /// </summary>
        [DataMember(Name = "ogrn")]
        public string ogrn { get; set; }
    }
}
