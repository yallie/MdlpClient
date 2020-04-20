namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта AddressResolved
    /// </summary>
    [DataContract]
    public class AddressResolved
    {
        /// <summary>
        /// Код выполнения операции:
        /// 0 — операция выполнена успешно, адрес найден
        /// 1 — адрес не может быть идентифицирован в БД ФИАС        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Адрес установки (код ФИАС)
        /// </summary>
        [DataMember(Name = "houseguid")]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Текстовый адрес объекта
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }
    }
}
