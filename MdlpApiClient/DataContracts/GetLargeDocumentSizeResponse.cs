namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.5. Получение информации об ограничении размера небольших документов
    /// </summary>
    [DataContract]
    internal class GetLargeDocumentSizeResponse
    {
        [DataMember(Name = "doc_size")]
        public int DocSize { get; set; }
    }
}
