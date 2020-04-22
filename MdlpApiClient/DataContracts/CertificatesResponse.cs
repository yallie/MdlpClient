namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список сертификатов:
    /// 6.1.9. Метод для получения информации о зарегистрированных сертификатах текущего пользователя
    /// 6.1.10. Метод для получения информации о зарегистрированных сертификатах пользователя
    /// </summary>
    /// <typeparam name="T">Тип поля Items</typeparam>
    [DataContract]
    public class CertificatesResponse<T>
    {
        [DataMember(Name = "certs")]
        public T[] Certificates { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
