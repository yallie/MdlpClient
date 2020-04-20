namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// Формат объекта AgreementInfoEntry
    /// </summary>
    [DataContract]
    public class AgreementInfoEntry
    {
        /// <summary>
        /// Статус документа
        /// 0 — не подписан
        /// 1 — подписан
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Дата последней смены статуса
        /// Если статус еще не менялся, будет возвращена дата регистрации участника        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }
    }
}
