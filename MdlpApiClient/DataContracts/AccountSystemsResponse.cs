namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 6.8.2. Метод для поиска УС по фильтру
    /// </summary>
    /// <typeparam name="T">Тип поля Entries</typeparam>
    [DataContract]
    public class AccountSystemsResponse<T>
    {
        /// <summary>
        /// Список записей
        /// </summary>
        [DataMember(Name = "account_systems", IsRequired = true)]
        public T[] AccountSystems { get; set; }

        /// <summary>
        /// Общее количество записей
        /// </summary>
        [DataMember(Name = "total", IsRequired = true)]
        public int Total { get; set; }
    }
}
