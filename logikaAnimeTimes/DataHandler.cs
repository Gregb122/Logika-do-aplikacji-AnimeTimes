using logikaAnimeTimes.AbstractClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static logikaAnimeTimes.AniListCommunication;

namespace logikaAnimeTimes
{
    /// <summary>
    /// Odpowiada za przetwarzanie otrzymanych danych.
    /// Zawiera metody odpowiedzialne za dostanie odpowiednio sformatowanych danych
    /// </summary>
    class DataHandler
    {
        private Communication obj;

        /// <summary>
        /// konstruktor tworzy nowy obiekt odpowiedzialny za komunikację z serwerem
        /// </summary>
        public DataHandler()
        {
            obj = new AniListCommunication("https://graphql.anilist.co");
        }

        /// <summary>
        /// Pobiera lsitę aktualnie oglądanych anime przez urzytkownika
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserWatchAnimeList(string user)
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
                {"userName", user}
            };

            GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            return MakeString(result);
        }

        /// <summary>
        /// Pobiera listę obejrzanych anime przez użytkownika
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserCompleteAnimeList(string user)
        {
            string query = @"
              query($userName: String) { # Define which variables will be used in the query (id)
              MediaListCollection(userName: $userName, type: ANIME, status: COMPLETED) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
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
                {"userName", user}
            };
            GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            return MakeString(result);
            
        }

        /// <summary>
        /// Tworzy stringa z listą nazw anime
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string MakeString(GraphQlQueryResponse result)
        {
            string userWatchAnimeList = "";
            dynamic gpl = result.GetData("MediaListCollection");
            if (gpl.lists.Count == 0)
                return "The list is empty";
            foreach (var item in gpl.lists[0].entries)
            {
                userWatchAnimeList = userWatchAnimeList + item.media.title.romaji.ToString() + '\n';
            }
            return userWatchAnimeList;
        }

        /// <summary>
        /// Pobiera ulubione gatunki anime
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetfavouredGenres(string user)
        {
            string str = "";
            string query = @"
              query($userName: String) { # Define which variables will be used in the query (id)
                  User(name: $userName) {
                    stats {
                        favouredGenresOverview {
                            genre
                            amount
                        }
                    }
                }
            }
            ";

            Dictionary<string, object> variables = new Dictionary<string, object>()
            {
                {"userName", user}
            };

            GraphQlQueryResponse result = obj.MakeRequestAsync(query, variables).Result;
            dynamic gpl = result.GetData("User");

            foreach (var item in gpl.stats.favouredGenresOverview)
            {
                str = str + "Genre: " + item.genre.ToString() + " Amount: " + item.amount.ToString() + "\n";
            }
            return str;
        }

    }
}
