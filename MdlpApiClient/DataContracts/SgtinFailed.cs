namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.2. Ошибка поиска КИЗ
    /// </summary>
    [DataContract]
    public class SgtinFailed
    {
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        [DataMember(Name = "error_code")]
        public int ErrorCode { get; set; }

        [DataMember(Name = "error_desc")]
        public string ErrorDescription { get; set; }
    }
}
