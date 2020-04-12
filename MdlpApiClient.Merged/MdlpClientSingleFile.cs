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
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "sender_id", IsRequired = false)]
        public string SenderID { get; set; }

        /// <summary>
        /// Уникальный идентификатор получателя.
        /// Идентификатор места осуществления деятельности, места ответственного 
        /// хранения или идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// Применимо для входящих документов.
        /// </summary>
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
        public const string FAILED_RESULT_READY = "FAILED_RESULT_READY";
    }
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
        public DocumentMetadata()
        {
        }

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
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// 5.7. Получение списка исходящих документов
    /// 5.8. Получение списка входящих документов
    /// </summary>
    [DataContract]
    public class GetDocumentsResponse
    {
        public GetDocumentsResponse()
        {
        }

        [DataMember(Name = "documents")]
        public List<DocumentMetadata> Documents { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
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

namespace MdlpApiClient
{
    using DataContracts;
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using MdlpApiClient.Toolbox;

    /// <remarks>
    /// This file contains strongly typed REST API methods.
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
        public GetDocumentsResponse GetOutcomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<GetDocumentsResponse>("documents/outcome", new
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
        public GetDocumentsResponse GetIncomeDocuments(DocFilter filter, int startFrom, int count)
        {
            return Post<GetDocumentsResponse>("documents/income", new
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
            return Get<DocumentMetadata>("documents/" + documentId);
        }
    }
}

namespace MdlpApiClient
{
    using DataContracts;

    /// <remarks>
    /// This file contains strongly typed REST API methods.
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
    using System.Text;
    using RestSharp;
    using System.Security.Cryptography.X509Certificates;
    using MdlpApiClient.Toolbox;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// MDLP REST API client.
    /// </summary>
    public partial class MdlpClient
    {
        public const string StageApiUrl = "http://api.stage.mdlp.crpt.ru/api/v1/";

        /// <summary>
        /// Initializes a new instance of the MDLP REST API client.
        /// </summary>
        /// <param name="credentials">Credentials used for authentication.</param>
        /// <param name="baseUrl">Base URL of the API endpoint.</param>
        public MdlpClient(CredentialsBase credentials, string baseUrl = StageApiUrl)
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
                ThrowOnAnyError = true
            };
        }

        public string BaseUrl { get; private set; }

        private IRestClient Client { get; set; }

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

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="request">The request to execute.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        internal T Execute<T>(IRestRequest request, string apiMethodName)
            where T : class, new()
        {
            if (!string.IsNullOrWhiteSpace(apiMethodName))
            {
                request.AddHeader(ApiMethodNameHeader, apiMethodName);
            }

            // trace requests and responses
            if (Tracer != null)
            {
                request.OnBeforeRequest = http => Trace(http, request);
                request.OnBeforeDeserialization = resp => Trace(resp);
            }

            var response = Client.Execute<T>(request);
            if (!response.IsSuccessful)
            {
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }

        /// <summary>
        /// Executes the given request and checks the result.
        /// </summary>
        /// <param name="request">The request to execute.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        internal void Execute(IRestRequest request, string apiMethodName)
        {
            if (!string.IsNullOrWhiteSpace(apiMethodName))
            {
                request.AddHeader(ApiMethodNameHeader, apiMethodName);
            }

            // trace requests and responses
            if (Tracer != null)
            {
                request.OnBeforeRequest = http => Trace(http, request);
                request.OnBeforeDeserialization = resp => Trace(resp);
            }

            var response = Client.Execute(request);
            if (!response.IsSuccessful)
            {
                Trace(response);
                throw new MdlpException(response.StatusCode, response.ErrorMessage, response.ErrorException);
            }
        }

        /// <summary>
        /// Performs GET request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public T Get<T>(string url, [CallerMemberName] string apiMethodName = null)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            return Execute<T>(request, apiMethodName);
        }

        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="body">Request body, to be serialized as JSON.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public T Post<T>(string url, object body, [CallerMemberName] string apiMethodName = null)
            where T : class, new()
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            return Execute<T>(request, apiMethodName);
        }

        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="url">Resource url.</param>
        /// <param name="body">Request body, to be serialized as JSON.</param>
        /// <param name="apiMethodName">Strong-typed REST API method name, for tracing.</param>
        public void Post(string url, object body, [CallerMemberName] string apiMethodName = null)
        {
            var request = new RestRequest(url, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
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

    partial class MdlpClient
    {
        private const string ApiMethodNameHeader = "X-ApiMethodName";

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
                var apiMethod = http.Headers.FirstOrDefault(h => StringComparer.OrdinalIgnoreCase.Equals(h.Name, ApiMethodNameHeader));
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

        private void Trace(IRestResponse response)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                var result = response.IsSuccessful ? "OK" : "ERROR";
                var headerList = response.Headers.Select(p => Tuple.Create(p.Name, p.Value));
                var headers = FormatHeaders(headerList);
                var body = FormatBody(response.Content);
                var errorMessage = string.IsNullOrWhiteSpace(response.ErrorMessage) ? string.Empty :
                    "error message: " + response.ErrorMessage + CR;

                tracer("<- {0} {1} ({2}) {3}{4}{5}{6}{7}", new object[]
                {
                    result,
                    (int)response.StatusCode,
                    response.StatusCode.ToString(),
                    response.ResponseUri, CR,
                    errorMessage,
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

    [Serializable]
    public class MdlpException : Exception
    {
        public MdlpException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = code;
        }

        protected MdlpException(SerializationInfo info, StreamingContext context)
        {
            StatusCode = (HttpStatusCode)info.GetInt32("Code");
        }

        public HttpStatusCode StatusCode { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Code", (int)StatusCode);
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

