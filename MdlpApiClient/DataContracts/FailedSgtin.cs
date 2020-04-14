namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class FailedSgtin
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
