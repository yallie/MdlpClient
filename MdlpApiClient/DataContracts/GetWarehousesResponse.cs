namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.2. Список мест ответственного хранения.
    /// </summary>
    [DataContract]
    public class GetWarehousesResponse
    {
        [DataMember(Name = "entries")]
        public WarehouseEntry[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
