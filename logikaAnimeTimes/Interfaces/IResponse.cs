using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logikaAnimeTimes.Interfaces
{
    interface IResponse
    {
        /// <summary>
        /// Pobiera czysty json
        /// </summary>
        /// <returns>string będący czystym kodem zwróconym przez serwer</returns>
        string GetRaw();

        /// <summary>
        /// pobiera błedy
        /// </summary>
        /// <returns>string z błędami</returns>
        string GetErrors();

        /// <summary>
        /// Deserializuje odpowiedź serwera
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetData<T>(string key);

        /// <summary>
        /// Deserializuje odpowiedź serwera - wersja dynamiczna
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        dynamic GetData(string key);
    }
}
