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
                Query = Query;
                Variables = Variables;
            }
        }

        public class GraphQlQueryResponse
        {
            private readonly string raw;
            private readonly JObject data;

            public GraphQlQueryResponse(string raw)
            {
                this.raw = raw;
                data = raw != null ? JObject.Parse(raw) : null;
            }

            public string GetRaw()
            {
                return raw;
            }

            public JObject GetData()
            {
                return data;
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
