namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.14. Прослеживание документов по отчёту из СУЗ
    /// </summary>
    [DataContract]
    public class GetDocumentsSkzkmResponse
    {
        [DataMember(Name = "items")]
        public DocumentSkzkmMetadata[] Documents { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
