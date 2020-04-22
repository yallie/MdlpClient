namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.9. Метод для получения информации о зарегистрированных сертификатах текущего пользователя
    /// 6.1.10. Метод для получения информации о зарегистрированных сертификатах пользователя
    /// Формат объекта UserCert    /// </summary>
    [DataContract]
    public class UserCertificate
    {
        /// <summary>
        /// Серийный номер публичного сертификата пользователя
        /// </summary>
        [DataMember(Name = "public_cert_serial_number", IsRequired = true)]
        public string PublicCertificateSerialNumber { get; set; }

        /// <summary>
        /// Отпечаток публичного сертификата пользователя
        /// </summary>
        [DataMember(Name = "public_cert_thumbprint", IsRequired = true)]
        public string PublicCertificateThumbprint { get; set; }

        /// <summary>
        /// Действует с
        /// </summary>
        [DataMember(Name = "valid_from", IsRequired = true)]
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Действует до
        /// </summary>
        [DataMember(Name = "valid_to", IsRequired = true)]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        [DataMember(Name = "org_name", IsRequired = true)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Владелец
        /// </summary>
        [DataMember(Name = "owner", IsRequired = true)]
        public string Owner { get; set; }

        /// <summary>
        /// Выписан на (ФИО владельца)
        /// </summary>
        [DataMember(Name = "owner_fio", IsRequired = true)]
        public string OwnerFio { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [DataMember(Name = "inn", IsRequired = true)]
        public string Inn { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [DataMember(Name = "position", IsRequired = false)]
        public string Position { get; set; }

        /// <summary>
        /// Выдан кем
        /// </summary>
        [DataMember(Name = "issuer_name", IsRequired = true)]
        public string IssuerName { get; set; }
    }
}
