namespace MdlpApiClient
{
    using RestSharp;
    using RestSharp.Serialization.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        private void Trace(IRestRequest request)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                var req = new
                {
                    resource = request.Resource,

                    // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                    // otherwise it will just show the enum value
                    parameters = request.Parameters.Select(parameter => new
                    {
                        name = parameter.Name,
                        value = parameter.Value,
                        type = parameter.Type.ToString()
                    }),

                    // ToString() here to have the method as a nice string otherwise it will just show the enum value
                    method = request.Method.ToString(),

                    // This will generate the actual Uri used in the request
                    uri = Client.BuildUri(request),
                };

                tracer("-> {0}", new[] { Json.Serialize(req) });
            }
        }

        private void Trace(IRestResponse response)
        {
            var tracer = Tracer;
            if (tracer != null)
            {
                var resp = new
                {
                    statusCode = response.StatusCode,
                    content = response.Content,
                    headers = response.Headers,

                    // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                    responseUri = response.ResponseUri,
                    errorMessage = response.ErrorMessage,
                };

                tracer("<- {0}", new[] { Json.Serialize(resp) });
            }
        }
    }
}
