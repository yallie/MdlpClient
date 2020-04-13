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
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}
