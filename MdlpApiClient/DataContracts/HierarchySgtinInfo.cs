namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.3. Информация о полной иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class HierarchySgtinInfo
    {
        /// <summary>
        /// Идентификационный код SGTIN
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Идентификационный код третичной упаковки
        /// </summary>
        [DataMember(Name = "sscc")]
        public string Sscc { get; set; }

        /// <summary>
        /// Идентификационный код товара в GS1
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Статус SGTIN
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "series_number")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Срок годности препарата
        /// </summary>
        [DataMember(Name = "expiration_date", IsRequired = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Решение о приостановке оборота, если есть
        /// </summary>
        [DataMember(Name = "pause_decision_info", IsRequired = false)]
        public HierarchyPausedCirculationDecision PausedDecision { get; set; }
    }
}
