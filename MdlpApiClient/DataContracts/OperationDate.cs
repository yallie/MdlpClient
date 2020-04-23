namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта OperationDate
    /// </summary>
    [DataContract]
    public class OperationDate
    {
        /// <summary>
        /// Дата операции
        /// </summary>
        [DataMember(Name = "$date")]
        public DateTime Date { get; set; }
    }
}
