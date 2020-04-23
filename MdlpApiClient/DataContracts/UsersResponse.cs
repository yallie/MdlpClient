namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 6.7.2. Метод для поиска зарегистрированных пользователей по фильтру
    /// </summary>
    /// <typeparam name="T">Тип поля Entries</typeparam>
    [DataContract]
    public class UsersResponse<T>
    {
        /// <summary>
        /// Список записей
        /// </summary>
        [DataMember(Name = "users", IsRequired = true)]
        public T[] Users { get; set; }

        /// <summary>
        /// Общее количество записей
        /// </summary>
        [DataMember(Name = "total", IsRequired = true)]
        public int Total { get; set; }
    }
}
