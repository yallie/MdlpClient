using System;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace MdlpApiClient
{
    public class MdlpClient
    {
        public MdlpClient(
            string baseUrl = "http://api.stage.mdlp.crpt.ru/api/v1/",
            IGostCryptoService cryptoService = null,
            Action<string, object[]> tracer = null)
        {
            GostCryptoService = cryptoService;
            Tracer = tracer;
            BaseUrl = baseUrl;
            if (!BaseUrl.EndsWith("/"))
            {
                BaseUrl += "/";
            }

            if (Client == null)
            {
                Client = new HttpClient();
            }
        }

        // API url, i.e. https://api.stage.mdlp.crpt.ru/api/v1/
        public string BaseUrl { get; private set; }

        private IGostCryptoService GostCryptoService { get; set; }

        private X509Certificate2 FindCertificate(string name)
        {
            return GostCryptoService != null ? GostCryptoService.FindCertificate(name) : null;
        }

        private string ComputeSignature(string message)
        {
            if (GostCryptoService == null || UserCertificate == null)
            {
                return null;
            }

            return GostCryptoService.ComputeDetachedSignature(UserCertificate, message);
        }

        private Action<string, object[]> Tracer { get; set; }

        private void Trace(string format, params object[] arguments)
        {
            if (Tracer != null)
            {
                Tracer.Invoke(format, arguments);
            }
        }

        // Using single shared HttpClient instance as recommended here:
        // https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/
        private static HttpClient Client { get; set; }

        // Sync helpers for HttpClient:
        // https://cpratt.co/async-tips-tricks/
        // https://stackoverflow.com/a/56241835/544641
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        // var result = RunSync(() => RunSomethingAsync());
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        // RunSync(() => RunSomethingAsync());
        public static void RunSync(Func<Task> func)
        {
            _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public HttpResponseMessage Post(string url, string body, string contentType = "application/json")
        {
            var content = new StringContent(body, Encoding.UTF8, contentType);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri(url)
            };

            // add authorization token header if available
            var token = "none";
            if (Token != null && !string.IsNullOrWhiteSpace(Token.Token))
            {
                token = Token.Token;
                request.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            }

            Trace(@"-> {0}{1}Content: {2}", request.ToString().Replace(@", RequestUri:", "").Replace(@", Content: System.Net.Http.StringContent", "").Replace(@"Version: 1.1, ", ""),
                Environment.NewLine, Ultima.Toolbox.JsonFormatter.FormatJson(body));

            var result = RunSync(() => Client.SendAsync(request));
            var reply = RunSync(() => result.Content.ReadAsStringAsync());
            var formatted = Ultima.Toolbox.JsonFormatter.FormatJson(reply);

            Trace(@"<- {0}{1}Content: {2}", result.ToString().Replace(@"Version: 1.1, Content: System.Net.Http.StreamContent, ", ""),
                Environment.NewLine, formatted);

            if (!result.IsSuccessStatusCode)
            {
                throw new UltimaException(@"Http request failed: " + result.StatusCode +
                    Environment.NewLine + @"Token = " + (Token != null ? Token.Token : "none") +
                    Environment.NewLine + @"Content = " + formatted);
            }

            return result;
        }

        public HttpResponseMessage Get(string url, string contentType = "application/json")
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            // add authorization token header if available
            var token = "none";
            if (Token != null && !string.IsNullOrWhiteSpace(Token.Token))
            {
                token = Token.Token;
                request.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            }

            Trace(@"-> {0}", request.ToString().Replace(@", RequestUri:", "")
                .Replace(@"Version: 1.1, ", "").Replace(@"Content: <null>, ", ""));

            var result = RunSync(() => Client.SendAsync(request));
            var reply = RunSync(() => result.Content.ReadAsStringAsync());
            var formatted = Ultima.Toolbox.JsonFormatter.FormatJson(reply);

            Trace(@"<- {0}{1}Content: {2}", result.ToString().Replace(@"Version: 1.1, Content: System.Net.Http.StreamContent, ", ""),
                Environment.NewLine, formatted);

            if (!result.IsSuccessStatusCode)
            {
                throw new UltimaException(@"Http request failed: " + result.StatusCode +
                    Environment.NewLine + @"Token = " + (Token != null ? Token.Token : "none") +
                    Environment.NewLine + @"Content = " + formatted);
            }

            return result;
        }

        public static string Serialize<T>(T requestBody)
        {
            return JsonSerializer.SerializeToString(requestBody);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.DeserializeFromString<T>(json);
        }

        // возвращает токен авторизации и время жизни (в минутах)
        public AuthToken Authenticate(string clientId, string clientSecret, string userId, string password = null)
        {
            // авторизация по паролю (только для нерезидентов)
            if (!string.IsNullOrWhiteSpace(password))
            {
                var authCode = Auth(clientId, clientSecret, userId, passwordAuth: true);
                return GetToken(authCode, password);
            }

            // если пароль не указан, поищем сертификат для пользователя
            UserCertificate = FindCertificate(userId);
            if (UserCertificate == null)
            {
                var msg = @"Сертификат не найден: {0}. Установите сертификат в хранилище компьютера с помощью консоли MMC, остнастка Certificates";
                throw new UltimaException(string.Format(msg, userId));
            }

            // авторизация с помощью сертификата (только для резидентов)
            var code = Auth(clientId, clientSecret, userId: UserCertificate.Thumbprint, passwordAuth: false);
            var signature = ComputeSignature(code);
            return GetTokenUsingSignedCode(code, signature);
        }

        // сертификат пользователя-резидента, которым подписываются все документы
        private X509Certificate2 UserCertificate { get; set; }

        // returns code
        private string Auth(string clientId, string clientSecret, string userId, bool passwordAuth)
        {
            var url = BaseUrl + "auth";
            var requestBody = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                user_id = userId,
                auth_type = passwordAuth ? "PASSWORD" : "SIGNED_CODE"
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var authResult = Deserialize<AuthResponse>(content);
            return authResult.Code;
        }

        [DataContract]
        private class AuthResponse
        {
            [DataMember(Name = "code")]
            public string Code { get; set; }
        }

        // returns authentication token and its life time
        private AuthToken GetToken(string authCode, string password)
        {
            var url = BaseUrl + "token";
            var requestBody = new
            {
                code = authCode,
                password = password,
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());

            // save the token for later use
            Token = Deserialize<AuthToken>(content);
            return Token;
        }

        // The auth token is used by all methods that need authorization
        // How to obtain the token: Authenticate, GetToken
        public AuthToken Token { get; private set; }

        // returns authentication token and its life time
        private AuthToken GetTokenUsingSignedCode(string code, string signature)
        {
            var url = BaseUrl + "token";
            var requestBody = new
            {
                code = code,
                signature = signature,
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());

            // save the token for later use
            Token = Deserialize<AuthToken>(content);
            return Token;
        }

        [DataContract]
        public class AuthToken
        {
            [DataMember(Name = "token")]
            public string Token { get; set; }

            [DataMember(Name = "life_time")]
            public int LifeTime { get; set; }
        }

        // returns user identity (a guid), requires authorization
        public string RegisterResidentUser(string sysId, string publicCertificate,
            string firstName, string lastName, string middleName, string email)
        {
            var url = BaseUrl + "registration/user_resident";
            var requestBody = new
            {
                sys_id = sysId,
                public_cert = publicCertificate,
                first_name = firstName,
                last_name = lastName,
                middle_name = middleName,
                email = email
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var user = Deserialize<RegisterResidentUserResponse>(content);
            return user.UserID;
        }

        [DataContract]
        private class RegisterResidentUserResponse
        {
            [DataMember(Name = "user_id")]
            public string UserID { get; set; }
        }

        // returns group identity (a guid), requires authorization
        public string CreateRightsGroup(string groupName, string[] rights)
        {
            var url = BaseUrl + "rights/create_group";
            var requestBody = new
            {
                group_name = groupName,
                rights = rights
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var group = Deserialize<CreateRightsGroupResponse>(content);
            return group.GroupID;
        }

        [DataContract]
        private class CreateRightsGroupResponse
        {
            [DataMember(Name = "group_id")]
            public string GroupID { get; set; }
        }

        // requires authorization
        public void AddUserToTheRightsGroup(string userId, string groupId)
        {
            var url = BaseUrl + "rights/" + groupId + "/user_add";
            var requestBody = new
            {
                user_id = userId,
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            Debug.Assert(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        // требует авторизации
        // возвращает идентификатор документа
        public string SendDocument(string document)
        {
            var url = BaseUrl + "documents/send";
            var docBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(document));
            var requestBody = new
            {
                document = docBase64,
                sign = ComputeSignature(document),
                request_id = Guid.NewGuid().ToString()
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var doc = Deserialize<SendDocumentResponse>(content);
            return doc.DocumentID;
        }

        [DataContract]
        private class SendDocumentResponse
        {
            [DataMember(Name = "document_id")]
            public string DocumentID { get; set; }
        }

        // требует авторизации
        // возвращает метаданные документа, типа общего заголовка
        public DocumentMetadata GetDocumentMetadata(string documentId)
        {
            var url = BaseUrl + "documents/" + documentId;
            var result = Get(url);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var doc = Deserialize<DocumentMetadata>(content);
            return doc;
        }

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

        // требует авторизации
        // возвращает ссылку на просмотр документа
        public string GetDocumentLink(string documentId)
        {
            var url = BaseUrl + "documents/download/" + documentId;
            var result = Get(url);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var doc = Deserialize<GetDocumentLinkResponse>(content);
            return doc.Link;
        }

        [DataContract]
        private class GetDocumentLinkResponse
        {
            [DataMember(Name = "link")]
            public string Link { get; set; }
        }

        // требует авторизации
        // скачивает документ по ссылке
        public string DownloadDocument(string documentLink)
        {
            var result = Get(documentLink);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            return content;
        }

        // требует авторизации
        // возвращает ссылку на квитанцию документа
        public string GetDocumentTicketLink(string documentId)
        {
            var url = BaseUrl + "documents/" + documentId + "/ticket";
            var result = Get(url);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var doc = Deserialize<GetDocumentLinkResponse>(content);
            return doc.Link;
        }

        private object GetDocFilter(MdlpDocFilter filter)
        {
            if (filter == null)
            {
                return new {};
            }

            // ServiceStack пропускает null-поля, это нам и нужно!
            return new
            {
                start_date = filter.StartDate,
                end_date = filter.EndDate,
                processed_date_from = filter.ProcessedDateFrom,
                processed_date_to = filter.ProcessedDateTo,
                document_id = filter.DocumentID,
                request_id = filter.RequestID,
                receiver_id = filter.ReceiverID,
                sender_id = filter.SenderID,
                doc_type = filter.DocType,
                doc_status = filter.DocStatus,
            };
        }

        // требует авторизации
        // возвращает список исходящих документов
        public GetDocumentsResponse GetOutcomeDocuments(MdlpDocFilter filter, int startFrom, int count)
        {
            var url = BaseUrl + "documents/outcome";
            var requestBody = new
            {
                filter = GetDocFilter(filter),
                start_from = startFrom,
                count = count
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var docs = Deserialize<GetDocumentsResponse>(content);
            return docs;
        }

        // требует авторизации
        // возвращает список входящих документов
        public GetDocumentsResponse GetIncomeDocuments(MdlpDocFilter filter, int startFrom, int count)
        {
            var url = BaseUrl + "documents/income";
            var requestBody = new
            {
                filter = GetDocFilter(filter),
                start_from = startFrom,
                count = count
            };

            var json = Serialize(requestBody);
            var result = Post(url, json);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var docs = Deserialize<GetDocumentsResponse>(content);
            return docs;
        }

        // требует авторизации
        // возвращает список документов по идентификатору запроса
        public GetDocumentsResponse GetDocuments(string requestId)
        {
            var url = BaseUrl + "documents/request/" + requestId;
            var result = Get(url);
            var content = RunSync(() => result.Content.ReadAsStringAsync());
            var docs = Deserialize<GetDocumentsResponse>(content);
            return docs;
        }

        [DataContract]
        public class GetDocumentsResponse
        {
            [DataMember(Name = "documents")]
            public DocumentMetadata[] Documents { get; set; }

            [DataMember(Name = "total")]
            public int Total { get; set; }
        }

        public void GetSgtin(int startFrom, int count)
        {
        }

        [DataContract]
        public class GetSgtinResponse
        {
            [DataMember(Name = "documents")]
            public DocumentMetadata[] Documents { get; set; }

            [DataMember(Name = "total")]
            public int Total { get; set; }
        }
    }
}
