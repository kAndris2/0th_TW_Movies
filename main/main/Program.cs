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
            data.Import_Data("movies.ini");
            //String[] table = data.Import_Data("movies.ini");
            if (option == "0")
                System.Environment.Exit(-1);
            else if (option == "1")
                Console.WriteLine("First menu");
            else if (option == "5")
            {
                Console.Clear();
                //PrintAlbumsList(table);
            }
            else
                throw new KeyNotFoundException();
        }

        public static void PrintAlbumsList(String[] table)
        {
            for (int i = 0; i < table.Length; i++)
                Console.WriteLine(table[i]);
        }

        
    
    }

    class DataManager
    {
        public Dictionary<string, Dictionary<string,string>> Import_Data(String filename)
        {
            String[] table = System.IO.File.ReadAllLines(filename);
            var mydict = new Dictionary<string, Dictionary<string, string>>();
            string lastMovieTitle = "";
            foreach (string lines in table)
            {
                if (lines.Contains("["))
                {
                    mydict[lines] = new Dictionary<string, string>();
                    lastMovieTitle = lines;
                }
                if (lines.Contains("="))
                {
                    string[] parts = lines.Split('=');
                    mydict[lastMovieTitle][parts[0]] = parts[1];
                }
            }

            foreach(var keyValuePair in mydict)
            {
                Console.WriteLine(keyValuePair.Key + keyValuePair.Value + keyValuePair.Value.Values);
            }
            return null;
        }
    }
}

