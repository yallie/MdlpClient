namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.2. Место ответственного хранения
    /// </summary>
    [DataContract]
    public class WarehouseEntry
    {
        /// <summary>
        /// Код сущности (для методов, возвращающих места и склады в одном списке, например, 7.8.1).
        /// </summary>
        public const int EntityType = 1;

        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
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
        /// ИНН юридического лица
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

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
        /// Название владельца лицензии
        /// </summary>
        [DataMember(Name = "warehouse_org_name")]
        public string WarehouseOrgName { get; set; }

        /// <summary>
        /// ИНН владельца лицензии
        /// </summary>
        [DataMember(Name = "warehouse_org_inn")]
        public string WarehouseOrgInn { get; set; }

        /// <summary>
        /// Статус:
        /// 0 — не действует,
        /// 1 — действует,
        /// 2 — в процессе приостановления
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
