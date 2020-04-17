namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.2. Список КИЗ, вложенных в третичную упаковку
    /// </summary>
    [DataContract]
    public class GetSsccSgtinsResponse : EntriesResponse<Sgtin>
    {
        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        /// <remarks>
        /// Присутствует только при ошибке
        /// 2 — Запрашиваемые данные не найдены
        /// 4 — Запрашиваемые данные доступны только текущему владельцу или контрагенту по операции
        /// </remarks>
        [DataMember(Name = "error_code", IsRequired = false)]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Текстовое описание ошибки
        /// </summary>
        /// <remarks>
        /// Присутствует только при ошибке
        /// </remarks>
        [DataMember(Name = "error_desc", IsRequired = false)]
        public string ErrorDescription { get; set; }
    }
}
