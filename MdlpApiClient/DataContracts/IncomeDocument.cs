namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.17. Формат объекта IncomeDocument
    /// Таблица 13. Формат объекта IncomeDocument
    /// Объект IncomeDocument наследует все поля объекта Document и добавляет следующие:
    /// </summary>
    [DataContract]
    public class IncomeDocument : DocumentMetadata
    {
        /// <summary>
        /// Идентификатор отправителя документа в «ИС "Маркировка". МДЛП»        /// </summary>
        [DataMember(Name = "sender_sys_id")]
        public string SenderSystemID { get; set; } // "e2cb20c1-1d5b-4ab6-b8dd-9297bec23f63" (optional)
    }
}
