namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Базовая информация о пользователе
    /// </summary>
    [DataContract]
    public abstract class UserBase
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DataMember(Name = "first_name", IsRequired = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DataMember(Name = "last_name", IsRequired = true)]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [DataMember(Name = "middle_name", IsRequired = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        [DataMember(Name = "position", IsRequired = false)]
        public string Position { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Статус пользователя, см. значения <see cref="UserStatus"/>.
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public string Status { get; set; }

        /// <summary>
        /// Причина, по которой пользователь переведен в статус, см. значения <see cref="UserStatusReason"/>.
        /// </summary>
        [DataMember(Name = "status_change_reason", IsRequired = false)]
        public string StatusChangeReason { get; set; }

        /// <summary>
        /// Дата и время последнего входа в систему.
        /// </summary>
        [DataMember(Name = "last_login_time", IsRequired = false)]
        public DateTime? LastLoginTime { get; set; }
    }
}
