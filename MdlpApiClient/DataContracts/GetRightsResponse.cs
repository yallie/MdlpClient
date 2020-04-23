namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.1. Метод для получения информации о существующих правах
    /// </summary>
    [DataContract]
    internal class GetRightsResponse<T>
    {
        /// <summary>
        /// Права и описание
        /// </summary>
        [DataMember(Name = "rights", IsRequired = true)]
        public T[] Rights { get; set; }
    }
}
