namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. Результат поиска по реестру КИЗ записей, ожидающих
    /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// </summary>
    [DataContract]
    public class GetSgtinsKktAwaitingWithdrawalResponse
    {
        [DataMember(Name = "entries")]
        public SgtinKktAwaitingWithdrawal[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
