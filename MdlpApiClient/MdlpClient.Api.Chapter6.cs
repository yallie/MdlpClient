namespace MdlpApiClient
{
    using DataContracts;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 6: users accounts.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 6.1.2. Метод для регистрации пользователей (для резидентов страны)
        /// </summary>
        /// <param name="sysId">Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»</param>
        /// <param name="publicCertificate">Публичный сертификат пользователя</param>
        /// <param name="firstName">Имя пользователя</param>
        /// <param name="lastName">Фамилия пользователя</param>
        /// <param name="middleName">Отчество пользователя</param>
        /// <param name="email">Электронная почта</param>
        /// <param name="phone">Контактный телефон</param>
        /// <param name="position">Должность</param>
        /// <returns>Идентификатор пользователя</returns>
        public string RegisterResidentUser(string sysId, string publicCertificate,
            string firstName, string lastName, string middleName,
            string email, string phone, string position)
        {
            var user = Post<RegisterResidentUserResponse>("registration/user_resident", new
            {
                sys_id = sysId,
                public_cert = publicCertificate,
                first_name = firstName,
                last_name = lastName,
                middle_name = middleName,
                email = email,
                phone = phone,
                position = position,
            });

            return user.UserID;
        }
    }
}
