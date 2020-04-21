namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.12.1. Фильтрация по реестру решений о приостановке КИЗ
    /// </summary>
    [DataContract]
    public class PausedCirculationDecision
    {
        /// <summary>
        /// Количество SGTIN
        /// </summary>
        [DataMember(Name = "sgtin_count", IsRequired = false)]
        public int SgtinCount { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// ИНН участника
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        [DataMember(Name = "org_name", IsRequired = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Дата решения
        /// </summary>
        [DataMember(Name = "halt_doc_date", IsRequired = false)]
        public DateTime? HaltDocDate { get; set; }

        /// <summary>
        /// Дата вступления в силу
        /// </summary>
        [DataMember(Name = "halt_date", IsRequired = false)]
        public DateTime? HaltDate { get; set; }

        /// <summary>
        /// Дата приостановки/отмены приостановки SGTIN
        /// </summary>
        [DataMember(Name = "op_date", IsRequired = false)]
        public DateTime? OperationDate { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Номер решения
        /// </summary>
        [DataMember(Name = "halt_doc_num", IsRequired = false)]
        public string HaltDocNumber { get; set; }

        /// <summary>
        /// Идентификатор решения
        /// </summary>
        [DataMember(Name = "halt_id", IsRequired = true)]
        public string HaltID { get; set; }

        /// <summary>
        /// Тип решения:
        /// 0 — временный вывод из обращения
        /// 1 — отмена временного вывода из обращения
        /// </summary>
        [DataMember(Name = "halt_type", IsRequired = false)]
        public int HaltType { get; set; }

        /// <summary>
        /// Адрес места деятельности (код ФИАС)
        /// </summary>
        [DataMember(Name = "owner_address", IsRequired = false)]
        public string OwnerAddressID { get; set; }

        /// <summary>
        /// Дата РУ
        /// </summary>
        [DataMember(Name = "reg_date", IsRequired = false)]
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// Номер РУ
        /// </summary>
        [DataMember(Name = "reg_num", IsRequired = false)]
        public string RegistrationNumber { get; set; }
    }
}
