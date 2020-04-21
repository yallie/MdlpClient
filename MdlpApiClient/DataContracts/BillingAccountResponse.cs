namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.3. Метод для получения информации о лицевых счетах
    /// </summary>
    [DataContract]
    internal class BillingAccountResponse
    {
        /// <summary>
        /// Список лицевых счетов
        /// </summary>
        [DataMember(Name = "accounts", IsRequired = false)]
        public BillingAccount[] Accounts { get; set; }
    }
}
