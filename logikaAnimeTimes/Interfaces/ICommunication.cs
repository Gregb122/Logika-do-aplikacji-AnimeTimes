using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static logikaAnimeTimes.AniListCommunication;

namespace logikaAnimeTimes.Interfaces
{
    interface ICommunication
    {
        Task<GraphQlQueryResponse> MakeRequestAsync(string query, Dictionary<string, object> variables);
    }
}
