using System;
using System.Collections.Generic;

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
                    if (!Choose())
                        break;
                    else
                    {
                        Console.WriteLine("\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                    }
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
                "Get movies by genre",//Andris
                "Get longest movie",//pipa
                "Get total movies length",//pipa
                "Print movie info",//pipa
                "Print movies list"//Andris
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i + 1}). {options[i]}");

            Console.WriteLine("\n(0). Exit");
        }

        public static bool Choose()
        {
            Console.WriteLine("\nPlease enter a number: ");
            string option = Console.ReadLine();
            DataManager data = new DataManager();
            Dictionary<string, Dictionary<string, string>> table = data.Import_Data("movies.ini");
            if (option == "0")
            {
                System.Environment.Exit(-1);
                return false;
            }
            else if (option == "1")
            {
                Console.Clear();
                GetMoviesByGenre(table);
                return true;
            }
            else if (option == "2")
            {
                Console.Clear();
                Console.WriteLine(GetLongestMovie(table));
                return true;
            }
            else if (option == "3")
            {
                Console.Clear();
                Console.WriteLine(GetTotalMoviesLenght(table) + " mins");
                return true;
            }
            else if (option == "4")
            {
                Console.Clear();
                Console.WriteLine("Give me the title of the movie: ");
                string choosenTitle = Console.ReadLine();
                PrintChoosenData(table, choosenTitle);
                return true;
            }
            else if (option == "5")
            {
                Console.Clear();
                PrintAlbumsList(table);
                return true;
            }
            else
                throw new KeyNotFoundException();
        }

        public static void PrintAlbumsList(Dictionary<string, Dictionary<string, string>> table)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in table)
            {
                Console.WriteLine();
                Console.WriteLine(keyValuePair.Key);
                foreach (KeyValuePair<string, string> key2 in keyValuePair.Value)
                {
                    Console.WriteLine(key2.Key + " " + key2.Value);
                }
            }
        }

        public static string GetLongestMovie(Dictionary<string, Dictionary<string, string>> table)
        {
            int maxmins = 0;
            string maxlenghtfilm = "";
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in table)
            {
                foreach (KeyValuePair<string, string> key2 in keyValuePair.Value)
                {
                    if (key2.Key == "length")
                    {
                        string[] keyparts = key2.Value.Split(':');
                        int mins = Convert.ToInt32(keyparts[0]) * 60 + Convert.ToInt32(keyparts[1]);
                        if (mins > maxmins)
                        {
                            maxmins = mins;
                            maxlenghtfilm = keyValuePair.Key;
                        }
                    }
                }
            }
            return maxlenghtfilm;
        }

        public static int GetTotalMoviesLenght(Dictionary<string, Dictionary<string, string>> table)
        {
            int totalmins = 0;
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in table)
            {
                foreach (KeyValuePair<string, string> key2 in keyValuePair.Value)
                {
                    if (key2.Key == "length")
                    {
                        string[] keyparts = key2.Value.Split(':');
                        int mins = Convert.ToInt32(keyparts[0]) * 60 + Convert.ToInt32(keyparts[1]);
                        totalmins = totalmins + mins;
                    }
                }
            }
            return totalmins;
        }

        public static void PrintChoosenData(Dictionary<string, Dictionary<string, string>> table, string choosenTitle)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in table)
            {
                if (keyValuePair.Key == choosenTitle)
                {
                    Console.WriteLine(keyValuePair.Key);
                    foreach (KeyValuePair<string, string> key2 in keyValuePair.Value)
                    {
                        Console.WriteLine(key2.Key + "=" + key2.Value);
                    }
                }
            }
        }

        public static void GetMoviesByGenre(Dictionary<string, Dictionary<string, string>> table)
        {
            Console.WriteLine("Enter a genre: ");
            string input = Console.ReadLine();

            foreach (var key in table)
            {
                foreach (var key2 in key.Value)
                {
                    if (key2.Value.Equals(input))
                        Console.WriteLine(key.Key);
                }
            }
        }
    }
}

