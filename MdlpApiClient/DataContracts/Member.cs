namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// Формат объекта Member
    /// </summary>
    [DataContract]
    public class Member
    {
        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "sys_id")]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// ИНН субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        [DataMember(Name = "ogrn")]
        public string Ogrn { get; set; }

        /// <summary>
        /// ОГРНИП
        /// </summary>
        [DataMember(Name = "ogrnip")]
        public string Ogrnip { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [DataMember(Name = "kpp")]
        public string Kpp { get; set; }

        /// <summary>
        /// Имя руководителя организации
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество руководителя организации
        /// </summary>
        [DataMember(Name = "middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия руководителя организации
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Признак резидента РФ
        /// </summary>
        [DataMember(Name = "is_resident")]
        public bool IsResident { get; set; }

        /// <summary>
        /// Сведения о задолженности организации
        /// </summary>
        [DataMember(Name = "Debts")]
        public string Debts { get; set; }

        /// <summary>
        /// Код налогового органа
        /// </summary>
        [DataMember(Name = "tax_authority_code", IsRequired = false)]
        public string TaxAuthorityCode { get; set; }

        /// <summary>
        /// Код статуса
        /// </summary>
        [DataMember(Name = "status_code", IsRequired = false)]
        public string StatusCode { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        [DataMember(Name = "status_name", IsRequired = false)]
        public string StatusName { get; set; }

        /// <summary>
        /// Коды внесения записи в ЕГРЮЛ
        /// </summary>
        [DataMember(Name = "esklp_codes", IsRequired = false)]
        public string[] EsklpCodes { get; set; }

        /// <summary>
        /// Подробное описание деятельности организации
        /// </summary>
        [DataMember(Name = "activity_description", IsRequired = false)]
        public string ActivityDescription { get; set; }

        /// <summary>
        /// Информация о руководителях организации        /// </summary>
        [DataMember(Name = "chiefs", IsRequired = false)]
        public ChiefInfo[] Chiefs { get; set; }

        /// <summary>
        /// Код языка квитанций
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Код субъекта РФ (код места юридической регистрации участника)        /// </summary>
        [DataMember(Name = "registration_federal_subject_code", IsRequired = false)]
        public string RegistrationFederalSubjectCode { get; set; }

        /// <summary>
        /// Информация о договорах и заявлениях участника
        /// </summary>
        [DataMember(Name = "agreements_info")]
        public AgreementsInfo AgreementsInfo { get; set; }

        /// <summary>
        /// Информация о банковских реквизитах участника
        /// </summary>
        [DataMember(Name = "banking_info", IsRequired = false)]
        public BankingInfo BankingInfo { get; set; }

        /// <summary>
        /// Номер контактного телефона
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }

        /// <summary>
        /// Тип участника:
        /// 1 — резидент РФ
        /// 2 — представительство иностранного держателя регистрационного удостоверения
        /// 3 — иностранный держатель регистрационного удостоверения
        /// 8 — иностранный контрагент
        /// </summary>
        /// <remarks>
        /// См. <see cref="RegEntityTypeEnum"/>
        /// </remarks>
        [DataMember(Name = "entity_type", IsRequired = false)]
        public int? EntityType { get; set; }

        /// <summary>
        /// Признак поставщика высокозатратных нозологий
        /// </summary>
        [DataMember(Name = "vzn_vendor")]
        public bool VznVendor { get; set; }

        /// <summary>
        /// Адрес юридической регистрации участника
        /// </summary>
        [DataMember(Name = "org_address")]
        public string Address { get; set; }

        /// <summary>
        /// Краткое наименование организации        /// </summary>
        [DataMember(Name = "org_short_name")]
        public string OrganizationShortName { get; set; }
    }
}
