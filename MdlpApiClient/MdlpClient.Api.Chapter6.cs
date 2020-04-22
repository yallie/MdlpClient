﻿namespace MdlpApiClient
{
    using DataContracts;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 6: users accounts.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 6.1.1. Метод для регистрации учетной системы
        /// </summary>
        /// <param name="sysId">Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»</param>
        /// <param name="name">Название учетной системы</param>
        /// <returns>Идентификатор учетной системы</returns>
        public AccountingSystem RegisterAccountingSystem(string sysId, string name)
        {
            return Post<AccountingSystem>("registration/accounting_system", new
            {
                sys_id = sysId,
                name = name,
            });
        }

        /// <summary>
        /// 6.1.2. Метод для регистрации пользователей (для резидентов страны)
        /// </summary>
        /// <param name="sysId">Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»</param>
        /// <param name="user">Свойства пользователя-резидента</param>
        /// <returns>Идентификатор пользователя</returns>
        public string RegisterUser(string sysId, ResidentUser user)
        {
            var response = Post<RegisterUserResponse>("registration/user_resident", new
            {
                sys_id = sysId,
                first_name = user.FirstName,
                last_name = user.LastName,
                middle_name = user.MiddleName,
                public_cert = user.PublicCertificate,
                email = user.Email,
                phone = user.Phone,
                position = user.Position,
            });

            return response.UserID;
        }

        /// <summary>
        /// 6.1.3. Метод для регистрации пользователей (для нерезидентов страны)
        /// </summary>
        /// <param name="sysId">Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»</param>
        /// <param name="user">Свойства пользователя-нерезидента</param>
        /// <returns>Идентификатор пользователя</returns>
        public string RegisterUser(string sysId, NonResidentUser user)
        {
            var response = Post<RegisterUserResponse>("registration/user_nonresident", new
            {
                sys_id = sysId,
                first_name = user.FirstName,
                last_name = user.LastName,
                middle_name = user.MiddleName,
                password = user.Password,
                email = user.Email,
                phone = user.Phone,
                position = user.Position,
            });

            return response.UserID;
        }

        /// <summary>
        /// 6.1.4. Метод для получения информации о пользователе
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Свойства пользователя</returns>
        public GroupedUser GetUserInfo(string userId)
        {
            return Get<GetUserResponse>("users/{user_id}", new[]
            {
                new Parameter("user_id", userId, ParameterType.UrlSegment),
            })
            .User;
        }
    }
}
