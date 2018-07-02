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
        static void Main(string[] args)
        {

            string query = @"
              query($id: Int) { # Define which variables will be used in the query (id)
              Media(id: $id, type: ANIME) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
                id
                title {
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

            AniListCommunication obj = new AniListCommunication("https://graphql.anilist.co");
            GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            Console.Write(result.GetRaw());
            Console.ReadKey();
        }
    }
}
