namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Collections.Generic;
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
        /// Converts items to real Hierarchy
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static HierarchySsccInfo[] Convert(IEnumerable<HierarchySsccInfoInternal> items)
        {
            var converted =
                from item in items
                let children = item.Children.EmptyIfNull()
                let ssccs = children.Where(c => !c.IsSgtinInfo)
                let sgtins = children.Where(c => c.IsSgtinInfo)
                select new HierarchySsccInfo
                {
                    Sscc = item.Sscc,
                    ChildSsccs = Convert(ssccs),
                    ChildSgtins = sgtins.Select(c => c.GetSgtinInfo()).ToArray(),
                };

            return converted.ToArray();
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
