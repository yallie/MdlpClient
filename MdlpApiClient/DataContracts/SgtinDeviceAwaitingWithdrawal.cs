namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.7. КИЗ, ожидающий вывода из оборота по чеку от РВ.
    /// </summary>
    [DataContract]
    public class SgtinDeviceAwaitingWithdrawal
    {
        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Тип документа, по которому производится вывод через РВ.
        /// 10521 — Регистрация в ИС МДЛП сведений об отпуске лекарственного препарата по льготному рецепту (информация с СКЗКМ)
        /// 10531 — Регистрация в ИС МДЛП сведений о выдаче лекарственного препарата для оказания медицинской помощи (информация с СКЗКМ)
        /// </summary>
        [DataMember(Name = "xml_document_type")]
        public int XmlDocumentType { get; set; }

        /// <summary>
        /// Идентификатор организации-отправителя
        /// </summary>
        [DataMember(Name = "subject_id", IsRequired = false)]
        public string SubjectID { get; set; }

        /// <summary>
        /// Дата операции из чека
        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Номер льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_num", IsRequired = false)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_date", IsRequired = false)]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Серия льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_series", IsRequired = false)]
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Дата  фиксации КИЗа в очереди
        /// </summary>
        [DataMember(Name = "insertion_date", IsRequired = false)]
        public DateTime? InsertionDate { get; set; }

        /// <summary>
        /// Идентификатор XML-документа
        /// </summary>
        [DataMember(Name = "xml_document_id", IsRequired = false)]
        public string XmlDocumentID { get; set; }

        /// <summary>
        /// Доля от вторичной упаковки (доля вида 1/2)
        /// </summary>
        [DataMember(Name = "sold_part", IsRequired = false)]
        public string SoldPart { get; set; }

        /// <summary>
        /// Уникальный идентификатор РЭ или РВ
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Уникальный идентификатор системы, сформировавшей сообщение
        /// </summary>
        [DataMember(Name = "skzkm_origin_msg_id", IsRequired = false)]
        public string SkskmOriginMessageID { get; set; }
    }
}
