using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static logikaAnimeTimes.AniListCommunication;

namespace logikaAnimeTimes
{
    class Program
    {
        class Title
        {
            [JsonProperty("romaji")]
            public string Romaji { get; set; }
            [JsonProperty("english")]
            public string English { get; set; }
            [JsonProperty("native")]
            public string Native { get; set; }
        }

        class Media
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public Title Title { get; set; }

        }
        static void Main(string[] args)
        {
            string query = @"
              query($id: Int) { # Define which variables will be used in the query (id)
              Media(id: $id, type: ANIME) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
                id
                tit {
                    romaji
                    english
                    native
                }
                }
            }
            ";

            Dictionary<string, object> variables = new Dictionary<string, object>()
            {
                {"id",  15125}
            };

            //AniListCommunication obj = new AniListCommunication("https://graphql.anilist.co");
            //GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            //Media gpl = result.GetData<Media>("Media");
            //if (gpl == null) Console.Write(result.GetErrors());
            //else Console.Write(gpl.Title.English);
            DataHandler.GetUserWatchAnimeList("");
            Console.ReadKey();
        }
    }
}
