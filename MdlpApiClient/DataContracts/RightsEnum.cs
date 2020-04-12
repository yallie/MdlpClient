namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.11. Список прав пользователей учетной системы
    /// </summary>
    public class RightsEnum
    {
        public const string MANAGE_ACCOUNTS = "MANAGE_ACCOUNTS"; // Управление учетками
        public const string VIEW_ACCOUNTS = "VIEW_ACCOUNTS"; // Просмотр учетных записей
        public const string UPLOAD_DOCUMENT = "UPLOAD_DOCUMENT"; // Загрузка документа
        public const string OUTCOME_LIST = "OUTCOME_LIST"; // Информация об исходящем документе
        public const string INCOME_LIST = "INCOME_LIST"; // Информация о входящих документах
        public const string DOWNLOAD_DOCUMENT = "DOWNLOAD_DOCUMENT"; // Получение ссылки на документ по идентификатору
        public const string REESTR_ALL = "REESTR_ALL"; // Доступ к реестрам (ко всем справочникам)
        public const string REESTR_FEDERAL_SUBJECT = "REESTR_FEDERAL_SUBJECT"; // Реестр субъектов РФ
        public const string REESTR_EGRUL = "REESTR_EGRUL"; // Реестр ЕГРЮЛ
        public const string REESTR_EGRIP = "REESTR_EGRIP"; // Реестр ЕГРИП
        public const string REESTR_REFP = "REESTR_REFP"; // Реестр аккредитованных филиалов и представительств
        internal const string REESTR_DUES = "REESTR_DUES"; // Реестр налоговой задолженности
        public const string REESTR_PROD_LICENSES = "REESTR_PROD_LICENSES"; // Реестр лицензий на производство
        public const string REESTR_PHARM_LICENSES = "REESTR_PHARM_LICENSES"; // Реестр лицензий на фарм. деятельность
        public const string REESTR_ESKLP = "REESTR_ESKLP"; // Реестр ЕСКЛП
        internal const string REESTR_GS1 = "REESTR_GS1"; // Реестр ГС1 (GS1)
        public const string REESTR_FIAS = "REESTR_FIAS"; // Реестр ФИАС
        public const string VIEW_BILLING_PRIORITY_RULES = "VIEW_BILLING_PRIORITY_RULES"; // Просмотр реестра приоритетной оплаты
        public const string MANAGE_BILLING_PRIORITY_RULES = "MANAGE_BILLING_PRIORITY_RULES"; // Редактирование реестра приоритетной оплаты
        public const string REESTR_SGTIN = "REESTR_SGTIN"; // Реестр КИЗ
        public const string REESTR_SGTIN_BILLING = "REESTR_SGTIN_BILLING"; // Реестр КИЗ для биллинга
        internal const string REESTR_OWNED_SSCC_SGTIN = "REESTR_OWNED_SSCC_SGTIN"; // Реестр КИЗ и реестр третичных упаковок с учетом текущего владельца
        public const string REESTR_MED_PRODUCTS = "REESTR_MED_PRODUCTS"; // Реестр производимых ЛП
        public const string MANAGE_TRUSTED_PARTNERS = "MANAGE_TRUSTED_PARTNERS"; // Редактирование реестра доверенных контрагентов
        public const string VIEW_TRUSTED_PARTNERS = "VIEW_TRUSTED_PARTNERS"; // Просмотр реестра доверенных контрагентов
        public const string MANAGE_BRANCH = "MANAGE_BRANCH"; // Редактирование реестра мест деятельности (МД)
        public const string MANAGE_SAFE_WAREHOUSE = "MANAGE_SAFE_WAREHOUSE"; // Редактирование реестра складов/мест ответственного хранения СОХ/МОХ
        public const string VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG = "VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG"; // Реестр заявок на регистрацию иностранных контрагентов
        public const string MANAGE_FOREIGN_COUNTERPARTY = "MANAGE_FOREIGN_COUNTERPARTY"; // Управление иностранными контрагентами
        internal const string MANAGE_MEMBER = "MANAGE_MEMBER"; // Управление организацией
        public const string REESTR_COUNTERPARTY = "REESTR_COUNTERPARTY"; // Реестр контрагентов
        public const string REESTR_REGISTRATION_DEVICES = "REESTR_REGISTRATION_DEVICES"; // Реестра регистраторов эмиссии/выбытия
        public const string REESTR_VIRTUAL_STORAGE = "REESTR_VIRTUAL_STORAGE"; // Реестр виртуального склада
        public const string MEMBER_PAYMENT_INFO = "MEMBER_PAYMENT_INFO"; // Финансовая информация
        public const string REESTR_PAUSED_CIRCULATION_DECISION = "REESTR_PAUSED_CIRCULATION_DECISION"; // Реестр решений о приостановке КИЗ
        public const string VIEW_SKZKM_REPORT = "VIEW_SKZKM_REPORT"; // Прослеживание документов по отчёту из СУЗ
        internal const string VIEW_BATCH_GRAF = "VIEW_BATCH_GRAF"; // Просмотр дерева по производственной серии для производителя
    }
}
