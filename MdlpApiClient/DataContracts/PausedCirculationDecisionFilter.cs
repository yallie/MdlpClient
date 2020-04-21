namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.12.1. Фильтрация по реестру решений о приостановке КИЗ
    /// </summary>
    [DataContract]
    public class PausedCirculationDecisionFilter
    {
        /// <summary>
        /// ИНН участника
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Тип решения:
        /// 0 — временный вывод из обращения
        /// 1 — отмена временного вывода из обращения
        /// </summary>
        [DataMember(Name = "halt_type", IsRequired = false)]
        public int HaltType { get; set; }

        /// <summary>
        /// Номер решения
        /// </summary>
        [DataMember(Name = "halt_doc_num", IsRequired = false)]
        public string HaltDocNumber { get; set; }

        /// <summary>
        /// Идентификатор решения
        /// </summary>
        [DataMember(Name = "halt_id", IsRequired = false)]
        public string HaltID { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// Дата решения, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "start_halt_doc_date", IsRequired = false)]
        public DateTime? StartHaltDocDate { get; set; }

        /// <summary>
        /// Дата решения, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "end_halt_doc_date", IsRequired = false)]
        public DateTime? EndHaltDocDate { get; set; }

        /// <summary>
        /// Дата вступления в силу, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "start_halt_date", IsRequired = false)]
        public DateTime? StartHaltDate { get; set; }

        /// <summary>
        /// ДДата вступления в силу, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "end_halt_date", IsRequired = false)]
        public DateTime? EndHaltDate { get; set; }
    }
}
