namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список документов:
    /// 5.7. Исходящих документов.
    /// 5.8. Входящих документов.
    /// 5.11. Документов по идентификатору запроса.
    /// </summary>
    /// <typeparam name="T">Тип поля Documents</typeparam>
    [DataContract]
    public class DocumentsResponse<T>
    {
        /// <summary>
        /// Список документов
        /// </summary>
        [DataMember(Name = "documents")]
        public T[] Documents { get; set; }

        /// <summary>
        /// Общее количество документов
        /// </summary>
        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
