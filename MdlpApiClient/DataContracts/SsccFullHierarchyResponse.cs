namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.3. Информация о полной иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class SsccFullHierarchyResponse<T>
    {
        /// <summary>
        /// Иерархия вложенности "вверх".
        /// </summary>
        [DataMember(Name = "up")]
        public T Up { get; set; }

        /// <summary>
        /// Иерархия вложенности "вниз".
        /// </summary>
        [DataMember(Name = "down")]
        public T Down { get; set; }
    }
}
