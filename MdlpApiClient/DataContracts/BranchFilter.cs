namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Фильтр для мест осуществления деятельности.
    /// Содержит информацию для фильтрации списка мест осуществления деятельности.
    /// </summary>
    [DataContract]
    public class BranchFilter
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Уникальный идентификатор дома
        /// </summary>
        [DataMember(Name = "houseguid", IsRequired = false)]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Код округа РФ
        /// </summary>
        [DataMember(Name = "federal_district_code", IsRequired = false)]
        public string FederalDistrictCode { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата начала периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public CustomDateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public CustomDateTime EndDate { get; set; }

        /// <summary>
        /// Возможность вывода ЛП из оборота через РВ или соответствующий документ.
        /// • true — вывод ЛП из оборота возможен через РВ или документ
        /// • false — вывод ЛП из оборота возможен только через РВ        /// </summary>
        [DataMember(Name = "is_withdrawal_via_document_allowed", IsRequired = false)]
        public bool? IsWithdrawalViaDocumentAllowed { get; set; }

        /// <summary>
        /// Лицензия на фармацевтическую деятельность        /// </summary>
        [DataMember(Name = "has_pharm_license", IsRequired = false)]
        public bool? HasPharmLicense { get; set; }

        /// <summary>
        /// Лицензия на производственную деятельность        /// </summary>
        [DataMember(Name = "has_prod_license", IsRequired = false)]
        public bool? HasProdLicense { get; set; }

        /// <summary>
        /// Лицензия на медицинскую деятельность        /// </summary>
        [DataMember(Name = "has_med_license", IsRequired = false)]
        public bool? HasMedLicense { get; set; }
    }
}
