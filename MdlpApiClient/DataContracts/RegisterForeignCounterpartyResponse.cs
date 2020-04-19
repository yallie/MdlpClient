namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.6.1. Метод для регистрации иностранного контрагента
    /// </summary>
    [DataContract]
    internal class RegisterForeignCounterpartyResponse
    {
        [DataMember(Name = "counterparty_id")]
        public string CounterpartyID { get; set; }
    }
}
