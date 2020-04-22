namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.11. Метод для получения информации об УС
    /// </summary>
    [DataContract]
    internal class GetAccountSystemResponse
    {
        /// <summary>
        /// Учетная система
        /// </summary>
        [DataMember(Name = "account_system", IsRequired = true)]
        public AccountSystem AccountSystem { get; set; }
    }
}
