# Честный знак. ИС «Маркировка». МДЛП

[![MDLP REST API Client v3.05](https://img.shields.io/badge/честный%20знак-v3.06.1-yellowgreen)](https://честныйзнак.рф/upload/iblock/200/IS-_Markirovka_.-MDLP.-Protokol-obmena-interfeysnogo-urovnya.pdf)
[![.NET Framework 4.62](https://img.shields.io/badge/.net-v4.62-yellow)](https://www.microsoft.com/ru-RU/download/details.aspx?id=53321)
[![Code Coverage](https://codecov.io/gh/yallie/mdlpclient/branch/master/graph/badge.svg)](https://codecov.io/gh/yallie/mdlpclient)
[![Unit tests](https://img.shields.io/badge/build%20passing-150%20tests-brightgreen)](https://github.com/yallie/MdlpClient/tree/master/MdlpApiClient.Tests)
[![Code quality](https://img.shields.io/codefactor/grade/github/yallie/MdlpClient)](https://www.codefactor.io/repository/github/yallie/mdlpclient)
[![NuGet](https://img.shields.io/nuget/v/MdlpApiClient.svg)](https://www.nuget.org/packages/MdlpApiClient)

<img alt="MDLP API client logo" src="https://raw.githubusercontent.com/yallie/MdlpClient/master/Docs/MdlpApiClientLogo.png" width="50" align="left" />

Протокол обмена интерфейсного уровня: [PDF](https://честныйзнак.рф/upload/iblock/200/IS-_Markirovka_.-MDLP.-Protokol-obmena-interfeysnogo-urovnya.pdf)  
Список реализованных методов: [TXT](https://github.com/yallie/MdlpClient/blob/master/Docs/list_of_methods.txt)

# Инструкция по быстрому старту

1. Прочитайте эту инструкцию, чтобы настроить окружение для работы с API: [PDF](https://честныйзнак.рф/upload/iblock/25b/Kratkaya_instruktsiya_po_bystromu_startu_dlya_izucheniya_API.pdf)  
2. Регистрация тестовой электронной подписи УКЭП в деталях [описана тут](https://github.com/yallie/MdlpClient/blob/master/Docs/Certificates/CreateTestCertificate.md).

# Как использовать REST API-клиент

1. Установить провайдер КриптоПро: http://cryptopro.ru/products/cryptopro-csp
2. Запросить в техподдержке «Честного знака» доступ к тестовому стенду API: http://api.stage.mdlp.crpt.ru
3. Добавить в hosts строчку с IP-адресом тестового стенда:

```txt
185.196.171.27 api.stage.mdlp.crpt.ru
``` 

4. Добавить в проект Nuget-пакет: https://www.nuget.org/packages/MdlpApiClient
5. Завести себе тестовый УКЭП по этой [инструкции](https://github.com/yallie/MdlpClient/blob/master/Docs/Certificates/CreateTestCertificate.md).
6. Проверить вызовы API по документу «Быстрый старт».

## Авторизация тестовым участником

Клиент для API называется `MdlpClient` (МДЛП — Мониторинг Движения Лекарственных Препаратов).
По умолчанию клиент работает с адресом тестовой системы.

Почти все методы работы с API требуют авторизации. Для авторизации используются классы credentials.
Для авторизации пользователя-нерезидента (логин и пароль) используется `NonResidentCredentials`.
Для резидента (авторизация с помощью сертификата ГОСТ) — соответственно, `ResidentCredentials`.

При запуске конструктора клиент просто сохраняет указанные credentials, но не устанавливает соединение.
Установка соединения и авторизация происходит при вызове любого метода API:

```c#
// клиент пока никуда не коннектится
var client = new MdlpClient(credentials: new NonResidentCredentials
{
    ClientID = "01db16f2-9a4e-4d9f-b5e8-c68f12566fd5",
    ClientSecret = "9199fe04-42c3-4e81-83b5-120eb5f129f2",
    UserID = "starter_resident_1",
    Password = "password"
});

// тут будет установлено соединение и выполнен запрошенный метод API:
var doc = client.GetDocumentMetadata("60786bb4-fcb5-4587-b703-d0147e3f9d1c");
Console.WriteLine($"Код документа: {doc.DocumentID}");
Console.WriteLine($"Код запроса: {doc.RequestID}");
```

Чтобы трассировать все http-запросы и ответы, можно установить свойство `Tracer`:

```c#
client.Tracer = Console.WriteLine;
```
<details>
  <summary>При выполнении вышеприведенного кода трассировка выглядит так:</summary>
    
```c
// Authenticate
-> POST http://api.stage.mdlp.crpt.ru/api/v1/auth
headers: {
  X-ApiMethodName = Authenticate
  Accept = application/json, text/json, text/x-json, text/javascript, application/xml, text/xml
  Content-type = application/json
}
body: {
  "client_id": "01db16f2-9a4e-4d9f-b5e8-c68f12566fd5",
  "client_secret": "9199fe04-42c3-4e81-83b5-120eb5f129f2",
  "user_id": "starter_resident_1",
  "auth_type": "PASSWORD"
}

<- OK 200 (OK) http://api.stage.mdlp.crpt.ru/api/v1/auth
timings: {
  started: 2020-04-22 20:22:12
  elapsed: 00:00:00.1971392
}
headers: {
  Connection = keep-alive
  X-XSS-Protection = 1; mode=block
  Pragma = no-cache
  X-Frame-Options = DENY
  X-Content-Type-Options = nosniff
  X-Application-Context = authentication-service-frontend:8095
  Strict-Transport-Security = max-age=15768000
  Content-Length = 47
  Cache-Control = no-cache, no-store, max-age=0, must-revalidate
  Content-Type = application/json;charset=UTF-8
  Date = Wed, 22 Apr 2020 17:22:12 GMT
  Expires = 0
  Server = nginx/1.14.0
}
body: {
  "code": "7c08d5f3-4a0c-4a71-b123-638533b4612c"
}

// GetToken
-> POST http://api.stage.mdlp.crpt.ru/api/v1/token
headers: {
  X-ApiMethodName = GetToken
  Accept = application/json, text/json, text/x-json, text/javascript, application/xml, text/xml
  Content-type = application/json
}
body: {
  "code": "7c08d5f3-4a0c-4a71-b123-638533b4612c",
  "password": "password"
}

<- OK 200 (OK) http://api.stage.mdlp.crpt.ru/api/v1/token
timings: {
  started: 2020-04-22 20:22:12
  elapsed: 00:00:00.2673376
}
headers: {
  Connection = keep-alive
  X-XSS-Protection = 1; mode=block
  Pragma = no-cache
  X-Frame-Options = DENY
  X-Content-Type-Options = nosniff
  X-Application-Context = authentication-service-frontend:8095
  Strict-Transport-Security = max-age=15768000
  Content-Length = 63
  Cache-Control = no-cache, no-store, max-age=0, must-revalidate
  Content-Type = application/json;charset=UTF-8
  Date = Wed, 22 Apr 2020 17:22:13 GMT
  Expires = 0
  Server = nginx/1.14.0
}
body: {
  "token": "9189625f-2bea-4cf9-a36d-2c827b08d276",
  "life_time": 30
}

// GetDocumentMetadata
-> GET http://api.stage.mdlp.crpt.ru/api/v1/documents/60786bb4-fcb5-4587-b703-d0147e3f9d1c
headers: {
  X-ApiMethodName = GetDocumentMetadata
  Authorization = token 9189625f-2bea-4cf9-a36d-2c827b08d276
  Accept = application/json, text/json, text/x-json, text/javascript, application/xml, text/xml
}

<- OK 200 (OK) http://api.stage.mdlp.crpt.ru/api/v1/documents/60786bb4-fcb5-4587-b703-d0147e3f9d1c
timings: {
  started: 2020-04-22 20:22:12
  elapsed: 00:00:00.5673959
}
headers: {
  Transfer-Encoding = chunked
  Connection = keep-alive
  X-Application-Context = mdlp-api-document-front
  X-Content-Type-Options = nosniff
  X-XSS-Protection = 1; mode=block
  Pragma = no-cache
  X-Frame-Options = DENY
  Strict-Transport-Security = max-age=15768000
  Cache-Control = no-cache, no-store, max-age=0, must-revalidate
  Content-Type = application/json;charset=UTF-8
  Date = Wed, 22 Apr 2020 17:22:13 GMT
  Expires = 0
  Server = nginx/1.14.0
}
body: {
  "request_id": "528700e0-f967-4ddb-995d-5c6c7b73bcc9",
  "document_id": "60786bb4-fcb5-4587-b703-d0147e3f9d1c",
  "date": "2020-04-07 07:55:33",
  "processed_date": "2020-04-07 07:55:33",
  "sender": "00000000100928",
  "receiver": "00000000100930",
  "sys_id": "9dedee17-e43a-47f1-910e-3a88ff6bc81b",
  "doc_type": 607,
  "doc_status": "PROCESSED_DOCUMENT",
  "file_uploadtype": 2,
  "sender_sys_id": "6f6fa779-b637-4234-9117-8ac4c1a9a81c",
  "version": "1.34"
}
```
</details>
