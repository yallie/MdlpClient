namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 8.1.2. Мест осуществления деятельности.
    /// 8.2.2. Список мест ответственного хранения.
    /// 8.2.5. Метод получения информации об адресах искомого участника.
    /// 8.3.1. Список найденных КИЗ.
    /// 8.3.5. Список КИЗ со статусом 'Оборот приостановлен'.
    /// 8.3.6. Результат поиска по реестру КИЗ записей, ожидающих
    /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// </summary>
    /// <typeparam name="T">Тип поля Entries</typeparam>
    [DataContract]
    public class EntriesResponse<T>
    {
        [DataMember(Name = "entries", IsRequired = true)]
        public T[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
