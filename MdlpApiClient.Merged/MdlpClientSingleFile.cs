// This is an auto-generated file.
namespace MdlpApiClient
{
    using DataContracts;
    using RestSharp;
    using RestSharp.Authenticators;

    /// <summary>
    /// MDLP REST API authenticator using credentials.
    /// </summary>
    internal class CredentialsAuthenticator : IAuthenticator
    {
        public CredentialsAuthenticator(MdlpClient apiClient, CredentialsBase credentials)
        {
            State = AuthState.NotAuthenticated;
            Client = apiClient;
            Credentials = credentials;
        }

        private MdlpClient Client { get; set; }

        private CredentialsBase Credentials { get; set; }

        private AuthState State { get; set; }

        private enum AuthState
        {
            NotAuthenticated, InProgress, Authenticated
        }

        private AuthToken AuthToken { get; set; }

        private string AuthHeader { get; set; }

        public void SetAuthToken(string authToken)
        {
            AuthHeader = string.IsNullOrWhiteSpace(authToken) ?
                null : "token " + authToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            // perform authentication request
            if (State == AuthState.NotAuthenticated)
            {
                State = AuthState.InProgress;
                AuthToken = Credentials.Authenticate(Client);
                SetAuthToken(AuthToken.Token);
                State = AuthState.Authenticated;
            }

            // add authorization header if any
            if (!string.IsNullOrWhiteSpace(AuthHeader))
            {
               request.AddOrUpdateParameter("Authorization", AuthHeader, ParameterType.HttpHeader);
            }
        }
    }
}

namespace MdlpApiClient
{
    using DataContracts;

    /// <summary>
    /// MDLP REST API credentials base class.
    /// </summary>
    public abstract class CredentialsBase
    {
        /// <summary>
        /// Client identifier.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the user identity.
        /// </summary>
        /// <remarks>
        /// For resident users: installed GOST certificate subject name or thumbprint.
        /// </remarks>
        public string UserID { get; set; }

        /// <summary>
        /// Performs authentication, returns access token with a limited lifetime.
        /// </summary>
        /// <param name="apiClient">MDLP client to perform API calls.</param>
        /// <returns><see cref="AuthToken"/> instance.</returns>
        public abstract AuthToken Authenticate(MdlpClient restClient);
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Адрес места осуществления деятельности.
    /// </summary>
    [DataContract]
    public class Address
    {
        [DataMember(Name = "aoguid")]
        public string AoGuid { get; set; }

        [DataMember(Name = "houseguid")]
        public string HouseGuid { get; set; }

        [DataMember(Name = "address_description")]
        public string AddressDescription { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class AuthResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// MDLP REST API authentication token.
    /// </summary>
    [DataContract]
    public class AuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthToken"/>.
        /// </summary>
        public AuthToken()
        {
            // make sure we don't expire prematurely
            CreationDate = DateTime.Now.AddSeconds(-30);
        }

        [IgnoreDataMember]
        public DateTime CreationDate { get; private set; }

        [IgnoreDataMember]
        public DateTime ExpirationDate
        {
            get { return CreationDate.AddMinutes(LifeTime); }
        }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "life_time")]
        public int LifeTime { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Место осуществления деятельности.
    /// </summary>
    [DataContract]
    public class BranchEntry
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code")]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Название субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_name")]
        public string FederalSubjectName { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrgName { get; set; }

        /// <summary>
        /// Перечень работ/услуг согласно лицензии
        /// </summary>
        [DataMember(Name = "work_list")]
        public string[] WorkList { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности
        /// </summary>
        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [DataMember(Name = "registration_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата приостановления
        /// </summary>
        [DataMember(Name = "suspension_date", IsRequired = false)]
        public DateTime? SuspensionDate { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Фильтр для мест осуществления деятельности.
    /// Содержит информацию для фильтрации списка мест осуществления деятельности.
    /// </summary>
    [DataContract]
    public class BranchFilter
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Уникальный идентификатор дома
        /// </summary>
        [DataMember(Name = "houseguid", IsRequired = false)]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Код округа РФ
        /// </summary>
        [DataMember(Name = "federal_district_code", IsRequired = false)]
        public string FederalDistrictCode { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата начала периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.18. Формат объекта DocFilter
    /// Содержит информацию для фильтрации списка документов.
    /// </summary>
    [DataContract]
    public class DocFilter
    {
        /// <summary>
        /// Дата начала периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Дата обработки документа: начало периода
        /// </summary>
        [DataMember(Name = "processed_date_from", IsRequired = false)]
        public DateTime? ProcessedDateFrom { get; set; }

        /// <summary>
        /// Дата обработки документа: окончание периода
        /// </summary>
        [DataMember(Name = "processed_date_to", IsRequired = false)]
        public DateTime? ProcessedDateTo { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        [DataMember(Name = "document_id", IsRequired = false)]
        public string DocumentID { get; set; }

        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [DataMember(Name = "request_id", IsRequired = false)]
        public string RequestID { get; set; }

        /// <summary>
        /// Уникальный идентификатор отправителя.
        /// Идентификатор места осуществления деятельности, места ответственного 
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»        /// </summary>
        [DataMember(Name = "sender_id", IsRequired = false)]
        public string SenderID { get; set; }

        /// <summary>
        /// Уникальный идентификатор получателя.
        /// Идентификатор места осуществления деятельности, места ответственного 
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»        /// Применимо для входящих документов.        /// </summary>
        [DataMember(Name = "receiver_id", IsRequired = false)]
        public string ReceiverID { get; set; }

        /// <summary>
        /// Тип документа. Соответствует номеру схемы XSD.
        /// </summary>
        [DataMember(Name = "doc_type", IsRequired = false)]
        public int? DocType { get; set; } // тут передается action_id: 311, 601, etc

        /// <summary>
        /// Статус документа. См. <see cref="DocStatusEnum"/>.
        /// </summary>
        [DataMember(Name = "doc_status", IsRequired = false)]
        public string DocStatus { get; set; }

        /// <summary>
        /// Тип загрузки в систему:
        /// 0 — УСО
        /// 1 — ЛК (личный кабинет)
        /// 2 — API
        /// 3 — ОФД (Оператор фискальных данных)
        /// 4 — СКЗКМ/ИС МП
        /// </summary>
        [DataMember(Name = "file_uploadtype", IsRequired = false)]
        public int? FileUploadType { get; set; } // 1 УСО, 2 ЛК, 3 API, 4 ОФД, 5 СКЗКМ
    }
}

namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.12. Список статусов документа (DocStatusEnum)
    /// </summary>
    public class DocStatusEnum
    {
        /// <summary>
        /// Документ загружается
        /// </summary>
        public const string UPLOADING_DOCUMENT = "UPLOADING_DOCUMENT";

        /// <summary>
        /// Документ принят и обрабатывается трансформатором
        /// </summary>
        public const string PROCESSING_DOCUMENT = "PROCESSING_DOCUMENT";

        /// <summary>
        /// Документ обработан трансформатором и принят на обработку системой
        /// </summary>
        public const string CORE_PROCESSING_DOCUMENT = "CORE_PROCESSING_DOCUMENT";

        /// <summary>
        /// Документ обработан системой и трансформатор подготавливает ответ
        /// </summary>
        public const string CORE_PROCESSED_DOCUMENT = "CORE_PROCESSED_DOCUMENT";

        /// <summary>
        /// Документ обработан трансформатором и готов для загрузки
        /// </summary>
        public const string PROCESSED_DOCUMENT = "PROCESSED_DOCUMENT";

        /// <summary>
        /// Произошла ошибка во время обработки документа
        /// </summary>
        public const string FAILED = "FAILED";

        /// <summary>
        /// Произошла ошибка во время обработки документа.
        /// Квитанция для документа с информацией о причине сбоя 
        /// сформирована и может быть получена по request_id
        /// </summary>
        public const string FAILED_RESULT_READY = "FAILED_RESULT_READY";    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.9. Получение метаданных документа
    /// </summary>
    [DataContract]
    public class DocumentMetadata
    {
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "sender")]
        public string SenderID { get; set; } // "935ba7bc-b022-11e7-abc4-cec278b6b50a",

        [DataMember(Name = "sys_id")]
        public string SystemID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        [DataMember(Name = "doc_status")]
        public string DocStatus { get; set; } // "PROCESSED_DOCUMENT",

        [DataMember(Name = "device_id")]
        public string DeviceID { get; set; } // 1230000011111111 (optional)

        [DataMember(Name = "skzkm_origin_msg_id")]
        public string SkzkmOriginMessageID { get; set; } // "e2cb20c1-1d5b-4ab6-b8dd-9297bec23f63" (optional)

        [DataMember(Name = "version")]
        public string Version { get; set; } // API version: "1.28"
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.14. Прослеживание документов по отчёту из СУЗ
    /// </summary>
    [DataContract]
    public class DocumentSkzkmMetadata
    {
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; } // "996f487c-d902-4dbd-b99f-76aef2d904dc",

        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; } // "6e491238-d4a9-495b-8d37-45181916c846",

        [DataMember(Name = "date")]
        public DateTime Date { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "doc_type")]
        public int DocType { get; set; } // 0,

        [DataMember(Name = "processing_document_status")]
        public string ProcessingDocStatus { get; set; } // "PROCESSING",

        [DataMember(Name = "processed_date")]
        public DateTime ProcessedDate { get; set; } // "2017-11-23 05:48:15",

        [DataMember(Name = "sgtin_count")]
        public int SgtinCount { get; set; } // 10
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список документов:
    /// 5.7. Исходящих документов.
    /// 5.8. Входящих документов.
    /// 5.11. Документов по идентификатору запроса.
    /// </summary>
    /// <typeparam name="T">Тип поля Documents</typeparam>
    [DataContract]
    public class DocumentsResponse<T>
    {
        [DataMember(Name = "documents")]
        public T[] Documents { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EgripRegistryResponse
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        [DataMember(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [DataMember(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [DataMember(Name = "ORG_NAME")]
        public string OrgName { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EgrulRegistryResponse
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        [DataMember(Name = "OGRN")]
        public string Ogrn { get; set; }

        [DataMember(Name = "KPP")]
        public string Kpp { get; set; }

        [DataMember(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [DataMember(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [DataMember(Name = "ORG_NAME")]
        public string OrgName { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EmptyResponse
    {
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список элементов и список ошибок.
    /// 8.3.2. Список КИЗ и список ошибок поиска.
    /// 8.3.3. Список КИЗ из общедоступного реестра КИЗ и список не найденных КИЗ.
    /// </summary>
    /// <typeparam name="T">Тип элемента поля Entries</typeparam>
    /// <typeparam name="F">Тип элемента поля FailedEntries</typeparam>
    [DataContract]
    public class EntriesFailedResponse<T, F>
    {
        [DataMember(Name = "entries")]
        public T[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "failed_entries")]
        public F[] FailedEntries { get; set; }

        [DataMember(Name = "failed")]
        public int Failed { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список записей:
    /// 8.1.2. Мест осуществления деятельности.
    /// 8.2.2. Список мест ответственного хранения.
    /// 8.3.1. Список найденных КИЗ.
    /// 8.3.5. Список КИЗ со статусом 'Оборот приостановлен'.
    /// 8.3.6. Результат поиска по реестру КИЗ записей, ожидающих
    /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// </summary>
    /// <typeparam name="T">Тип поля Entries</typeparam>
    [DataContract]
    public class EntriesResponse<T>
    {
        [DataMember(Name = "entries")]
        public T[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ErrorResponse
    {
        // Sometimes error response has this structure: { timestamp, status, error, message, path }

        [DataMember(Name = "timestamp")] // "2020-04-13T12:51:22.873+0000",
        public DateTime TimeStamp { get; set; }

        [DataMember(Name = "status")] // 404,
        public int StatusCode { get; set; }

        [DataMember(Name = "error")] // "Not Found",
        public string Error { get; set; }

        [DataMember(Name = "message")] // "Not Found",
        public string Message{ get; set; }

        [DataMember(Name = "path")] // "/api/v1/reestr/shtuchek/dryuchek"
        public string Path { get; set; }

        // And sometimes it's like { error_description }

        [DataMember(Name = "error_description")] // "Ошибка такая-то с подробностями",
        public string Description { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.5. Метод получения информации об адресах искомого участника.
    /// </summary>
    [DataContract]
    public class GetAvailableAddressesResponse
    {
        [DataMember(Name = "entries")]
        public RegistrationAddress[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.3. Получение информации о конкретном месте осуществления деятельности
    /// </summary>
    [DataContract]
    public class GetBranchResponse
    {
        /// <summary>
        /// Уникальный идентификатор места осуществления деятельности
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности
        /// </summary>
        [DataMember(Name = "address", IsRequired = false)]
        public Address Address { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.10. Получение документа по идентификатору
    /// </summary>
    [DataContract]
    public class GetDocumentResponse
    {
        [DataMember(Name = "link")]
        public string Link { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.5. Получение информации об ограничении размера небольших документов
    /// </summary>
    [DataContract]
    internal class GetLargeDocumentSizeResponse
    {
        [DataMember(Name = "doc_size")]
        public int DocSize { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Подробности о КИЗ (SGTIN) и ЛП (GTIN)
    /// </summary>
    [DataContract]
    public class GetSgtinResponse
    {
        [DataMember(Name = "sgtin_info")]
        public SgtinExtended SgtinInfo { get; set; }

        [DataMember(Name = "gtin_info")]
        public GtinInfo GtinInfo { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.1. Информация об иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class GetSsccHierarchyResponse
    {
        /// <summary>
        /// Иерархия вложенности "вверх".
        /// </summary>
        /// <remarks>
        /// Описывающий иерархию вложенности "вверх" массив упорядочен согласно уровням
        /// вложенности упаковки и в качестве первого элемента содержит описание для
        /// запрошенного идентификационного кода третичной упаковки, а в качестве последнего
        /// элемента — описание для идентификационного кода третичной упаковки самого верхнего
        /// уровня.
        /// </remarks>
        [DataMember(Name = "up")]
        public SsccInfo[] Up { get; set; }

        /// <summary>
        /// Иерархия вложенности "вниз".
        /// </summary>
        /// <remarks>
        /// Содержит информацию о вложенности третичной упаковки,
        /// начиная с запрошенного идентификационного кода третичной упаковки.
        /// </remarks>
        [DataMember(Name = "down")]
        public SsccInfo[] Down { get; set; }

        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        /// <remarks>
        /// В случае успешного поиска информация об ошибке отсутствует
        /// </remarks>
        [DataMember(Name = "error_code", IsRequired = false)]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        /// <remarks>
        /// В случае успешного поиска информация об ошибке отсутствует
        /// </remarks>
        [DataMember(Name = "error_desc", IsRequired = false)]
        public string ErrorDescription { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.2. Список КИЗ, вложенных в третичную упаковку
    /// </summary>
    [DataContract]
    public class GetSsccSgtinsResponse : EntriesResponse<Sgtin>
    {
        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        /// <remarks>
        /// Присутствует только при ошибке
        /// </remarks>
        [DataMember(Name = "error_code", IsRequired = false)]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Текстовое описание ошибки
        /// </summary>
        /// <remarks>
        /// Присутствует только при ошибке
        /// </remarks>
        [DataMember(Name = "error_desc", IsRequired = false)]
        public string ErrorDescription { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.3. Получение информации о конкретном месте ответственного хранения
    /// </summary>
    [DataContract]
    public class GetWarehouseResponse
    {
        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "warehouse_id", IsRequired = false)]
        public string WarehouseID { get; set; }

        /// <summary>
        /// Адрес места ответственного хранения
        /// </summary>
        [DataMember(Name = "address", IsRequired = false)]
        public Address Address { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Подробности о ЛП (GTIN)
    /// </summary>
    [DataContract]
    public class GtinInfo
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Статус рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_status")]
        public string RegistrationStatus { get; set; }

        /// <summary>
        /// Номер рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        [DataMember(Name = "reg_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата окончания рег. удостоверения
        /// </summary>
        [DataMember(Name = "reg_end_date", IsRequired = false)]
        public DateTime? RegistrationEndDate { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// </summary>
        [DataMember(Name = "type_form")]
        public string TypeForm { get; set; }

        /// <summary>
        /// Количество массы/объема в первичной упаковке        /// </summary>
        [DataMember(Name = "prod_pack_1_ed")]
        public string ProductPack1Amount { get; set; }

        /// <summary>
        /// Количество (мера, ед.измерения) массы/объема в первичной упаковке        /// </summary>
        [DataMember(Name = "prod_pack1_ed_name")]
        public string ProductPack1AmountName { get; set; }

        /// <summary>
        /// Адрес упаковщика
        /// </summary>
        [DataMember(Name = "packer_address")]
        public string PackerAddress { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe лекарственного препарата
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "prod_sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// Содержимое лекарственного препарата
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_content", IsRequired = false)]
        public string ProductContent { get; set; }

        /// <summary>
        /// Наименование товара на этикетке
        /// Например: лиофилизат для приготовления концентрата для приготовления раствора для инфузий "гертикад®" 150 мг, 440 мг
        /// </summary>
        [DataMember(Name = "prod_desc", IsRequired = false)]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name")]
        public string ProdDosageName { get; set; }

        /// <summary>
        /// Первичная упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack_1_name")]
        public string ProductPack1Name { get; set; }

        /// <summary>
        /// Вторичная (потребительская) упаковка (строковое представление)
        /// </summary>
        [DataMember(Name = "prod_pack_2_name")]
        public string ProductPack2Name { get; set; }

        /// <summary>
        /// Количество первичной упаковки в потребительской упаковке
        /// </summary>
        [DataMember(Name = "prod_pack_1_2")]
        public string ProductPack1InPack2 { get; set; }

        /// <summary>
        /// Код ТНВЭД ЕАЭС
        /// </summary>
        [DataMember(Name = "tn_ved")]
        public string Tnved { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp")]
        public bool Gnvlp { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена (для ЖНВЛП) (руб)
        /// </summary>
        [DataMember(Name = "max_gnvlp", IsRequired = false)]
        public string MaxGnvlpPrice { get; set; }

        /// <summary>
        /// Дата регистрации предельной цены
        /// </summary>
        [DataMember(Name = "max_gnvlp_reg_date", IsRequired = false)]
        public DateTime? MaxGnvlpPriceRegistrationDate { get; set; }

        /// <summary>
        /// Наименование держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder")]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Страна регистрации держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_country")]
        public string RegistrationHolderCountry { get; set; }

        /// <summary>
        /// Статус лекарственного препарата
        /// </summary>
        [DataMember(Name = "prod_status")]
        public string ProductStatus { get; set; }

        /// <summary>
        /// Признак регистрации в Минздраве
        /// </summary>
        [DataMember(Name = "min_zdrav")]
        public bool MinZdrav { get; set; }

        /// <summary>
        /// Признак регистрации в ГС1
        /// </summary>
        [DataMember(Name = "gs1")]
        public bool Gs1 { get; set; }

        /// <summary>
        /// Предельная зарегистрированная цена
        /// </summary>
        [DataMember(Name = "cost_limit", IsRequired = false)]
        public string CostLimit { get; set; }

        /// <summary>
        /// ИНН держателя регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_inn")]
        public string RegistrationHolderInn { get; set; }

        /// <summary>
        /// Комплектность
        /// </summary>
        [DataMember(Name = "completeness", IsRequired = false)]
        public string Сompleteness { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
        public string ProdFormName { get; set; }

        /// <summary>
        /// Производитель готовой ЛФ
        /// </summary>
        [DataMember(Name = "glf_name", IsRequired = false)]
        public string FormProducerName { get; set; }

        /// <summary>
        /// Страна регистрации производителя готовой ЛФ
        /// </summary>
        [DataMember(Name = "glf_country", IsRequired = false)]
        public string FormProducerCountry { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Версия внутреннего уникального идентификатора лекарственного препарата в реестре ЕСКЛП        /// 1 — устаревшие, 2 — актуальные данные        /// </summary>
        [DataMember(Name = "drug_code_version", IsRequired = false)]
        public int? DrugCodeVersion { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Список элементов:
    /// 5.14. Документов по отчёту из СУЗ.
    /// </summary>
    /// <typeparam name="T">Тип поля Items</typeparam>
    [DataContract]
    public class ItemsResponse<T>
    {
        [DataMember(Name = "items")]
        public T[] Items { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. Метод для поиска по реестру КИЗ записей, ожидающих
    /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// Статус последней проверки.
    /// </summary>
    [DataContract]
    public class LastCheckStatus
    {
        /// <summary>
        /// Время последней проверки
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Список нарушений при попытке обработки чека
        /// 1 — нарушение лицензионных требований
        /// 2 — повторный вывод из оборота
        /// 3 — отсутствуют сведения о вводе в оборот
        /// 4 — не подлежит розничной реализации
        /// 5 — нарушение формата чека
        /// 6 — нарушение порядка предоставления сведений
        /// 7 — нарушение правовладения
        /// 8 — истек срок годности
        /// 9 — отсутствие информации о рецепте
        /// </summary>
        [DataMember(Name = "violation_reasons")]
        public int[] ViolationReasons { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.32. Формат объекта PublicSGTIN
    /// 8.3.3. Метод поиска по общедоступному реестру КИЗ по списку значений
    /// </summary>
    [DataContract]
    public class PublicSgtin
    {
        /// <summary>
        /// SGTIN (КИЗ) 
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        [DataMember(Name = "expiration_date", IsRequired = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Название препарата.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
        public string ProdFormName { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name", IsRequired = false)]
        public string ProdDosageName { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        [DataMember(Name = "reg_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Держатель регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RafpRegistryResponse
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        [DataMember(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [DataMember(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [DataMember(Name = "KPP")]
        public string Kpp { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.2. Метод для регистрации пользователей (для резидентов страны)
    /// </summary>
    [DataContract]
    internal class RegisterResidentUserResponse
    {
        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.4. Метод для регистрация места ответственного хранения
    /// </summary>
    [DataContract]
    internal class RegisterWarehouseResponse
    {
        [DataMember(Name = "safe_warehouse_id")]
        public string WarehouseID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.5. Адрес для регистрации места ответственного хранения.
    /// </summary>
    [DataContract]
    public class RegistrationAddress
    {
        [DataMember(Name = "address_id")]
        public string AddressID { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        [DataMember(Name = "resolved_address")]
        public string ResolvedAddress { get; set; }

        [DataMember(Name = "license_type")]
        public string LicenseType { get; set; }

        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }
    }
}

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

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.1. Отправка документа
    /// </summary>
    [DataContract]
    internal class SendDocumentResponse
    {
        [DataMember(Name = "document_id")]
        public string DocumentID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.2. Отправка документа большого объема
    /// 5.3. Загрузка документа большого объема
    /// 5.4. Завершение отправки документа
    [DataContract]
    internal class SendLargeDocumentResponse
    {
        [DataMember(Name = "link", IsRequired = false)]
        public string Link { get; set; }

        [DataMember(Name = "document_id", IsRequired = false)]
        public string DocumentID { get; set; }

        [DataMember(Name = "request_id", IsRequired = false)]
        public string RequestID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.32. Формат объекта SGTIN
    /// </summary>
    [DataContract]
    public class Sgtin
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// ИНН владельца
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// SGTIN (КИЗ) 
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string SgtinValue { get; set; }

        /// <summary>
        /// Статус (см. Список возможных статусов КИЗ)
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Дата последней смены статуса
        /// </summary>
        [DataMember(Name = "status_date")]
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Наименование владельца
        /// </summary>
        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Тип эмиссии: 1 — собственное, 2 — контрактное, 3 — иностранное производство
        /// </summary>
        [DataMember(Name = "emission_type")]
        public int EmissionType { get; set; }

        /// <summary>
        /// Дата ввода в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Дата начала периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date")]
        public DateTime EmissionDate { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Местонахождение ЛП — название субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_name")]
        public string FederalSubjectName { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        [DataMember(Name = "expiration_date", IsRequired = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Название препарата.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// Полное наименование товара
        /// Например: лиофилизат для приготовления концентрата для приготовления раствора для инфузий "гертикад®" 150 мг, 440 мг
        /// </summary>
        [DataMember(Name = "full_prod_name", IsRequired = false)]
        public string FullProductName { get; set; }

        /// <summary>
        /// Держатель регистрационного удостоверения
        /// </summary>
        [DataMember(Name = "reg_holder", IsRequired = false)]
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// Полное наименование товара (что это за чертовщина?)
        /// </summary>
        [DataMember(Name = "pack1_desc", IsRequired = false)]
        public string Pack1Desc { get; set; }

        /// <summary>
        /// SSCC (Идентификатор третичной упаковки)
        /// </summary>
        [DataMember(Name = "pack3_id", IsRequired = false)]
        public string Sscc { get; set; }

        /// <summary>
        /// Дата выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date", IsRequired = false)]
        public DateTime? LastTracingDate { get; set; }

        /// <summary>
        /// Источник финансирования.
        /// Возможные значения см. в XSD описании базовых типов комплекта схем.
        /// </summary>
        [DataMember(Name = "source_type", IsRequired = false)]
        public int? SourceType { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор лекарственного препарата в реестре ЕСКЛП        /// </summary>
        [DataMember(Name = "drug_code", IsRequired = false)]
        public string DrugCode { get; set; }

        /// <summary>
        /// Лекарственная форма
        /// Например: ЛИОФИЛИЗАТ ДЛЯ ПРИГОТОВЛЕНИЯ КОНЦЕНТРАТА ДЛЯ ПРИГОТОВЛЕНИЯ РАСТВОРА ДЛЯ ИНФУЗИЙ
        /// </summary>
        [DataMember(Name = "prod_form_name", IsRequired = false)]
        public string ProdFormName { get; set; }

        /// <summary>
        /// Количество единиц измерения дозировки лекарственного препарата (строковое представление)
        /// Например: 150 мг
        /// </summary>
        [DataMember(Name = "prod_d_name", IsRequired = false)]
        public string ProdDosageName { get; set; }

        /// <summary>
        /// Идентификатор места нахождения товара в ЗТК (в формате SysID)
        /// </summary>
        [DataMember(Name = "customs_point_id", IsRequired = false)]
        public string CustomsPointID { get; set; }

        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// Информация о биллинге        /// </summary>
        [DataMember(Name = "billing_info", IsRequired = false)]
        public SgtinBillingInformation BillingInfo { get; set; }

        /// <summary>
        /// Состояние оплаты SGTIN
        /// 0 — успешно оплачен
        /// 1 — выбран для перемещения в очередь на оплату
        /// 2 — помещается в очередь на оплату
        /// 3 — помещен в очередь на оплату
        /// 4 — не оплачен в установленные сроки
        /// </summary>
        [DataMember(Name = "billing_state", IsRequired = false)]
        public int? BillingState { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug")]
        public bool VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp")]
        public bool Gnvlp { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. Фильтр для поиска по реестру КИЗ записей, ожидающих вывода из оборота по чеку от контрольно-кассовой техники (ККТ)
    /// 8.3.7. Фильтр для поиска по реестру КИЗ записей, ожидающих вывода из оборота через РВ
    /// </summary>
    [DataContract]
    public class SgtinAwaitingWithdrawalFilter
    {
        /// <summary>
        /// Идентификатор места деятельности отправителя
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = false)]
        public string Sgtin { get; set; }

        /// <summary>
        /// Дата операции из чека, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "op_start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата операции из чека, конец периода фильтрации
        /// </summary>
        [DataMember(Name = "op_end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.34. Формат объекта SgtinBillingInformation
    /// </summary>
    [DataContract]
    public class SgtinBillingInformation
    {
        /// <summary>
        /// Признак предоплаты
        /// </summary>
        [DataMember(Name = "is_prepaid")]
        public bool IsPrepaid { get; set; }

        /// <summary>
        /// Признак бесплатного кода
        /// </summary>
        [DataMember(Name = "free_code")]
        public bool FreeCode { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        [DataMember(Name = "is_paid")]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Признак вхождения в список высокозатратных нозологий        /// </summary>
        [DataMember(Name = "contains_vzn")]
        public bool ContainsVzn { get; set; }

        /// <summary>
        /// Список информации о платежах        /// </summary>
        [DataMember(Name = "payments")]
        public SgtinPaymentInformation[] Payments { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.7. КИЗ, ожидающий вывода из оборота по чеку от РВ.
    /// </summary>
    [DataContract]
    public class SgtinDeviceAwaitingWithdrawal
    {
        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Тип документа, по которому производится вывод через РВ.
        /// 10521 — Регистрация в ИС МДЛП сведений об отпуске лекарственного препарата по льготному рецепту (информация с СКЗКМ)
        /// 10531 — Регистрация в ИС МДЛП сведений о выдаче лекарственного препарата для оказания медицинской помощи (информация с СКЗКМ)
        /// </summary>
        [DataMember(Name = "xml_document_type")]
        public int XmlDocumentType { get; set; }

        /// <summary>
        /// Идентификатор организации-отправителя
        /// </summary>
        [DataMember(Name = "subject_id", IsRequired = false)]
        public string SubjectID { get; set; }

        /// <summary>
        /// Дата операции из чека
        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Номер льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_num", IsRequired = false)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_date", IsRequired = false)]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Серия льготного рецепта/документа, на основании которого осуществлена выдача
        /// </summary>
        [DataMember(Name = "doc_series", IsRequired = false)]
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Дата  фиксации КИЗа в очереди
        /// </summary>
        [DataMember(Name = "insertion_date", IsRequired = false)]
        public DateTime? InsertionDate { get; set; }

        /// <summary>
        /// Идентификатор XML-документа
        /// </summary>
        [DataMember(Name = "xml_document_id", IsRequired = false)]
        public string XmlDocumentID { get; set; }

        /// <summary>
        /// Доля от вторичной упаковки (доля вида 1/2)
        /// </summary>
        [DataMember(Name = "sold_part", IsRequired = false)]
        public string SoldPart { get; set; }

        /// <summary>
        /// Уникальный идентификатор РЭ или РВ
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Уникальный идентификатор системы, сформировавшей сообщение
        /// </summary>
        [DataMember(Name = "skzkm_origin_msg_id", IsRequired = false)]
        public string SkskmOriginMessageID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.33. Формат объекта SgtinExtended
    /// </summary>
    [DataContract]
    public class SgtinExtended : Sgtin
    {
        // <summary>
        // Идентификатор заказа системы управления заказами (СУЗ), Guid        // Он и так есть в классе Sgtin.        // </summary>
        // [DataMember(Name = "oms_order_id", IsRequired = false)]
        // public string OmsOrderID { get; set; }

        /// <summary>
        /// ИНН/ИТИН производителя-упаковщика        /// </summary>
        [DataMember(Name = "packing_inn", IsRequired = false)]
        public string PackingInn { get; set; }

        /// <summary>
        /// Наименование производителя-упаковщика        /// </summary>
        [DataMember(Name = "packing_name", IsRequired = false)]
        public string PackingName { get; set; }

        /// <summary>
        /// ИНН/ИТИН производителя-выпускающего        /// </summary>
        [DataMember(Name = "control_inn", IsRequired = false)]
        public string ControlInn { get; set; }

        /// <summary>
        /// Наименование производителя-выпускающего        /// </summary>
        [DataMember(Name = "control_name", IsRequired = false)]
        public string ControlName { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.2. Ошибка поиска КИЗ
    /// </summary>
    [DataContract]
    public class SgtinFailed
    {
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Код ошибки: 2 — не найден, 4 — доступ запрещен
        /// </summary>
        [DataMember(Name = "error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Текстовое описание ошибки
        /// </summary>
        [DataMember(Name = "error_desc")]
        public string ErrorDescription { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.1. Метод для поиска по реестру КИЗ. Структура данных SgtinFilter    /// </summary>
    [DataContract]
    public class SgtinFilter
    {
        /// <summary>
        /// Статус
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public string[] Status { get; set; }

        /// <summary>
        /// Тип эмиссии: 1 — собственное, 2 — контрактное, 3 — иностранное производство
        /// </summary>
        [DataMember(Name = "emission_type", IsRequired = false)]
        public int[] EmissionType { get; set; }

        /// <summary>
        /// Название препарата.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = false)]
        public string Sgtin { get; set; }

        /// <summary>
        /// SSCC (Идентификатор третичной упаковки)
        /// </summary>
        [DataMember(Name = "pack3_id", IsRequired = false)]
        public string Sscc { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»        /// </summary>
        [DataMember(Name = "sys_id", IsRequired = false)]
        public string SystemID { get; set; } // "0c290e4a-aabb-40ae-8ef2-ce462561ce7f",

        /// <summary>
        /// Дата упаковки, начала временного диапазона — дата ввода в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_from", IsRequired = false)]
        public DateTime? ReleaseDateFrom { get; set; }

        /// <summary>
        /// Дата упаковки, конец временного диапазона — дата окончания в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_to", IsRequired = false)]
        public DateTime? ReleaseDateTo { get; set; }

        /// <summary>
        /// Дата начала периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_from", IsRequired = false)]
        public DateTime? EmissionDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_to", IsRequired = false)]
        public DateTime? EmissionDateTo { get; set; }

        /// <summary>
        /// Дата начала периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_from", IsRequired = false)]
        public DateTime? LastTracingDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_to", IsRequired = false)]
        public DateTime? LastTracingDateTo { get; set; }

        /// <summary>
        /// Источник финансирования.
        /// Возможные значения см. в XSD описании базовых типов комплекта схем.
        /// </summary>
        [DataMember(Name = "source_type", IsRequired = false)]
        public int[] SourceType { get; set; }

        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// Информация о биллинге        /// </summary>
        [DataMember(Name = "billing_info", IsRequired = false)]
        public SgtinBillingInformation BillingInfo { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug", IsRequired = false)]
        public bool? VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp", IsRequired = false)]
        public bool? Gnvlp { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. КИЗ, ожидающий вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// </summary>
    [DataContract]
    public class SgtinKktAwaitingWithdrawal
    {
        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Тип реализации
        /// 0 — розничная продажа
        /// 1 — отпуск по льготному рецепту
        /// </summary>
        [DataMember(Name = "sold_type")]
        public int SoldType { get; set; }

        /// <summary>
        /// Статус обработки
        /// 0 — принято
        /// 1 — в обработке
        /// 2 — завершено
        /// 3 — завершено с ошибкой
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Дата операции из чека
        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// ИНН из чека
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// Статус последней проверки
        /// </summary>
        [DataMember(Name = "last_check_status", IsRequired = false)]
        public LastCheckStatus LastCheckStatus { get; set; }

        /// <summary>
        /// Розничная цена, в коп. (обязательно при SoldType = 0)
        /// </summary>
        [DataMember(Name = "price", IsRequired = false)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Сумма НДС (если сделка облагается НДС), в коп.        /// </summary>
        [DataMember(Name = "vat_value", IsRequired = false)]
        public decimal? VatValue { get; set; }

        /// <summary>
        /// Доля от вторичной упаковки (доля вида 1/2)
        /// </summary>
        [DataMember(Name = "sold_part", IsRequired = false)]
        public string SoldPart { get; set; }

        /// <summary>
        /// Сумма скидки, в коп.        /// </summary>
        [DataMember(Name = "discount", IsRequired = false)]
        public decimal? Discount { get; set; }

        /// <summary>
        /// Номер льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_num", IsRequired = false)]
        public string PrescriptionNumber { get; set; }

        /// <summary>
        /// Дата льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_num", IsRequired = false)]
        public DateTime? PrescriptionDate { get; set; }

        /// <summary>
        /// Серия льготного рецепта
        /// </summary>
        [DataMember(Name = "prescription_series", IsRequired = false)]
        public string PrescriptionSeries { get; set; }

        /// <summary>
        /// Уникальный идентификатор РЭ или РВ
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Уникальный идентификатор системы, сформировавшей сообщение
        /// </summary>
        [DataMember(Name = "skzkm_origin_msg_id", IsRequired = false)]
        public string SkskmOriginMessageID { get; set; }

        /// <summary>
        /// Идентификатор организации-отправителя
        /// </summary>
        [DataMember(Name = "subject_id", IsRequired = false)]
        public string SubjectID { get; set; }

        /// <summary>
        /// Идентификатор XML-документа
        /// </summary>
        [DataMember(Name = "xml_document_id", IsRequired = false)]
        public string XmlDocumentID { get; set; }

        /// <summary>
        /// Дата фактического получения чека в системе
        /// </summary>
        [DataMember(Name = "op_exec_date")]
        public DateTime OperationExecutionDate { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.5. Метод для поиска по реестру КИЗ всех записей со статусом 'Оборот приостановлен'
    /// </summary>
    [DataContract]
    public class SgtinOnHoldFilter
    {
        /// <summary>
        /// ИНН владельца
        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Тип эмиссии: 1 — собственное, 2 — контрактное, 3 — иностранное производство
        /// </summary>
        [DataMember(Name = "emission_type", IsRequired = false)]
        public int[] EmissionType { get; set; }

        /// <summary>
        /// Название препарата.
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        [DataMember(Name = "sell_name", IsRequired = false)]
        public string SellingName { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = false)]
        public string Sgtin { get; set; }

        /// <summary>
        /// SSCC (Идентификатор третичной упаковки)
        /// </summary>
        [DataMember(Name = "pack3_id", IsRequired = false)]
        public string Sscc { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»        /// </summary>
        [DataMember(Name = "sys_id", IsRequired = false)]
        public string SystemID { get; set; }

        /// <summary>
        /// Дата упаковки, начала временного диапазона — дата ввода в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_from", IsRequired = false)]
        public DateTime? ReleaseDateFrom { get; set; }

        /// <summary>
        /// Дата упаковки, конец временного диапазона — дата окончания в гражданский оборот
        /// </summary>
        [DataMember(Name = "release_date_to", IsRequired = false)]
        public DateTime? ReleaseDateTo { get; set; }

        /// <summary>
        /// Дата начала периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_from", IsRequired = false)]
        public DateTime? EmissionDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода регистрации
        /// </summary>
        [DataMember(Name = "emission_operation_date_to", IsRequired = false)]
        public DateTime? EmissionDateTo { get; set; }

        /// <summary>
        /// Дата начала периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_from", IsRequired = false)]
        public DateTime? LastTracingDateFrom { get; set; }

        /// <summary>
        /// Дата окончания периода выполнения последней операции
        /// </summary>
        [DataMember(Name = "last_tracing_op_date_to", IsRequired = false)]
        public DateTime? LastTracingDateTo { get; set; }

        /// <summary>
        /// Идентификатор заказа системы управления заказами (СУЗ), Guid        /// </summary>
        [DataMember(Name = "oms_order_id", IsRequired = false)]
        public string OmsOrderID { get; set; }

        /// <summary>
        /// Информация о биллинге        /// </summary>
        [DataMember(Name = "billing_info", IsRequired = false)]
        public SgtinBillingInformation BillingInfo { get; set; }

        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug", IsRequired = false)]
        public bool? VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp", IsRequired = false)]
        public bool? Gnvlp { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.35. Формат объекта SgtinPaymentInformation
    /// </summary>
    [DataContract]
    public class SgtinPaymentInformation
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        [DataMember(Name = "created_date", IsRequired = false)]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Дата оплаты платежа
        /// </summary>
        [DataMember(Name = "payment_date", IsRequired = false)]
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Тариф оплаты
        /// </summary>
        [DataMember(Name = "tariff", IsRequired = false)]
        public decimal? Tariff { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.1. Информация об иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class SsccInfo
    {
        /// <summary>
        /// Идентификационный код третичной упаковки
        /// </summary>
        [DataMember(Name = "sscc")]
        public string Sscc { get; set; }

        /// <summary>
        /// Дата и время совершения операции упаковки
        /// </summary>
        [DataMember(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения, осуществившего операцию упаковки
        /// </summary>
        /// <remarks>
        /// Идентификационный код SysID или BranchID
        /// </remarks>
        [DataMember(Name = "system_subj_id")]
        public string SystemSubjectID { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.2. Фильтр для получения информации о КИЗ, вложенных в третичную упаковку
    /// </summary>
    [DataContract]
    public class SsccSgtinsFilter
    {
        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug", IsRequired = false)]
        public bool? VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp", IsRequired = false)]
        public bool? Gnvlp { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.2. Место ответственного хранения
    /// </summary>
    [DataContract]
    public class WarehouseEntry
    {
        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Код субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_code")]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// Название субъекта РФ
        /// </summary>
        [DataMember(Name = "federal_subject_name")]
        public string FederalSubjectName { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrgName { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// Перечень работ/услуг согласно лицензии
        /// </summary>
        [DataMember(Name = "work_list")]
        public string[] WorkList { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности
        /// </summary>
        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Название владельца лицензии
        /// </summary>
        [DataMember(Name = "warehouse_org_name")]
        public string WarehouseOrgName { get; set; }

        /// <summary>
        /// ИНН владельца лицензии
        /// </summary>
        [DataMember(Name = "warehouse_org_inn")]
        public string WarehouseOrgInn { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [DataMember(Name = "registration_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата приостановления
        /// </summary>
        [DataMember(Name = "suspension_date", IsRequired = false)]
        public DateTime? SuspensionDate { get; set; }
    }
}

namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.2. Метод для поиска информации о местах ответственного хранения по фильтру
    /// </summary>
    [DataContract]
    public class WarehouseFilter
    {
        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "warehouse_id", IsRequired = false)]
        public string WarehouseID { get; set; }

        /// <summary>
        /// Уникальный идентификатор дома
        /// </summary>
        [DataMember(Name = "houseguid", IsRequired = false)]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата начала периода фильтрации по дате регистрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации по дате регистрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}

namespace MdlpApiClient
{
    using DataContracts;
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using MdlpApiClient.Toolbox;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 5: documents.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 5.1. Отправка документа
        /// </summary>
        /// <param name="xmlDocument">Документ в формате XML.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendDocument(string xmlDocument)
        {
            var result = Post<SendDocumentResponse>("documents/send", new
            {
                document = Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlDocument)),
                sign = ComputeSignature(xmlDocument),
                request_id = Guid.NewGuid().ToString(),
            });

            return result.DocumentID;
        }

        /// <summary>
        /// 5.2. Отправка документа большого объема
        /// 5.3. Загрузка документа большого объема
        /// 5.4. Завершение отправки документа
        /// </summary>
        /// <param name="xmlDocument">Документ в формате XML.</param>
        /// <returns>Идентификатор документа</returns>
        public string SendLargeDocument(string xmlDocument)
        {
            // 5.2
            var link = Post<SendLargeDocumentResponse>("documents/send_large", new
            {
                sign = ComputeSignature(xmlDocument),
                hash_sum = xmlDocument.ComputeHash<SHA256>(),
                request_id = Guid.NewGuid().ToString(),
            });

            // 5.3
            Put(link.Link, xmlDocument);

            // 5.4
            var result = Post<SendLargeDocumentResponse>("documents/send_finished", new
            {
                document_id = link.DocumentID
            });

            return link.DocumentID;
        }

        /// <summary>
        /// 5.5. Получение информации об ограничении размера небольших документов
        /// </summary>
        /// <returns>Максимальный размер документа в байтах.</returns>
        public int GetLargeDocumentSize()
        {
            var result = Get<GetLargeDocumentSizeResponse>("documents/doc_size");
            return result.DocSize;
        }

        /// <summary>
        /// 5.6. Отмена отправки документа
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="requestId"></param>
        public void CancelSendDocument(string docId, string requestId)
        {
            Post("documents/cancel", new
            {
                document_id = docId,
                request_id = requestId,
            });
        }

        /// <summary>
        /// 5.7. Получение списка исходящих документов
        /// </summary>
        /// <param name="filter">Фильтр <see cref="DocFilter"/> списка документов.</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public DocumentsResponse<DocumentMetadata> GetOutcomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<DocumentsResponse<DocumentMetadata>>("documents/outcome", new
            {
                filter = filter,
                start_from = startFrom,
                count = count
            });
        }

        /// <summary>
        /// 5.8. Получение списка входящих документов
        /// </summary>
        /// <param name="filter">Фильтр <see cref="DocFilter"/> списка документов.</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public DocumentsResponse<DocumentMetadata> GetIncomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<DocumentsResponse<DocumentMetadata>>("documents/income", new
            {
                filter = filter,
                start_from = startFrom,
                count = count
            });
        }

        /// <summary>
        /// 5.9. Получение метаданных документа
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Метаданные документа</returns>
        public DocumentMetadata GetDocumentMetadata(string documentId)
        {
            return Get<DocumentMetadata>("documents/{document_id}", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 5.10. Получение документа по идентификатору
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        public string GetDocument(string documentId)
        {
            var docLink = Get<GetDocumentResponse>("/documents/download/{document_id}", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });

            return Get(docLink.Link);
        }

        /// <summary>
        /// 5.11. Получение списка документов по идентификатору запроса
        /// </summary>
        /// <param name="requestId">Идентификатор запроса</param>
        public DocumentsResponse<DocumentMetadata> GetDocumentsByRequestID(string requestId)
        {
            return Get<DocumentsResponse<DocumentMetadata>>("documents/request/{request_id}", new[]
            {
                new Parameter("request_id", requestId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 5.12. Получение квитанции по номеру исходящего документа
        /// </summary>
        /// <param name="requestId">Идентификатор документа</param>
        public string GetTicket(string documentId)
        {
            var link = Get<GetDocumentResponse>("documents/{document_id}/ticket", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
            });

            return Get(link.Link);
        }

        /// <summary>
        /// 5.13. Получение электронной подписи исходящего документа
        /// </summary>
        /// <param name="requestId">Идентификатор документа</param>
        public string GetSignature(string documentId)
        {
            return Get("documents/{document_id}/signature", new[]
            {
                new Parameter("document_id", documentId, ParameterType.UrlSegment),
                new Parameter("Accept", "text/plain", ParameterType.HttpHeader),
            });
        }

        /// <summary>
        /// 5.14. Прослеживание документов по отчёту из СУЗ
        /// </summary>
        /// <param name="reportId">Идентификатор отчета СУЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        public ItemsResponse<DocumentSkzkmMetadata> GetDocumentsBySkzkmReportID(string reportId, int startFrom, int count)
        {
            return Post<ItemsResponse<DocumentSkzkmMetadata>>("documents/skzkm-traces/filter", new
            {
                filter = new
                {
                    skzkm_report_id = reportId,
                },
                start_from = startFrom,
                count = count,
            });
        }
    }
}

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

namespace MdlpApiClient
{
    using DataContracts;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 7: registries.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 7.1.1. Получение данных записи ЕГРЮЛ
        /// </summary>
        /// <returns>Данные из реестра ЕГРЮЛ</returns>
        public EgrulRegistryResponse GetEgrulRegistryEntry()
        {
            return Get<EgrulRegistryResponse>("reestr/egrul");
        }

        /// <summary>
        /// 7.2.1. Получение данных записи ЕГРИП
        /// </summary>
        /// <returns>Данные из реестра ЕГРИП</returns>
        public EgripRegistryResponse GetEgripRegistryEntry()
        {
            return Get<EgripRegistryResponse>("reestr/egrip");
        }

        /// <summary>
        /// 7.3.1. Получение записи реестра РАФП (реестра аккредитованных филиалов и представительств)
        /// </summary>
        /// <returns>Данные из реестра РАФП</returns>
        public RafpRegistryResponse GetRafpRegistryEntry()
        {
            return Get<RafpRegistryResponse>("reestr/rafp");
        }
    }
}

namespace MdlpApiClient
{
    using DataContracts;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 8: MDLP information.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 8.1.2. Метод для поиска информации о местах осуществления деятельности по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест осуществления деятельности</returns>
        public EntriesResponse<BranchEntry> GetBranches(BranchFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<BranchEntry>>("reestr/branches/filter", new
            {
                filter = filter ?? new BranchFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.1.3. Получение информации о конкретном месте осуществления деятельности
        /// </summary>
        public GetBranchResponse GetBranch(string branchId)
        {
            return Get<GetBranchResponse>("reestr/branches/{branch_id}", new[]
            {
                new Parameter("branch_id", branchId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 8.1.4. Метод для регистрация места осуществления деятельности
        /// </summary>
        public string RegisterBranch(Address address)
        {
            var branch = Post<GetBranchResponse>("reestr/branches/register", new
            {
                branch_address = address
            });

            return branch.BranchID;
        }

        /// <summary>
        /// 8.2.2. Метод для поиска информации о местах ответственного хранения по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест ответственного хранения</returns>
        public EntriesResponse<WarehouseEntry> GetWarehouses(WarehouseFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<WarehouseEntry>>("reestr/warehouses/filter", new
            {
                filter = filter ?? new WarehouseFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.2.3. Получение информации о конкретном месте ответственного хранения
        /// </summary>
        public GetWarehouseResponse GetWarehouses(string warehouseId)
        {
            return Get<GetWarehouseResponse>("reestr/warehouses/{warehouse_id}", new[]
            {
                new Parameter("warehouse_id", warehouseId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 8.2.4. Метод для регистрация места ответственного хранения
        /// </summary>
        public string RegisterWarehouse(string warehouseOrgInn, Address address)
        {
            var warehouse = Post<RegisterWarehouseResponse>("reestr/warehouses/register", new
            {
                warehouse_org_inn = warehouseOrgInn,
                warehouse_address = address
            });

            return warehouse.WarehouseID;
        }

        /// <summary>
        /// 8.2.5. Метод получения информации об адресах искомого участника,
        /// для регистрации мест ответственного хранения или отправки документов.
        /// </summary>
        /// <param name="inn">ИНН (необязательно)</param>
        /// <param name="licenseNumber">Номер лицензии (необязательно)</param>
        public GetAvailableAddressesResponse GetAvailableAddresses(string inn = null, string licenseNumber = null)
        {
            return Post<GetAvailableAddressesResponse>("reestr/warehouses/available_safe_warehouses_addresses", new
            {
                inn = inn,
                licence_number = licenseNumber,
            });
        }

        /// <summary>
        /// 8.3.1. Метод для поиска по реестру КИЗ
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinExtended> GetSgtins(SgtinFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinExtended>>("reestr/sgtin/filter", new
            {
                filter = filter ?? new SgtinFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.2. Метод поиска по реестру КИЗ по списку значений
        /// </summary>
        /// <param name="filters">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<SgtinExtended, SgtinFailed> GetSgtins(string[] sgtins)
        {
            return Post<EntriesFailedResponse<SgtinExtended, SgtinFailed>>("reestr/sgtin/sgtins-by-list", new
            {
                filter = new
                {
                    sgtins = sgtins
                },
            });
        }

        /// <summary>
        /// 8.3.3. Метод поиска по общедоступному реестру КИЗ по списку значений
        /// </summary>
        /// <param name="filters">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<PublicSgtin, string> GetPublicSgtins(string[] sgtins)
        {
            return Post<EntriesFailedResponse<PublicSgtin, string>>("reestr/sgtin/public/sgtins-by-list", new
            {
                filter = new
                {
                    sgtins = sgtins
                },
            });
        }

        /// <summary>
        /// 8.3.4. Метод для получения детальной информации о КИЗ и связанным с ним ЛП
        /// </summary>
        /// <param name="sgtin">КИЗ для поиска</param>
        /// <returns>Подробная информация КИЗ и ЛП</returns>
        public GetSgtinResponse GetSgtin(string sgtin)
        {
            return Get<GetSgtinResponse>("reestr/sgtin/{sgtin}", new[]
            {
                new Parameter("sgtin", sgtin, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.3.5. Метод для поиска по реестру КИЗ всех записей со статусом 'Оборот приостановлен'
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinExtended> GetSgtinsOnHold(SgtinOnHoldFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinExtended>>("reestr/sgtin/on_hold", new
            {
                filter = filter ?? new SgtinOnHoldFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.6. Метод для поиска по реестру КИЗ записей, ожидающих
        /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ)
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinKktAwaitingWithdrawal> GetSgtinsKktAwaitingWithdrawal(SgtinAwaitingWithdrawalFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinKktAwaitingWithdrawal>>("reestr/sgtin/kkt/awaiting-withdrawal/filter", new
            {
                filter = filter ?? new SgtinAwaitingWithdrawalFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.7. Метод для поиска по реестру КИЗ записей,
        /// ожидающих вывода из оборота через РВ
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinDeviceAwaitingWithdrawal> GetSgtinsDeviceAwaitingWithdrawal(SgtinAwaitingWithdrawalFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinDeviceAwaitingWithdrawal>>("reestr/sgtin/device/awaiting-withdrawal/filter", new
            {
                filter = filter ?? new SgtinAwaitingWithdrawalFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.4.1. Метод для получения информации об иерархии вложенности третичной упаковки
        /// </summary>
        /// <param name="sscc">Идентификационный код третичной упаковки</param>
        /// <returns>Подробная информация КИЗ и ЛП</returns>
        public GetSsccHierarchyResponse GetSsccHierarchy(string sscc)
        {
            return Get<GetSsccHierarchyResponse>("reestr/sscc/{sscc}/hierarchy", new[]
            {
                new Parameter("sscc", sscc, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.4.2. Метод для получения информации о КИЗ, вложенных в третичную упаковку
        /// </summary>
        /// <param name="sscc">Идентификационный код третичной упаковки</param>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ, непосредственно вложенных в указанную третичную упаковку</returns>
        public GetSsccSgtinsResponse GetSsccSgtins(string sscc, SsccSgtinsFilter filter, int startFrom, int count)
        {
            return Post<GetSsccSgtinsResponse>("reestr/sscc/{sscc}/sgtins", new
            {
                sscc = sscc,
                filter = filter ?? new SsccSgtinsFilter(),
                start_from = startFrom,
                count = count,
            },
            new[]
            {
                new Parameter("sscc", sscc, ParameterType.UrlSegment),
            });
        }
    }
}

namespace MdlpApiClient
{
    using System.Text;
    using RestSharp;
    using System.Security.Cryptography.X509Certificates;
    using MdlpApiClient.Toolbox;
    using System.Runtime.CompilerServices;
    using MdlpApiClient.Serialization;
    using System.Xml;
    using RestSharp.Serialization;
    using System.Linq;
    using MdlpApiClient.DataContracts;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public partial class MdlpClient
    {
        public const string StageApiHttp = "http://api.stage.mdlp.crpt.ru/api/v1/";
        public const string StageApiHttps = "https://api.stage.mdlp.crpt.ru/api/v1/";

        /// <summary>
        /// Initializes a new instance of the MDLP REST API client.
        /// </summary>
        /// <param name="credentials">Credentials used for authentication.</param>
        /// <param name="baseUrl">Base URL of the API endpoint.</param>
        public MdlpClient(CredentialsBase credentials, string baseUrl = StageApiHttp)
        {
            // make sure BaseUrl ends with a slash
            BaseUrl = baseUrl ?? string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                BaseUrl += "/";
            }

            Credentials = credentials;
            Client = new RestClient(BaseUrl)
            {
                Authenticator = new CredentialsAuthenticator(this, credentials),
                Encoding = Encoding.UTF8,
                ThrowOnDeserializationError = false
            };

            Serializer = new ServiceStackSerializer();
            Client.UseSerializer(() => Serializer);
        }

        public string BaseUrl { get; private set; }

        private IRestSerializer Serializer { get; set; }

        public IRestClient Client { get; private set; }

        private CredentialsBase Credentials { get; set; }

        private X509Certificate2 userCertificate;

        /// <summary>
        /// X.509 certificate of the resident user (if applicable).
        /// </summary>
        internal X509Certificate2 UserCertificate
        {
            set { userCertificate = value; }
            get
            {
                if (userCertificate == null)
                {
                    userCertificate = GostCryptoHelpers.FindCertificate(Credentials.UserID);
                }

                return userCertificate;
            }
        }

        /// <summary>
        /// Computes the detached digital signature of the given text.
        /// </summary>
        /// <param name="textToSign">Text to sign.</param>
        /// <returns>Detached signature in CMS format and base64 encoding.</returns>
        private string ComputeSignature(string textToSign)
        {
            if (UserCertificate == null)
            {
                return null;
            }

            return GostCryptoHelpers.ComputeDetachedSignature(UserCertificate, textToSign);
        }

        private void PrepareRequest(IRestRequest request, string apiMethodName)
        {
            // use request parameters to store additional properties, not really used by the requests
            request.AddParameter(ApiTimestampParameterName, DateTime.Now.Ticks, ParameterType.UrlSegment);
            request.AddParameter(ApiStopwatchParameterName, Stopwatch.StartNew(), ParameterType.UrlSegment);
            if (!string.IsNullOrWhiteSpace(apiMethodName))
            {
                request.AddHeader(ApiMethodNameHeaderName, apiMethodName);
            }

            // trace requests and responses
            if (Tracer != null)
            {
                request.OnBeforeRequest = http => Trace(http, request);
                request.OnBeforeDeserialization = resp => Trace(resp);
            }
        }

        private void ThrowOnFailure(IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                // already traced
                //Trace(response);

                // try to find the non-empty error message
                var errorMessage = response.ErrorMessage;
                var contentMessage = response.Content;
                var errorResponse = default(ErrorResponse);
                if (response.ContentType != null)
                {
                    // Text/plain;charset=UTF-8 => text/plain
                    var contentType = response.ContentType.ToLower().Trim();
                    var semicolonIndex = contentType.IndexOf(';');
                    if (semicolonIndex >= 0)
                    {
                        contentType = contentType.Substring(0, semicolonIndex).Trim();
                    }

                    // Try to deserialize error response DTO
                    if (Serializer.SupportedContentTypes.Contains(contentType))
                    {
                        errorResponse = Serializer.Deserialize<ErrorResponse>(response);
                        contentMessage = string.Join(". ", new[]
                        {
                            errorResponse.Error,
                            errorResponse.Message,
                            errorResponse.Description,
                        }
                        .Distinct()
                        .Where(m => !string.IsNullOrWhiteSpace(m)));
                    }
                    else if (response.ContentType.ToLower().Contains("html"))
                    {
                        // Try to parse HTML
                        contentMessage = HtmlHelper.ExtractText(response.Content);
                    }
                    else
                    {
                        // Return as is assuming text/plain content
                        contentMessage = response.Content;
                    }
                }

                // HTML->XML deserialization errors are meaningless
                if (response.ErrorException is XmlException && errorMessage == response.ErrorException.Message)
                {
                    errorMessage = contentMessage;
                }

                // empty error message is meaningless
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = contentMessage;
                }

                // finally, throw it
                throw new MdlpException(response.StatusCode, errorMessage, errorResponse, response.ErrorException);
            }
        }

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="request">The request to execute.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        internal T Execute<T>(IRestRequest request, string apiMethodName)
            where T : class, new()
        {
            PrepareRequest(request, apiMethodName);
            var response = Client.Execute<T>(request);
            ThrowOnFailure(response);
            return response.Data;
        }

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <param name="request">The request to execute.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        internal void Execute(IRestRequest request, string apiMethodName)
        {
            PrepareRequest(request, apiMethodName);
            var response = Client.Execute(request);

            // there is no body deserialization step, so we need to trace
            Trace(response);
            ThrowOnFailure(response);
        }

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <param name="request">The request to execute.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        internal string ExecuteString(IRestRequest request, string apiMethodName)
        {
            PrepareRequest(request, apiMethodName);
            var response = Client.Execute(request);

            // there is no body deserialization step, so we need to trace
            Trace(response);
            ThrowOnFailure(response);
            return response.Content;
        }

        /// <summary>
        /// Performs GET request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="parameters">IRestRequest parameters.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public T Get<T>(string url, Parameter[] parameters = null, [CallerMemberName] string apiMethodName = null)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            if (!parameters.IsNullOrEmpty())
            {
                request.AddOrUpdateParameters(parameters);
            }

            return Execute<T>(request, apiMethodName);
        }

        /// <summary>
        /// Performs GET request and returns a string.
        /// </summary>
        /// <param name="url">Resource url.</param>
        /// <param name="parameters">IRestRequest parameters.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public string Get(string url, Parameter[] parameters = null, [CallerMemberName] string apiMethodName = null)
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            if (!parameters.IsNullOrEmpty())
            {
                request.AddOrUpdateParameters(parameters);
            }

            //if (!string.IsNullOrWhiteSpace(accept))
            //{
            //    request.AddOrUpdateParameter("Accept", accept, ParameterType.HttpHeader);
            //}

            return ExecuteString(request, apiMethodName);
        }

        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="body">Request body, to be serialized as JSON.</param>
        /// <param name="parameters">IRestRequest parameters.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public T Post<T>(string url, object body, Parameter[] parameters = null, [CallerMemberName] string apiMethodName = null)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            if (!parameters.IsNullOrEmpty())
            {
                request.AddOrUpdateParameters(parameters);
            }

            return Execute<T>(request, apiMethodName);
        }

        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="body">Request body, to be serialized as JSON.</param>
        /// <param name="parameters">IRestRequest parameters.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public void Post(string url, object body, Parameter[] parameters = null, [CallerMemberName] string apiMethodName = null)
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            if (!parameters.IsNullOrEmpty())
            {
                request.AddOrUpdateParameters(parameters);
            }

            Execute(request, apiMethodName);
        }

        /// <summary>
        /// Performs PUT request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="body">Request body, serialized as string.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public void Put(string url, string body, [CallerMemberName] string apiMethodName = null)
        {
            var request = new RestRequest(url, Method.PUT, DataFormat.None);
            request.AddParameter(string.Empty, body, ParameterType.RequestBody);
            Execute(request, apiMethodName);
        }
    }
}

namespace MdlpApiClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RestSharp;
    using RestSharp.Serialization.Json;
    using MdlpApiClient.Toolbox;
    using System.Diagnostics;

    partial class MdlpClient
    {
        private const string ApiMethodNameHeaderName = "X-ApiMethodName";
        private const string ApiTimestampParameterName = "X-ApiTimestamp";
        private const string ApiStopwatchParameterName = "X-ApiStopwatch";

        public Action<string, object[]> Tracer { get; set; }

        private void Trace(string format, params object[] arguments)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                tracer(format, arguments);
            }
        }

        private JsonSerializer Json = new JsonSerializer();

        private static string CR = Environment.NewLine;

        public static string FormatHeaders(IEnumerable<Tuple<string, object>> headers)
        {
            if (headers == null || !headers.Any())
            {
                return "headers: none" + CR;
            }

            return "headers: {" + CR +
                string.Join(CR, headers.Select(h => "  " + h.Item1 + " = " + h.Item2)) +
            CR + "}" + CR;
        }

        public static string FormatBody(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            return "body: " + JsonFormatter.FormatJson(content) + CR;
        }

        public static string FormatBody(RequestBody body)
        {
            if (IsEmpty(body))
            {
                return string.Empty;
            }

            var bodyValue = body.Value + string.Empty;
            if (body.ContentType != null && body.ContentType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                bodyValue = JsonFormatter.FormatJson(bodyValue);
            }

            return "body: " + bodyValue + CR;
        }

        private static bool IsEmpty(RequestBody body)
        {
            return (body == null || body.Value == null || string.Empty.Equals(body.Value));
        }

        private IEnumerable<Tuple<string, object>> GetHeaders(IRestRequest request)
        {
            var headers =
                from p in request.Parameters
                where p.Type == ParameterType.HttpHeader
                select Tuple.Create(p.Name, p.Value);

            if (request.Body != null && !string.IsNullOrWhiteSpace(request.Body.ContentType))
            {
                headers = headers.Concat(new[]
                {
                    Tuple.Create("Content-type", request.Body.ContentType as object)
                });
            }

            return headers;
        }

        private IEnumerable<Tuple<string, object>> GetHeaders(IHttp http)
        {
            var headers =
                from p in http.Headers
                select Tuple.Create(p.Name, p.Value as object);

            if (!string.IsNullOrWhiteSpace(http.RequestContentType))
            {
                headers = headers.Concat(new[]
                {
                    Tuple.Create("Content-type", http.RequestContentType as object)
                });
            }

            return headers;
        }

        private void Trace(IRestRequest request)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                var method = request.Method.ToString();
                var uri = Client.BuildUri(request);
                var body = FormatBody(request.Body);
                var headers = FormatHeaders(GetHeaders(request));

                tracer("-> {0} {1}{2}{3}{4}", new object[]
                {
                    method, uri, CR,
                    headers,
                    body,
                });
            }
        }

        private void Trace(IHttp http, IRestRequest request)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                // trace API method name
                var apiMethod = http.Headers.FirstOrDefault(h => StringComparer.OrdinalIgnoreCase.Equals(h.Name, ApiMethodNameHeaderName));
                if (apiMethod != null && !string.IsNullOrWhiteSpace(apiMethod.Value))
                {
                    tracer("// {0}", new[] { apiMethod.Value });
                }

                // trace HTTP request internals
                var method = request.Method.ToString();
                var uri = http.Url;
                var body = FormatBody(request.Body);
                var headers = FormatHeaders(GetHeaders(http));

                tracer("-> {0} {1}{2}{3}{4}", new object[]
                {
                    method, uri, CR,
                    headers,
                    body,
                });
            }
        }

        public static string FormatTimings(DateTime? startTime, Stopwatch stopwatch)
        {
            if (startTime == null && stopwatch == null)
            {
                return string.Empty;
            }

            var items = new List<string>()
            {
                "timings: {"
            };

            if (startTime.HasValue)
            {
                items.Add("  started: " + startTime.Value.ToString("s").Replace("T", " ").Replace("00:00:00", "").Trim());
            }

            if (stopwatch != null)
            {
                stopwatch.Stop();
                items.Add("  elapsed: " + stopwatch.Elapsed);
            }

            items.Add("}");
            return string.Join(CR, items) + CR;
        }

        private static string FormatTimings(IRestResponse response)
        {
            // extract timings from request parameters
            var timings = string.Empty;
            var startTime = default(DateTime?);
            var timestampParameter = response.Request.Parameters.FirstOrDefault(h => StringComparer.OrdinalIgnoreCase.Equals(h.Name, ApiTimestampParameterName));
            if (timestampParameter != null && timestampParameter.Value != null)
            {
                var timestampTicks = Convert.ToInt64(timestampParameter.Value);
                startTime = new DateTime(timestampTicks);
            }

            var stopwatch = default(Stopwatch);
            var stopwatchParameter = response.Request.Parameters.FirstOrDefault(h => StringComparer.OrdinalIgnoreCase.Equals(h.Name, ApiStopwatchParameterName));
            if (stopwatchParameter != null && stopwatchParameter.Value is Stopwatch)
            {
                stopwatch = stopwatchParameter.Value as Stopwatch;
                if (stopwatch != null)
                {
                    stopwatch.Stop();
                }
            }

            // trace timestamp and duration
            return FormatTimings(startTime, stopwatch);
        }

        private void Trace(IRestResponse response)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                // trace the response
                var result = response.IsSuccessful ? "OK" : "ERROR";
                var timings = FormatTimings(response);
                var headerList = response.Headers.Select(p => Tuple.Create(p.Name, p.Value));
                var headers = FormatHeaders(headerList);
                var body = FormatBody(response.Content);
                var errorMessage = string.IsNullOrWhiteSpace(response.ErrorMessage) ? string.Empty :
                    "error message: " + response.ErrorMessage + CR;

                tracer("<- {0} {1} ({2}) {3}{4}{5}{6}{7}{8}", new object[]
                {
                    result,
                    (int)response.StatusCode,
                    response.StatusCode.ToString(),
                    response.ResponseUri, CR,
                    errorMessage,
                    timings,
                    headers,
                    body,
                });
            }
        }
    }
}

namespace MdlpApiClient
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using MdlpApiClient.DataContracts;

    [Serializable]
    public class MdlpException : Exception
    {
        public MdlpException(HttpStatusCode code, string message, ErrorResponse errorResponse, Exception innerException)
            : base(GetMessage(code, message), innerException)
        {
            StatusCode = code;
            ErrorResponse = errorResponse;
        }

        private static string GetMessage(HttpStatusCode code, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                return message;
            }

            return code.ToString();
        }

        protected MdlpException(SerializationInfo info, StreamingContext context)
        {
            StatusCode = (HttpStatusCode)info.GetInt32("Code");
            if (info.GetString("Path") != null)
            {
                ErrorResponse = new ErrorResponse
                {
                    TimeStamp = info.GetDateTime("TimeStamp"),
                    StatusCode = info.GetInt32("StatusCode"),
                    Error = info.GetString("Error"),
                    Message = info.GetString("Message"),
                    Path = info.GetString("Path"),
                    Description = info.GetString("Description"),
                };
            }
        }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorResponse ErrorResponse { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Code", (int)StatusCode);
            if (ErrorResponse != null)
            {
                info.AddValue("TimeStamp", ErrorResponse.TimeStamp);
                info.AddValue("StatusCode", ErrorResponse.StatusCode);
                info.AddValue("Error", ErrorResponse.Error);
                info.AddValue("Message", ErrorResponse.Message);
                info.AddValue("Path", ErrorResponse.Path);
                info.AddValue("Description", ErrorResponse.Description);
            }
        }
    }
}

namespace MdlpApiClient
{
    using DataContracts;

    /// <summary>
    /// Non-resident user credentials (password-based authentication).
    /// </summary>
    public class NonResidentCredentials : CredentialsBase
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// </inheritdoc>
        public override AuthToken Authenticate(MdlpClient apiClient)
        {
            // get authentication code
            var authResponse = apiClient.Post<AuthResponse>("auth", new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "PASSWORD",
            });

            // get authentication token
            return apiClient.Post<AuthToken>("token", new
            {
                code = authResponse.Code,
                password = Password,
            });
        }
    }
}

namespace MdlpApiClient
{
    using System.Security;
    using DataContracts;
    using MdlpApiClient.Toolbox;

    /// <summary>
    /// Resident credentials. Uses GOST cryptocertificate with a private key.
    /// </summary>
    public class ResidentCredentials : CredentialsBase
    {
        /// </inheritdoc>
        public override AuthToken Authenticate(MdlpClient apiClient)
        {
            // load the certificate with a private key by userId
            var certificate = apiClient.UserCertificate;
            if (certificate == null)
            {
                throw new SecurityException("GOST-compliant certificate not found. " +
                    "Make sure that the certificate is properly installed and has the associated private key. " +
                    "Thumbprint or subject name: " + UserID);
            }

            // get authentication code
            var authResponse = apiClient.Post<AuthResponse>("auth", new
            {
                client_id = ClientID,
                client_secret = ClientSecret,
                user_id = UserID,
                auth_type = "SIGNED_CODE",
            });

            // get authentication token
            return apiClient.Post<AuthToken>("token", new
            {
                code = authResponse.Code,
                signature = GostCryptoHelpers.ComputeDetachedSignature(certificate, authResponse.Code),
            });
        }
    }
}

namespace MdlpApiClient.Serialization
{
    using RestSharp;
    using RestSharp.Serialization;
    using ServiceStack.Text;

    /// <summary>
    /// ServiceStack.Text.v4.0.33-based serializer.
    /// </summary>
    internal class ServiceStackSerializer : IRestSerializer
    {
        //public ServiceStackSerializer()
        //{
        //    JsConfig.DateHandler = DateHandler.ISO8601;// ISO8601;
        //}

        public string[] SupportedContentTypes
        {
            get
            {
                return new[]
                {
                    "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
                };
            }
        }

        public DataFormat DataFormat
        {
            get { return DataFormat.Json; }
        }

        private string contentType = "application/json";

        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonSerializer.DeserializeFromString<T>(response.Content);
        }

        public string Serialize(Parameter bodyParameter)
        {
            return Serialize(bodyParameter.Value);
        }

        public string Serialize(object obj)
        {
            using (var scope = JsConfig.BeginScope())
            {
                scope.DateHandler = DateHandler.UnixTime;
                return JsonSerializer.SerializeToString(obj);
            }
        }
    }
}

namespace MdlpApiClient.Toolbox
{
    using GostCryptography.Base;
    using GostCryptography.Pkcs;
    using System;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class GostCryptoHelpers
    {
        /// <summary>
        /// For the unit tests, set this to the StoreLocation.CurrentUser.
        /// For the production code, keep it set to the StoreLocation.LocalMachine.
        /// </summary>
        public static StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

        /// <summary>
        /// Checks if GOST cryptoprovider CryptoPro is installed.
        /// </summary>
        public static bool IsGostCryptoProviderInstalled()
        {
            return
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro) &&
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro_2012_512) &&
                GostCryptography.Native.CryptoApiHelper.IsInstalled(ProviderType.CryptoPro_2012_1024);
        }

        /// <summary>
        /// Looks for the GOST certificate with a private key using the subject name or a thumbprint.
        /// Returns null, if certificate is not found, the algorithm isn't GOST-compliant, or the private key is not associated with it.
        /// </summary>
        public static X509Certificate2 FindCertificate(string cnameOrThumbprint, StoreName storeName = StoreName.My, StoreLocation? storeLocation = null)
        {
            // avoid returning any certificate
            if (string.IsNullOrWhiteSpace(cnameOrThumbprint))
            {
                return null;
            }

            // a thumbprint is a hexadecimal number, compare it case-insensitive
            using (var store = new X509Store(storeName, storeLocation ?? DefaultStoreLocation))
            {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                foreach (var certificate in store.Certificates)
                {
                    if (certificate.HasPrivateKey && certificate.IsGost())
                    {
                        var nameMatches = certificate.SubjectName.Name.IndexOf(cnameOrThumbprint, StringComparison.OrdinalIgnoreCase) >= 0;
                        var thumbprintMatches = StringComparer.OrdinalIgnoreCase.Equals(certificate.Thumbprint, cnameOrThumbprint);
                        if (nameMatches || thumbprintMatches)
                        {
                            return certificate;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Signs the message with a GOST digital signature and returns the detached signature (CMS format, base64 encoding).
        /// Detached signature is a CMS message, that doesn't contain the original signed data: only the signature and the certificates.
        /// </summary>
        public static string ComputeDetachedSignature(X509Certificate2 certificate, string textToSign)
        {
            // The following line opens the private key.
            // It requires that the current user has permissions to use the private key.
            // Permissions are given using MMC console, Certificates snap-in.
            var privateKey = (GostAsymmetricAlgorithm)certificate.GetPrivateKeyAlgorithm();
            var publicKey = (GostAsymmetricAlgorithm)certificate.GetPublicKeyAlgorithm();
            var message = Encoding.UTF8.GetBytes(textToSign);

            // Create GOST-compliant signature helper
            var signedCms = new GostSignedCms(new ContentInfo(message), true);

            // The object that has the signer information
            var signer = new CmsSigner(certificate);

            // Computing the CMS/PKCS#7 signature
            signedCms.ComputeSignature(signer);

            // Encoding the CMS/PKCS#7 message
            var encoded = signedCms.Encode();
            return Convert.ToBase64String(encoded);
        }
    }
}

namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Helper class for working with hashes.
    /// </summary>
    public static class HashUtilities
    {
        /// <summary>
        /// Returns the hex representation for an array of bytes (for example, md5-hash).
        /// </summary>
        /// <param name="hash">Byte array to convert.</param>
        public static string ToHexString(this byte[] hash)
        {
            return string.Join(string.Empty, (hash ?? EmptyBuffer).Select(b => string.Format("{0:x2}", b)));
        }

        /// <summary>
        /// Converts hexadecimal string such as md5 hash back to the array of bytes.
        /// </summary>
        /// <param name="hexString">Hexadecimal string, such as "cafebabe".</param>
        public static byte[] FromHexString(this string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return EmptyBuffer;
            }

            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentOutOfRangeException("hexString", "Hexadecimal string length should be even.");
            }

            var result = new byte[hexString.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return result;
        }

        /// <summary>
        /// Computes the hash of an array of bytes.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="data">The data to hash.</param>
        public static string ComputeHash<T>(this byte[] data)
            where T : HashAlgorithm
        {
            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            var hash = hasher.ComputeHash(data);
            return hash.ToHexString();
        }

        /// <summary>
        /// Empty byte array.
        /// </summary>
        public static readonly byte[] EmptyBuffer = new byte[0];

        /// <summary>
        /// Computes the hash of the specified string.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="str">The string to hash.</param>
        public static string ComputeHash<T>(this string str)
            where T : HashAlgorithm
        {
            var bytes = EmptyBuffer;
            if (!string.IsNullOrEmpty(str))
            {
                bytes = Encoding.Default.GetBytes(str);
            }

            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            bytes = hasher.ComputeHash(bytes);
            return bytes.ToHexString();
        }

        /// <summary>
        /// Computes the hash for the stream.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="fs"><see cref="Stream"/> of bytes to hash.</param>
        public static string ComputeHash<T>(this Stream fs)
            where T : HashAlgorithm
        {
            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            var hash = hasher.ComputeHash(fs);
            return hash.ToHexString();
        }

        /// <summary>
        /// Checks whether the given enumerable sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">Sequence element type.</typeparam>
        /// <param name="sequence">Enumerable sequence.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence == null || !sequence.Any();
        }
    }
}

namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class HtmlHelper
    {
        /// <summary>
        /// Try to extract readable text from HTML.
        /// </summary>
        /// <param name="html">HTML to process.</param>
        /// <returns>Human-readable text.</returns>
        public static string ExtractText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            var bodyStart = html.IndexOf("<body");
            if (bodyStart >= 0)
            {
                html = html.Substring(bodyStart);
            }

            // replace tags
            var text = new Regex("<(br/?)|(</h[1-6])>").Replace(html, Environment.NewLine);
            text = new Regex("<[^>]+>").Replace(text, " ");
            text = new Regex("[ \t]+").Replace(text, " ");

            // trim lines
            var lines = text.Split('\r').Select(l => l.Trim()).Where(l => !string.IsNullOrEmpty(l));
            return string.Join(Environment.NewLine, lines);
        }
    }
}

namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Linq;

    /// <summary>
    /// Dependency-free JSON formatter.
    /// </summary>
    public static class JsonFormatter
	{
		/// <summary>
		/// Formats the given JSON code.
		/// </summary>
		/// <remarks>
		/// Based on:
		/// https://stackoverflow.com/a/57100143/544641
		/// https://stackoverflow.com/a/24782322/544641
		/// </remarks>
		/// <param name="json">JSON code to format.</param>
		/// <param name="indent">Optional indent.</param>
		public static string FormatJson(string json, string indent = "  ")
		{
			var indentation = 0;
			var quoteCount = 0;
			var escapeCount = 0;

			var result =
				from ch in json ?? string.Empty
				let escaped = (ch == '\\' ? escapeCount++ : escapeCount > 0 ? escapeCount-- : escapeCount) > 0
				let quotes = ch == '"' && !escaped ? quoteCount++ : quoteCount
				let unquoted = quotes % 2 == 0
				let colon = ch == ':' && unquoted ? ": " : null
				let nospace = char.IsWhiteSpace(ch) && unquoted ? string.Empty : null
				let lineBreak = ch == ',' && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, indentation)) : null
				let openChar = (ch == '{' || ch == '[') && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, ++indentation)) : ch.ToString()
				let closeChar = (ch == '}' || ch == ']') && unquoted ? Environment.NewLine + string.Concat(Enumerable.Repeat(indent, --indentation)) + ch : ch.ToString()
				select colon ?? nospace ?? lineBreak ?? (
					openChar.Length > 1 ? openChar : closeChar
				);

			return string.Concat(result);
		}
	}
}

