namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта ResolvedFiasAddress
    /// </summary>
    [DataContract]
    public class ResolvedFiasAddress
    {
        /// <summary>
        /// Идентификатор        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Адрес ФИАС
        /// </summary>
        [DataMember(Name = "address_fias")]
        public AddressFias AddressFias { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [DataMember(Name = "address_resolved")]
        public AddressResolved AddressResolved { get; set; }

        /// <summary>
        /// Статус:
        /// 0 — не действует
        /// 1 — действует        /// 2 — в процессе приостановления        /// </summary>
        [DataMember(Name = "Status")]
        public int Status { get; set; }
    }
}
