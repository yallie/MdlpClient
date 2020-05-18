namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.16. Формат объекта OutcomeDocument
    /// Таблица 12. Формат объекта OutcomeDocument
    /// Объект OutcomeDocument наследует все поля объекта Document и добавляет следующие:
    /// </summary>
    [DataContract]
    public class OutcomeDocument : DocumentMetadata
    {
        /// <summary>
        /// Уникальный идентификатор регистратора событий РЭ или РВ.
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; } // 1230000011111111 (optional)

        /// <summary>
        /// Уникальный идентификатор системы, сформировавшей сообщение.
        /// </summary>
        [DataMember(Name = "skzkm_origin_msg_id", IsRequired = false)]
        public string SkzkmOriginMessageID { get; set; } // "e2cb20c1-1d5b-4ab6-b8dd-9297bec23f63" (optional)

        /// <summary>
        /// Идентификатор отчета системы управления заказами (СУЗ), Guid.
        /// Для документов, полученных от регистраторов событий.
        /// </summary>
        [DataMember(Name = "skzkm_report_id", IsRequired = false)]
        public string SkzkmReportID { get; set; }
    }
}
