namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.3. Метод для получения информации о лицевых счетах
    /// Формат объекта BillingAccount
    /// </summary>
    [DataContract]
    public class BillingAccount
    {
        /// <summary>
        /// Идентификатор лицевого счета
        /// </summary>
        [DataMember(Name = "account_number", IsRequired = true)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Баланс лицевого счета
        /// </summary>
        /// <remarks>
        /// Может быть не заполнен, если данной информации не поступало в ИС "МДЛП"
        /// </remarks>
        [DataMember(Name = "balance", IsRequired = false)]
        public decimal? Balance { get; set; }

        /// <summary>
        /// Дата последнего обновления баланса лицевого счёта
        /// </summary>
        [DataMember(Name = "last_update", IsRequired = false)]
        public DateTime? LastUpdate { get; set; }
    }
}
