namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.1. Информация об иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class GetSsccHierarchyResponse
    {
        /// <summary>
        /// Иерархия вложенности "вверх".
        /// </summary>
        /// <remarks>
        /// Описывающий иерархию вложенности "вверх" массив упорядочен согласно уровням
        /// вложенности упаковки и в качестве первого элемента содержит описание для
        /// запрошенного идентификационного кода третичной упаковки, а в качестве последнего
        /// элемента — описание для идентификационного кода третичной упаковки самого верхнего
        /// уровня.
        /// </remarks>
        [DataMember(Name = "up")]
        public SsccInfo[] Up { get; set; }

        /// <summary>
        /// Иерархия вложенности "вниз".
        /// </summary>
        /// <remarks>
        /// Содержит информацию о вложенности третичной упаковки,
        /// начиная с запрошенного идентификационного кода третичной упаковки.
        /// </remarks>
        [DataMember(Name = "down")]
        public SsccInfo[] Down { get; set; }

        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        /// <remarks>
        /// В случае успешного поиска информация об ошибке отсутствует
        /// </remarks>
        [DataMember(Name = "error_code", IsRequired = false)]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        /// <remarks>
        /// В случае успешного поиска информация об ошибке отсутствует
        /// </remarks>
        [DataMember(Name = "error_desc", IsRequired = false)]
        public string ErrorDescription { get; set; }
    }
}
