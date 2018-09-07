using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logikaAnimeTimes
{
    /// <summary>
    /// Odpowiada za to co sie wyswietla w konsoli
    /// </summary>
    static class Display
    {
        /// <summary>
        /// Wyświetla podstawowe informacje
        /// </summary>
        public static void DisplayBasicInfo()
        {
            Console.WriteLine("User name: ");

            string user = Console.ReadLine();

            DataHandler dataHandler = new DataHandler();

            Console.WriteLine();
            Console.Write("Watching list: \n\n");
            Console.Write(dataHandler.GetUserWatchAnimeList(user));
            Console.WriteLine();
            Console.Write("Completed list: \n\n");
            Console.Write(dataHandler.GetUserCompleteAnimeList(user));
            Console.WriteLine();
            Console.Write("favouredGenres: \n\n");
            Console.Write(dataHandler.GetfavouredGenres(user));
        }
    }
}
