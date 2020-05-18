namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.16. Список причин смены статуса пользователя (UserStatusReason)
    /// Таблица 12. Список причин смены статуса пользователя
    /// </summary>
    public class UserStatusReason
    {
        /// <summary>
        /// Заблокирован вручную.
        /// </summary>
        public const string MANUAL_BLOCK = "MANUAL_BLOCK";

        /// <summary>
        /// Временно заблокирован по причине превышения количества неверных попыток входа.
        /// </summary>
        public const string TEMPORARY_BLOCK = "TEMPORARY_BLOCK";

        /// <summary>
        /// Заблокирован по причине длительного бездействия.
        /// </summary>
        public const string INACTIVE_BLOCK = "INACTIVE_BLOCK";

        /// <summary>
        /// Заблокирован по причине истечения срока действия пароля.
        /// </summary>
        public const string PASSWORD_EXPIRED = "PASSWORD_EXPIRED";
    }
}
