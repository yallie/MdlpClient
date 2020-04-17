namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.1. Список найденных КИЗ
    /// 8.3.2. Список КИЗ и список ошибок поиска
    /// 8.3.5. Список КИЗ со статусом 'Оборот приостановлен'
    /// </summary>
    [DataContract]
    public class GetSgtinsResponse
    {
        [DataMember(Name = "entries")]
        public SgtinExtended[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        // следующие поля есть только в ответе метода 8.3.2

        [DataMember(Name = "failed", IsRequired = false)]
        public int Failed { get; set; }

        [DataMember(Name = "failed_entries", IsRequired = false)]
        public FailedSgtin[] FailedEntries { get; set; }
    }
}
