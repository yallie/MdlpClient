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

        internal void Trace(IRestRequest request)
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
