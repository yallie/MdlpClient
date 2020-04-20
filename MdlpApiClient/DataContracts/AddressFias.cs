namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта AddressFias
    /// </summary>
    [DataContract]
    public class AddressFias
    {
        /// <summary>
        /// Уникальный идентификатор адресного объекта (ФИАС)
        /// </summary>
        [DataMember(Name = "aoguid")]
        public string AoGuid { get; set; }

        /// <summary>
        /// Адрес установки (код ФИАС)
        /// </summary>
        [DataMember(Name = "houseguid")]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Комната, 1-50 символов
        /// </summary>
        [DataMember(Name = "room")]
        public string Room { get; set; }
    }
}
