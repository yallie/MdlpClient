namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 6.6.11. Метод для поиска списка групп прав пользователей по фильтру
    /// </summary>
    /// <typeparam name="T">Тип поля Groups</typeparam>
    [DataContract]
    public class GroupsResponse<T>
    {
        /// <summary>
        /// Список элементов
        /// </summary>
        [DataMember(Name = "groups", IsRequired = true)]
        public T[] Groups { get; set; }

        /// <summary>
        /// Общее число элементов
        /// </summary>
        [DataMember(Name = "total", IsRequired = true)]
        public int Total { get; set; }
    }
}
