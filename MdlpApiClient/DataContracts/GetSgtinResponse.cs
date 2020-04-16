namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Подробности о КИЗ (SGTIN) и ЛП (GTIN)
    /// </summary>
    [DataContract]
    public class GetSgtinResponse
    {
        [DataMember(Name = "sgtin_info")]
        public SgtinExtended SgtinInfo { get; set; }

        [DataMember(Name = "gtin_info")]
        public GtinInfo GtinInfo { get; set; }
    }
}
