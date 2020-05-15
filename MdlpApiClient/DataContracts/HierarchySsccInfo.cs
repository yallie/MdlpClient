namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.3. Информация о полной иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class HierarchySsccInfo
    {
        /// <summary>
        /// Идентификационный код третичной упаковки
        /// </summary>
        [DataMember(Name = "sscc")]
        public string Sscc { get; set; }

        /// <summary>
        /// Дата и время совершения операции упаковки
        /// </summary>
        [DataMember(Name = "packing_date")]
        public DateTime PackingDate { get; set; }

        /// <summary>
        /// Список вложенных упаковок
        /// </summary>
        [IgnoreDataMember]
        public HierarchySsccInfo[] ChildSsccs { get; set; }

        /// <summary>
        /// Список вложенных препаратов
        /// </summary>
        [IgnoreDataMember]
        public HierarchySgtinInfo[] ChildSgtins { get; set; }
    }
}
