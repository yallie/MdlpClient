namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список элементов и список ошибок.
    /// 8.3.2. Список КИЗ и список ошибок поиска.
    /// 8.3.3. Список КИЗ из общедоступного реестра КИЗ и список не найденных КИЗ.
    /// </summary>
    /// <typeparam name="T">Тип элемента поля Entries</typeparam>
    /// <typeparam name="F">Тип элемента поля FailedEntries</typeparam>
    [DataContract]
    public class EntriesFailedResponse<T, F> : EntriesResponse<T>
    {
        /// <summary>
        /// Список ошибочных записей
        /// </summary>
        [DataMember(Name = "failed_entries")]
        public F[] FailedEntries { get; set; }

        /// <summary>
        /// Общее количество ошибочных записей
        /// </summary>
        [DataMember(Name = "failed")]
        public int Failed { get; set; }
    }
}
