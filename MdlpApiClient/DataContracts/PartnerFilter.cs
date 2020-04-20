namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта PartnersFilter
    /// </summary>
    [DataContract]
    public class PartnerFilter
    {
        /// <summary>
        /// Идентификатор доверенного контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "system_subj_id", IsRequired = false)]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// Код ОКТМО субъекта Российской Федерации
        /// </summary>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Код округа Российской Федерации
        /// </summary>
        [DataMember(Name = "federal_district_code", IsRequired = false)]
        public string FederalDistrictCode { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        [DataMember(Name = "country", IsRequired = false)]
        public string Country { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        [DataMember(Name = "org_name", IsRequired = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [DataMember(Name = "kpp", IsRequired = false)]
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        [DataMember(Name = "ogrn", IsRequired = false)]
        public string Ogrn { get; set; }

        /// <summary>
        /// Дата заявки на регистрацию, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата заявки на регистрацию, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Тип участника, см. <see cref="RegEntityTypeEnum"/>:
        /// 1 — резидент РФ
        /// 2 — представительство иностранного держателя регистрационного удостоверения
        /// 3 — иностранный держатель регистрационного удостоверения
        /// 8 — иностранный контрагент
        /// </summary>
        [DataMember(Name = "reg_entity_type", IsRequired = false)]
        public int RegEntityType { get; set; }

        /// <summary>
        /// Дата фактической регистрации, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "op_exec_date_start", IsRequired = false)]
        public DateTime? OperationStartDate { get; set; }

        /// <summary>
        /// Дата фактической регистрации, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "op_exec_date_end", IsRequired = false)]
        public DateTime? OperationEndDate { get; set; }
    }
}
