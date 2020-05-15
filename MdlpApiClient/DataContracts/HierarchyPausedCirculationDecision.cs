namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.3. Информация о приостановке оборота ЛП
    /// </summary>
    [DataContract]
    public class HierarchyPausedCirculationDecision
    {
        /// <summary>
        /// Идентификатор решения
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public string HaltID { get; set; }

        /// <summary>
        /// Номер решения
        /// </summary>
        [DataMember(Name = "number", IsRequired = true)]
        public string HaltDocNumber { get; set; }

        /// <summary>
        /// Дата решения
        /// </summary>
        [DataMember(Name = "date", IsRequired = true)]
        public DateTime HaltDate { get; set; }
    }
}
