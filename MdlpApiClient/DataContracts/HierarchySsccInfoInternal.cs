namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using MdlpApiClient.Toolbox;

    /// <summary>
    /// 8.4.3. Информация о полной иерархии вложенности третичной упаковки
    /// </summary>
    /// <remarks>
    /// Вспомогательный класс для решения проблемы десериализации 
    /// разнородных элементов в массиве "childs"
    /// </remarks>
    [DataContract]
    internal class HierarchySsccInfoInternal : HierarchySsccInfo
    {
        /// <summary>
        /// Converts items to real <see cref="HierarchySsccInfo"/>.
        /// </summary>
        /// <param name="item">Item to convert</param>
        public static HierarchySsccInfo Convert(HierarchySsccInfoInternal item)
        {
            var children = item.Children.EmptyIfNull();
            var ssccs = children.Where(c => !c.IsSgtinInfo);
            var sgtins = children.Where(c => c.IsSgtinInfo);
            return new HierarchySsccInfo
            {
                Sscc = item.Sscc,
                ChildSsccs = ssccs.Select(s => Convert(s)).ToArray(),
                ChildSgtins = sgtins.Select(c => c.GetSgtinInfo()).ToArray(),
            };
        }

        /// <summary>
        /// Checks if the current instance represents <see cref="HierarchySgtinInfo"/>.
        /// </summary>
        [IgnoreDataMember]
        private bool IsSgtinInfo
        {
            get { return Sgtin != null; }
        }

        /// <summary>
        /// Converts to <see cref="HierarchySgtinInfo"/>.
        /// </summary>
        private HierarchySgtinInfo GetSgtinInfo()
        {
            return new HierarchySgtinInfo
            {
                Sgtin = Sgtin,
                Sscc = Sscc,
                Gtin = Gtin,
                Status = Status,
                BatchNumber = BatchNumber,
                ExpirationDate = ExpirationDate,
                PausedDecision = PausedDecision,
            };
        }

        /// <summary>
        /// Идентификационный код SGTIN
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

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

        /// <summary>
        /// Дочерние элементы, сваленные в кучу
        /// </summary>
        [DataMember(Name = "childs", IsRequired = false)]
        public HierarchySsccInfoInternal[] Children { get; set; }
    }
}
