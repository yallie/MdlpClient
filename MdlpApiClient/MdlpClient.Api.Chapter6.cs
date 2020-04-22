namespace MdlpApiClient
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
        public AccountSystem RegisterAccountSystem(string sysId, string name)
        {
            var accSystem = Post<AccountSystem>("registration/accounting_system", new
            {
                sys_id = sysId,
                name = name,
            });

            // при регистрации поле Name не возвращается
            accSystem.Name = name;
            return accSystem;
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

        /// <summary>
        /// 6.1.5. Метод для получения информации о языке текущего пользователя
        /// </summary>
        /// <returns>Свойства пользователя</returns>
        public string GetCurrentLanguage()
        {
            return Get<UserPreferences>("users/current/preferences").Language;
        }

        /// <summary>
        /// 6.1.6. Метод для изменения данных профиля пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="user">Свойства профиля пользователя</param>
        public void UpdateUserProfile(string userId, UserEditProfileEntry user)
        {
            Put("users/{user_id}", new
            {
                user_id = userId,
                user = user,
            },
            new[]
            {
                new Parameter("user_id", userId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 6.1.7. Метод для получения информации о текущем пользователе
        /// </summary>
        /// <returns>Свойства пользователя</returns>
        public GroupedUser GetCurrentUserInfo()
        {
            return Get<GetUserResponse>("users/current").User;
        }

        /// <summary>
        /// 6.1.8. Метод для изменения языка в профиле текущего пользователя
        /// </summary>
        /// <param name="language">Язык интерфейса пользователя (ru/en)</param>
        public void SetCurrentLanguage(string language)
        {
            Put("users/current/preferences", new
            {
                language = language,
            });
        }

        /// <summary>
        /// 6.1.9. Метод для получения информации о зарегистрированных сертификатах текущего пользователя
        /// </summary>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых сертификатов</param>
        /// <param name="count">Количество записей в списке возвращаемых сертификатов</param>
        /// <returns>Список сертификатов</returns>
        public CertificatesResponse<UserCertificate> GetCurrentCertificates(int startFrom, int count)
        {
            return Post<CertificatesResponse<UserCertificate>>("users/current/keys", new
            {
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 6.1.10. Метод для получения информации о зарегистрированных сертификатах пользователя
        /// </summary>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых сертификатов</param>
        /// <param name="count">Количество записей в списке возвращаемых сертификатов</param>
        /// <returns>Список сертификатов</returns>
        public CertificatesResponse<UserCertificate> GetUserCertificates(string userId, int startFrom, int count)
        {
            return Post<CertificatesResponse<UserCertificate>>("users/{user_id}/keys", new
            {
                start_from = startFrom,
                count = count,
            },
            new[]
            {
                new Parameter("user_id", userId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 6.1.11. Метод для получения информации об УС
        /// </summary>
        /// <param name="accountSystemId">Уникальный идентификатор УС</param>
        /// <returns>Свойства УС</returns>
        public AccountSystem GetAccountSystem(string accountSystemId)
        {
            return Get<GetAccountSystemResponse>("account_systems/{account_system_id}", new[]
            {
                new Parameter("account_system_id", accountSystemId, ParameterType.UrlSegment),
            })
            .AccountSystem;
        }

        /// <summary>
        /// 6.2.1. Метод для получения кода аутентификации
        /// </summary>
        /// <remarks>
        /// Это внутренний метод, он не является частью публичного API.
        /// Он используется классами-наследниками <see cref="CredentialsBase"/>.
        /// </remarks>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="clientSecret">Секретный ключ клиента</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <param name="authType">Тип аутентификации: PASSWORD или SIGNED_CODE</param>
        /// <returns>Код аутентификации для получения ключа сессии</returns>
        internal string Authenticate(string clientId, string clientSecret, string userId, string authType)
        {
            var auth = Post<AuthResponse>("auth", new
            {
                client_id = clientId,
                client_secret = clientSecret,
                user_id = userId,
                auth_type = authType,
            });

            return auth.Code;
        }

        /// <summary>
        /// 6.2.2. Метод для получения ключа сессии
        /// </summary>
        /// <remarks>
        /// Это внутренний метод, он не является частью публичного API.
        /// Он используется классами-наследниками <see cref="CredentialsBase"/>.
        /// </remarks>
        /// <param name="authCode">Код аутентификации для получения ключа сессии</param>
        /// <param name="signature">Открепленная подпись кода для аутентификации типа SIGNED_CODE</param>
        /// <param name="password">Пароль пользователя для аутентификации типа PASSWORD</param>
        /// <returns>Ключ сессии <see cref="AuthToken"/>.</returns>
        internal AuthToken GetToken(string authCode, string signature = null, string password = null)
        {
            return Post<AuthToken>("token", new
            {
                code = authCode,
                signature = signature,
                password = password,
            });
        }

        /// <summary>
        /// 6.2.3. Метод для выхода из системы
        /// </summary>
        internal void Logout()
        {
            Get("auth/logout");
        }
    }
}
