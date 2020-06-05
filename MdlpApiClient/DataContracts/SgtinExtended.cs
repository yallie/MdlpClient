namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.35. Формат объекта SgtinExtended
    /// Таблица 31. Формат объекта SgtinExtended
    /// </summary>
    [DataContract]
    public class SgtinExtended : Sgtin
    {
        // <summary>
        // Идентификатор заказа системы управления заказами (СУЗ), Guid
        // Он и так есть в классе Sgtin.
        // </summary>
        // [DataMember(Name = "oms_order_id", IsRequired = false)]
        // public string OmsOrderID { get; set; }

        /// <summary>
        /// ИНН/ИТИН упаковщика во вторичную/третичную упаковку.
        /// </summary>
        [DataMember(Name = "packing_inn", IsRequired = false)]
        public string PackingInn { get; set; }

        /// <summary>
        /// Наименование упаковщика во вторичную/третичную упаковку.
        /// </summary>
        [DataMember(Name = "packing_name", IsRequired = false)]
        public string PackingName { get; set; }

        /// <summary>
        /// Идентификатор упаковщика во вторичную/третичную упаковку.
        /// </summary>
        [DataMember(Name = "packing_id", IsRequired = false)]
        public string PackingID { get; set; }

        /// <summary>
        /// ИНН/ИТИН производителя стадии выпускающий контроль качества.
        /// </summary>
        [DataMember(Name = "control_inn", IsRequired = false)]
        public string ControlInn { get; set; }

        /// <summary>
        /// Наименование производителя стадии выпускающий контроль качества.
        /// </summary>
        [DataMember(Name = "control_name", IsRequired = false)]
        public string ControlName { get; set; }

        /// <summary>
        /// Идентификатор производителя стадии выпускающий контроль качества.
        /// </summary>
        [DataMember(Name = "control_id", IsRequired = false)]
        public string ControlID { get; set; }
    }
}
