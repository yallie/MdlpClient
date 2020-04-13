namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.3. Получение информации о конкретном месте осуществления деятельности
    /// </summary>
    [DataContract]
    public class GetBranchResponse
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности
        /// </summary>
        [DataMember(Name = "address", IsRequired = false)]
        public Address Address { get; set; }
    }
}
