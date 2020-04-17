namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. Фильтр для поиска по реестру КИЗ записей, ожидающих вывода из оборота по чеку от контрольно-кассовой техники (ККТ)
    /// 8.3.7. Фильтр для поиска по реестру КИЗ записей, ожидающих вывода из оборота через РВ
    /// </summary>
    [DataContract]
    public class SgtinAwaitingWithdrawalFilter
    {
        /// <summary>
        /// Идентификатор места деятельности отправителя
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = false)]
        public string Sgtin { get; set; }

        /// <summary>
        /// Дата операции из чека, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "op_start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата операции из чека, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "op_end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}
