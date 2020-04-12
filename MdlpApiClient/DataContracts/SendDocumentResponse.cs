namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.1. Отправка документа
    /// </summary>
    [DataContract]
    internal class SendDocumentResponse
    {
        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; }
    }
}
