namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.3. Метод поиска по общедоступному реестру КИЗ по списку значений
    /// </summary>
    [DataContract]
    public class GetPublicSgtinResponse
    {
        [DataMember(Name = "entries")]
        public PublicSgtin[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "failed")]
        public int Failed { get; set; }

        [DataMember(Name = "failed_entries")]
        public string[] FailedEntries { get; set; }
    }
}
