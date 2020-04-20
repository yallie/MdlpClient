namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// </summary>
    /// <typeparam name="T">Тип поля Entries</typeparam>
    [DataContract]
    public class EntriesFilteredResponse<T>
    {
        [DataMember(Name = "filtered_records")]
        public T[] Entries { get; set; }

        [DataMember(Name = "filtered_records_count")]
        public int Total { get; set; }
    }
}
