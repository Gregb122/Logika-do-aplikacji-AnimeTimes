using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static logikaAnimeTimes.AniListCommunication;

namespace logikaAnimeTimes
{
    class DataHandler
    {
        public static void GetUserWatchAnimeList(string user)
        {
            string query = @"
              query($userName: String) { # Define which variables will be used in the query (id)
              MediaListCollection(userName: $userName, type: ANIME, status: CURRENT) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
                lists {
                  entries {
                    media {
                      title {
                        romaji
                      }
                      nextAiringEpisode{
                        airingAt
                        episode
                      }
                    }
                  }
               }
               }
            }
            ";

            Dictionary<string, object> variables = new Dictionary<string, object>()
            {
                {"userName",  "gregb12"}
            };
            AniListCommunication obj = new AniListCommunication("https://graphql.anilist.co");
            GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            dynamic gpl = result.GetData("MediaListCollection");
            if (gpl == null) Console.Write(result.GetErrors());
            else Console.Write(gpl.lists[0].entries[1]);
        }
    }
}
