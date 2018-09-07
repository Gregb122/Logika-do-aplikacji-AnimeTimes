using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static logikaAnimeTimes.AniListCommunication;

namespace logikaAnimeTimes.AbstractClasses
{
    /// <summary>
    /// Interfejs klas odpowiedzialnych za komunikowanie się z bazą danych
    /// </summary>
    abstract class Communication
    {

        /// <summary>
        /// Tworzy oraz wysyła zapytanie na serwer
        /// </summary>
        /// <param name="query"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public abstract Task<GraphQlQueryResponse> MakeRequestAsync(string query, Dictionary<string, object> variables);
    }
}
