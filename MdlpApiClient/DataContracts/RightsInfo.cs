namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.27. Формат объекта RightsInfo
    /// Таблица 23. Формат объекта RightsInfo
    /// 6.6.1. Метод для получения информации о существующих правах
    /// </summary>
    [DataContract]
    public class RightsInfo
    {
        /// <summary>
        /// Псевдоним права в системе
        /// </summary>
        [DataMember(Name = "right")]
        public string Right { get; set; }

        /// <summary>
        /// Описание права
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
