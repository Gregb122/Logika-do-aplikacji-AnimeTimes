using logikaAnimeTimes.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logikaAnimeTimes
{
    /// <summary>
    /// Klasa zawierajaca odpowiedż serwera AniList
    /// </summary>
    class GraphQlQueryResponse : IResponse
    {
        private readonly string raw;
        private readonly JObject data;
        private readonly string errors;

        /// <summary>
        /// Konstruktor przypisujący odpowiednie dane do odpowiednich pól
        /// </summary>
        /// <param name="raw"></param>
        public GraphQlQueryResponse(string raw)
        {
            this.raw = raw;
            data = raw != null ? JObject.Parse(raw) : null;
            errors = data["errors"]?.ToString();
        }
        /// <summary>
        /// Zwraca czysty Json odpoweidzi serwera AniList
        /// </summary>
        /// <returns></returns>
        public string GetRaw()
        {
            return raw;
        }

        /// <summary>
        /// Pobiera błedy
        /// </summary>
        /// <returns></returns>
        public string GetErrors()
        {
            return errors;
        }

        /// <summary>
        /// Deserializuje pobranego jsona
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deserializuje pobranego jsona - dynamicznie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
}
