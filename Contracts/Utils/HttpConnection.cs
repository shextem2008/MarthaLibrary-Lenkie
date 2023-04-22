using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Utils
{
    public class HttpConnection
    {
        public static async Task<T> WebRequest<T>(string url, object request, string requestType, Dictionary<string, string> headers = null, string authUserName = null, string authPword = null) where T : new()
        {
            T result = new T();

            Method method = requestType.ToLower() == "post" ? Method.Post : Method.Get;
            var client = new RestClient(url);
            var restRequest = new RestRequest(url, method);

            if (method == Method.Post)
            {
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.AddJsonBody(request);
            }

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    restRequest.AddHeader(item.Key, item.Value);
                }
            }

            if (!string.IsNullOrEmpty(authUserName) && !string.IsNullOrEmpty(authPword))
            {
                restRequest.Authenticator = new HttpBasicAuthenticator(authUserName, authPword);
            }

            try
            {
                RestResponse<T> response = client.Execute<T>(restRequest);

                var res = JsonConvert.DeserializeObject<T>(response.Content);
                var Datacontents = response.Content;
                var ParseData = JObject.Parse(Datacontents);
                var obj = Convert.ToString(ParseData["object"]);
                result = JsonConvert.DeserializeObject<T>(obj);

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        private class NewtonsoftJsonSerializer : IDeserializer
        {
            private readonly Newtonsoft.Json.JsonSerializer serializer;

            public NewtonsoftJsonSerializer(JsonSerializer serializer)
            {
                this.serializer = serializer;
            }

            public string ContentType
            {
                get { return "application/json"; } // Probably used for Serialization?
                set { }
            }

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            public string Serialize(object obj)
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                    {
                        serializer.Serialize(jsonTextWriter, obj);

                        return stringWriter.ToString();
                    }
                }
            }

            public T? Deserialize<T>(RestResponse response)
            {
                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }

            public static NewtonsoftJsonSerializer Default
            {
                get
                {
                    return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
                }
            }

        }
    }
}
