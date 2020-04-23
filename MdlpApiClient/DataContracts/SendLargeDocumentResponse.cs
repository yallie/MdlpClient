namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.2. Отправка документа большого объема
    /// 5.3. Загрузка документа большого объема
    /// 5.4. Завершение отправки документа
    /// </summary>
    [DataContract]
    internal class SendLargeDocumentResponse
    {
        [DataMember(Name = "link", IsRequired = false)]
        public string Link { get; set; }

        [DataMember(Name = "document_id", IsRequired = false)]
        public string DocumentID { get; set; }

        [DataMember(Name = "request_id", IsRequired = false)]
        public string RequestID { get; set; }
    }
}
