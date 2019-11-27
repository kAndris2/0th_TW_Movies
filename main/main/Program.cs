using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            while (true)
            {
                HandleMenu();
                try
                {
                    Choose();
                }
                catch (KeyNotFoundException)
                {
                    Console.Clear();
                    Console.WriteLine("[ERROR]: There is no such option!\n");
                }
            } 
        }

        public static void HandleMenu()
        {
            List<string> options = new List<string>
            {
                "Get albums by genre",
                "Get longest album",
                "Get total albums length",
                "Print album info",
                "Print movies list",
                "Export data"
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i + 1}). {options[i]}");

            Console.WriteLine("\n(0). Exit");
        }

        public static void Choose()
        {
            string option = Console.ReadLine();
            DataManager data = new DataManager();
            Dictionary<string, Dictionary<string, string>> table = data.Import_Data("movies.ini");
            if (option == "0")
                System.Environment.Exit(-1);
            else if (option == "1")
                Console.WriteLine("First menu");
            else if (option == "5")
            {
                Console.Clear();
                PrintAlbumsList(table);
            }
            else
                throw new KeyNotFoundException();
        }

        public static void PrintAlbumsList(Dictionary<string, Dictionary<string, string>> table)
        {
            foreach (var keyValuePair in table)
            {
                Console.WriteLine();
                Console.WriteLine(keyValuePair.Key);
                foreach (var key2 in keyValuePair.Value)
                {
                    Console.WriteLine(key2.Key + " " + key2.Value);
                }
            }
        }
    }
}

