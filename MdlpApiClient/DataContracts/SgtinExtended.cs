namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.33. Формат объекта SgtinExtended
    /// </summary>
    [DataContract]
    public class SgtinExtended : Sgtin
    {
        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// ИНН/ИТИН производителя-упаковщика        /// </summary>
        [DataMember(Name = "packing_inn", IsRequired = false)]
        public string PackingInn { get; set; }

        /// <summary>
        /// Наименование производителя-упаковщика        /// </summary>
        [DataMember(Name = "packing_name", IsRequired = false)]
        public string PackingName { get; set; }

        /// <summary>
        /// ИНН/ИТИН производителя-выпускающего        /// </summary>
        [DataMember(Name = "control_inn", IsRequired = false)]
        public string ControlInn { get; set; }

        /// <summary>
        /// Наименование производителя-выпускающего        /// </summary>
        [DataMember(Name = "control_name", IsRequired = false)]
        public string ControlName { get; set; }
    }
}
