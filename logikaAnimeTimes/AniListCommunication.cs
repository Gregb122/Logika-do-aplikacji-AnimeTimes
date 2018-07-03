using logikaAnimeTimes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace logikaAnimeTimes
{
    class AniListCommunication : ICommunication
    {
        private class GraphQLQuery
        {
            [JsonProperty("query")]
            public string Query { get; set; }

            [JsonProperty("variables")]
            public Dictionary<string, object> Variables { get; set; }

            public GraphQLQuery(string query, Dictionary<string, object> variables)
            {
                Query = query;
                Variables = variables;
            }
        }

        public class GraphQlQueryResponse
        {
            private readonly string raw;
            private readonly JObject data;
            private readonly string errors;

            public GraphQlQueryResponse(string raw)
            {
                this.raw = raw;
                data = raw != null ? JObject.Parse(raw) : null;
                errors = data["errors"] != null ? data["errors"].ToString() : null;
            }

            public string GetRaw()
            {
                return raw;
            }

            public string GetErrors()
            {
                return errors;
            }

            public T GetData<T>(string key)
            {
                if (data == null) return default(T);
                try
                {
                    return JsonConvert.DeserializeObject<T>(data["data"][key].ToString());
                }
                catch
                {
                    return default(T);
                }
            }

            public dynamic GetData(string key)
            {
                try
                {
                    return JsonConvert.DeserializeObject<dynamic>(data["data"][key].ToString());
                }
                catch
                {
                    return null;
                }
            }
        }

        private HttpClient client;
        private readonly string url;

        public AniListCommunication(string url)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.url = url;
        }

        public async Task<GraphQlQueryResponse> MakeRequestAsync(string query, Dictionary<string, object> variables)
        {
            GraphQLQuery q = new GraphQLQuery(query, variables);

            string jsonContent = JsonConvert.SerializeObject(q);
            string reponse = await RequestAsync(url, jsonContent);

            return new GraphQlQueryResponse(reponse);
        }

        private async Task<string> RequestAsync(string url, string json)
        {
            HttpContent validJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, validJson);
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
