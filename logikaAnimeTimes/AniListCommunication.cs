using logikaAnimeTimes.AbstractClasses;
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
    /// <summary>
    /// Komunikacja z api AniList
    /// </summary>
    class AniListCommunication : Communication
    {
        /// <summary>
        /// Klasa zawierająca zapytanie na serwer anilist
        /// </summary>
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


        private HttpClient client;
        private readonly string url;

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="url"></param>
        public AniListCommunication(string url)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.url = url;
        }

        /// <summary>
        /// Tworzy zapytanie i odpowiedź z serwera AniList
        /// </summary>
        /// <param name="query"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public override async Task<GraphQlQueryResponse> MakeRequestAsync(string query, Dictionary<string, object> variables)
        {
            GraphQLQuery q = new GraphQLQuery(query, variables);

            string jsonContent = JsonConvert.SerializeObject(q);
            string reponse = await RequestAsync(url, jsonContent);

            return new GraphQlQueryResponse(reponse);
        }

        /// <summary>
        /// Wysyła zapytanie i odbiera odpowiedź
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private async Task<string> RequestAsync(string url, string json)
        {
            HttpContent validJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, validJson);
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
