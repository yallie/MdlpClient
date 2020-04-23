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

        /// <summary>
        /// Tracer function, such as <see cref="Console.WriteLine(string, object[])"/>.
        /// </summary>
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

        internal static string FormatHeaders(IEnumerable<Tuple<string, object>> headers)
        {
            if (headers == null || !headers.Any())
            {
                return "headers: none" + CR;
            }

            return "headers: {" + CR +
                string.Join(CR, headers.Select(h => "  " + h.Item1 + " = " + h.Item2)) +
            CR + "}" + CR;
        }

        internal static string FormatBody(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            return "body: " + JsonFormatter.FormatJson(content) + CR;
        }

        internal static string FormatBody(RequestBody body)
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

        internal static string FormatTimings(DateTime? startTime, Stopwatch stopwatch)
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
