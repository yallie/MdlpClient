namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.11. Список прав пользователей учетной системы
    /// </summary>
    public class RightsEnum
    {
        /// <summary>
        /// Позволяет по идентификатору документа получить ссылку на документ.
        /// </summary>
        public const string DOWNLOAD_DOCUMENT = "DOWNLOAD_DOCUMENT";

        /// <summary>
        /// Позволяет получить информацию о входящих документах
        /// </summary>
        public const string INCOME_LIST = "INCOME_LIST";

        /// <summary>
        /// Позволяет управлять учетными системами, пользователями, группами прав пользователей
        /// </summary>
        public const string MANAGE_ACCOUNTS = "MANAGE_ACCOUNTS";

        /// <summary>
        /// Редактирование реестра приоритетной оплаты
        /// </summary>
        public const string MANAGE_BILLING_PRIORITY_RULES = "MANAGE_BILLING_PRIORITY_RULES";

        /// <summary>
        /// Позволяет добавлять МД
        /// </summary>
        public const string MANAGE_BRANCH = "MANAGE_BRANCH";

        /// <summary>
        /// Позволяет взаимодействовать с анкетами на РЭ
        /// </summary>
        public const string MANAGE_EMISSION_FORM = "MANAGE_EMISSION_FORM";

        /// <summary>
        /// Позволяет регистрировать иностранных контрагентов
        /// </summary>
        public const string MANAGE_FOREIGN_COUNTERPARTY = "MANAGE_FOREIGN_COUNTERPARTY";

        /// <summary>
        /// Позволяет регистрировать ЛП и получать список заявок на регистрацию ЛП
        /// </summary>
        public const string MANAGE_LP = "MANAGE_LP";

        /// <summary>
        /// Позволяет управлять организацией
        /// </summary>
        internal const string MANAGE_MEMBER = "MANAGE_MEMBER";

        /// <summary>
        /// Позволяет взаимодействовать с заявлениями
        /// </summary>
        public const string MANAGE_MEMBER_APPLICATIONS = "MANAGE_MEMBER_APPLICATIONS";

        /// <summary>
        /// Позволяет редактировать адрес в лицензиях на фармацевтическую деятельность
        /// </summary>
        public const string MANAGE_PHARM_LICENSE_ADDRESS = "MANAGE_PHARM_LICENSE_ADDRESS";

        /// <summary>
        /// Позволяет редактировать адрес в лицензиях на производство ЛП
        /// </summary>
        public const string MANAGE_PROD_LICENSE_ADDRESS = "MANAGE_PROD_LICENSE_ADDRESS";

        /// <summary>
        /// Позволяет добавлять СОХ/МОХ
        /// </summary>
        public const string MANAGE_SAFE_WAREHOUSE = "MANAGE_SAFE_WAREHOUSE";

        /// <summary>
        /// Создание, редактирование и отправка заявок в СТП
        /// </summary>
        public const string MANAGE_SERVICE_REQUESTS = "MANAGE_SERVICE_REQUESTS";

        /// <summary>
        /// Позволяет управлять доверенными контрагентами при обратном акцептировании
        /// </summary>
        public const string MANAGE_TRUSTED_PARTNERS = "MANAGE_TRUSTED_PARTNERS";

        /// <summary>
        /// Позволяет взаимодействовать с анкетами на РВ
        /// </summary>
        public const string MANAGE_WITHDRAWAL_FORM = "MANAGE_WITHDRAWAL_FORM";

        /// <summary>
        /// Управление атрибутами ЛП
        /// </summary>
        public const string MANAGEMENT_ATTR_LP = "MANAGEMENT_ATTR_LP";

        /// <summary>
        /// Позволяет взаимодействовать с финансовыми данными участника
        /// </summary>
        public const string MEMBER_PAYMENT_INFO = "MEMBER_PAYMENT_INFO";

        /// <summary>
        /// Позволяет получить информацию о исходящих документах
        /// </summary>
        public const string OUTCOME_LIST = "OUTCOME_LIST";

        /// <summary>
        /// Позволяет получить доступ ко всем справочникам
        /// </summary>
        public const string REESTR_ALL = "REESTR_ALL";

        /// <summary>
        /// Получение информации из реестра контрагентов
        /// </summary>
        public const string REESTR_COUNTERPARTY = "REESTR_COUNTERPARTY";

        /// <summary>
        /// Получение информации из реестра налоговой задолженности
        /// </summary>
        internal const string REESTR_DUES = "REESTR_DUES";

        /// <summary>
        /// Получение информации из реестра ЕГРИП
        /// </summary>
        public const string REESTR_EGRIP = "REESTR_EGRIP";

        /// <summary>
        /// Получение информации из реестра ЕГРЮЛ
        /// </summary>
        public const string REESTR_EGRUL = "REESTR_EGRUL";

        /// <summary>
        /// Получение информации из реестра ЕСКЛП
        /// </summary>
        public const string REESTR_ESKLP = "REESTR_ESKLP";

        /// <summary>
        /// Просмотр информации МД/МОХ
        /// </summary>
        public const string REESTR_FEDERAL_SUBJECT = "REESTR_FEDERAL_SUBJECT";

        /// <summary>
        /// Получение информации из реестра ФИАС
        /// </summary>
        public const string REESTR_FIAS = "REESTR_FIAS";

        /// <summary>
        /// Получение информации из реестра ГС1 (GS1)
        /// </summary>
        internal const string REESTR_GS_1 = "REESTR_GS_1";

        /// <summary>
        /// Получение информации из реестра производимых ЛП
        /// </summary>
        public const string REESTR_MED_PRODUCTS = "REESTR_MED_PRODUCTS";

        /// <summary>
        /// Получение информации из реестра КИЗ и по третичной упаковке с учетом текущего владельца
        /// </summary>
        internal const string REESTR_OWNED_SSCC_SGTIN = "REESTR_OWNED_SSCC_SGTIN";

        /// <summary>
        /// Позволяет просматривать реестр субъектов обращения
        /// </summary>
        public const string REESTR_PARTNERS = "REESTR_PARTNERS";

        /// <summary>
        /// Получение информации из реестра решений о приостановке ЛП
        /// </summary>
        public const string REESTR_PAUSED_CIRCULATION_DECISION = "REESTR_PAUSED_CIRCULATION_DECISION";

        /// <summary>
        /// Получение информации из реестра лицензий на фармацевтическую деятельность
        /// </summary>
        public const string REESTR_PHARM_LICENSES = "REESTR_PHARM_LICENSES";

        /// <summary>
        /// Получение информации из реестра лицензий на производство
        /// </summary>
        public const string REESTR_PROD_LICENSES = "REESTR_PROD_LICENSES";

        /// <summary>
        /// Получение информации из реестра аккредитованных филиалов и представительств
        /// </summary>
        public const string REESTR_REFP = "REESTR_REFP";

        /// <summary>
        /// Получение информации из реестра устройств регистрации
        /// </summary>
        public const string REESTR_REGISTRATION_DEVICES = "REESTR_REGISTRATION_DEVICES";

        /// <summary>
        /// Получение информации из реестра КИЗ
        /// </summary>
        public const string REESTR_SGTIN = "REESTR_SGTIN";

        /// <summary>
        /// Доступ к реестру КИЗ, подлежащих оплате
        /// </summary>
        public const string REESTR_SGTIN_BILLING = "REESTR_SGTIN_BILLING";

        /// <summary>
        /// Получение информации из реестра виртуального склада
        /// </summary>
        public const string REESTR_VIRTUAL_STORAGE = "REESTR_VIRTUAL_STORAGE";

        /// <summary>
        /// Позволяет загружать документ
        /// </summary>
        public const string UPLOAD_DOCUMENT = "UPLOAD_DOCUMENT";

        /// <summary>
        /// Позволяет просматривать учетные системы, пользователей, группы прав пользователей
        /// </summary>
        public const string VIEW_ACCOUNTS = "VIEW_ACCOUNTS";

        /// <summary>
        /// Позволяет просмотривать и создавать заявки на корректировку сведений ЛП
        /// </summary>
        public const string VIEW_AND_CREATE_APPLICATION_CORRECT_MEDICINE_INFO = "VIEW_AND_CREATE_APPLICATION_CORRECT_MEDICINE_INFO";

        /// <summary>
        /// Просмотр дерева по производственной серии
        /// </summary>
        internal const string VIEW_BATCH_GRAF = "VIEW_BATCH_GRAF";

        /// <summary>
        /// Доступ к реестру приоритетной оплаты
        /// </summary>
        public const string VIEW_BILLING_PRIORITY_RULES = "VIEW_BILLING_PRIORITY_RULES";

        /// <summary>
        /// Позволяет получать информацию о заявках на регистрацию иностранных контрагентов
        /// </summary>
        public const string VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG = "VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG";

        /// <summary>
        /// Просмотр заявок в СТП
        /// </summary>
        public const string VIEW_SERVICE_REQUESTS = "VIEW_SERVICE_REQUESTS";

        /// <summary>
        /// Получение трассировки документов о нанесении
        /// </summary>
        public const string VIEW_SKZKM_REPORT = "VIEW_SKZKM_REPORT";

        /// <summary>
        /// Позволяет просматривать информацию по доверенным контрагентам при обратном акцептировании
        /// </summary>
        public const string VIEW_TRUSTED_PARTNERS = "VIEW_TRUSTED_PARTNERS";
    }
}
