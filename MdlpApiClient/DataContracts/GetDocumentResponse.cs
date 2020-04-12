namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.10. Получение документа по идентификатору
    /// </summary>
    [DataContract]
    public class GetDocumentResponse
    {
        [DataMember(Name = "link")]
        public string Link { get; set; }
    }
}
