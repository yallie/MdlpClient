namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.14. Список типов аутентификации (AuthType)
    /// Таблица 10. Список типов аутентификации    /// </summary>
    public class AuthTypeEnum
    {
        /// <summary>
        /// Аутентификация с помощью пароля.
        /// </summary>
        public const string PASSWORD = "PASSWORD";

        /// <summary>
        /// Аутентификация с помощью подписанного одноразового кода.
        /// </summary>
        public const string SIGNED_CODE = "SIGNED_CODE";
    }
}
