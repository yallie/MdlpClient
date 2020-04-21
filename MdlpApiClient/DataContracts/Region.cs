namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.9.2. Метод для получения списка субъектов РФ
    /// Структура данных Region
    /// </summary>
    [DataContract]
    public class Region
    {
        /// <summary>
        /// Название
        /// </summary>
        [DataMember(Name = "title", IsRequired = true)]
        public string Title { get; set; }

        /// <summary>
        /// Код субъекта
        /// </summary>
        [DataMember(Name = "code", IsRequired = true)]
        public string Code { get; set; }

        /// <summary>
        /// Идентификатор субъекта
        /// </summary>
        /// <remarks>
        /// Имеется далеко не всегда
        /// </remarks>
        [DataMember(Name = "key", IsRequired = false)]
        public string Key { get; set; }

        /// <summary>
        /// Список идентификаторов дочерних субъектов
        /// </summary>
        [DataMember(Name = "children", IsRequired = false)]
        public string[] Children { get; set; }
    }
}
