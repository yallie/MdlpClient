namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.15. Список статусов пользователя (UserStatus)    /// Таблица 11. Список статусов пользователя
    /// </summary>
    public class UserStatus
    {
        /// <summary>
        /// Активен.
        /// </summary>
        public const string ACTIVE = "ACTIVE";

        /// <summary>
        /// Заблокирован.
        /// </summary>
        public const string BLOCKED = "BLOCKED";

        /// <summary>
        /// Удален.
        /// </summary>
        public const string DELETED = "DELETED";
    }
}
