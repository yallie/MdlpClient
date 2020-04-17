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
        [DataMember(Name = "items")]
        public T[] Items { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
