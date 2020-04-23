namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список элементов:
    /// 5.14. Документов по отчёту из СУЗ.
    /// </summary>
    /// <typeparam name="T">Тип поля Items</typeparam>
    [DataContract]
    public class ItemsResponse<T>
    {
        /// <summary>
        /// Список элементов
        /// </summary>
        [DataMember(Name = "items")]
        public T[] Items { get; set; }

        /// <summary>
        /// Общее число элементов
        /// </summary>
        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
