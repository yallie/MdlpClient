namespace MdlpApiClient.DataContracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.7. Получение списка исходящих документов
    /// 5.8. Получение списка входящих документов
    /// </summary>
    [DataContract]
    public class GetDocumentsResponse
    {
        [DataMember(Name = "documents")]
        public DocumentMetadata[] Documents { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
