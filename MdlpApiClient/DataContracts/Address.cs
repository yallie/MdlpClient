namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.28. Формат объекта Address
    /// Таблица 24. Формат объекта Address
    /// 8.1.2. Адрес места осуществления деятельности.
    /// </summary>
    [DataContract]
    public class Address
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
        /// Текстовый адрес объекта
        /// </summary>
        [DataMember(Name = "address_description")]
        public string AddressDescription { get; set; }
    }
}
