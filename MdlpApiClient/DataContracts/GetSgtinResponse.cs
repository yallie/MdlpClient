namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.1. Список КИЗ
    /// </summary>
    [DataContract]
    public class GetSgtinResponse
    {
        [DataMember(Name = "entries")]
        public SgtinExtended[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
