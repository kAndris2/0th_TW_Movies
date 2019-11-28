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
                catch (KeyNotFoundException e)
                {
                    Console.Clear();
                    Console.WriteLine("[ERROR]: " + e.Message);
                }
            }
        }

        public static void HandleMenu()
        {
            List<string> options = new List<string>
            {
                "Get movies by genre",
                "Get longest movie",
                "Get total movies length",
                "Print movie info",
                "Print movies list",
                "Add movie",
                "Remove movie",
                "Update movie"
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i + 1}). {options[i]}");

            Console.WriteLine("\n(0). Exit");
        }

        public static bool Choose()
        {
            Console.WriteLine("\nPlease enter a number: ");
            string option = Console.ReadLine();
            string filename = "movies.ini";

            DataManager data = new DataManager();
            Dictionary<string, Dictionary<string, string>> table = data.Import_Data(filename);

            UI ui = new UI();

            if (option == "0")
            {
                System.Environment.Exit(-1);
                return false;
            }
            else if (option == "1")
            {
                Console.Clear();
                Console.WriteLine("Give me a genre:");
                string input = Console.ReadLine();
                ui.UIP($"Movies by {input}", GetMoviesByGenre(table, input));
                return true;
            }
            else if (option == "2")
            {
                Console.Clear();
                ui.UIP("Longest Movie",GetLongestMovie(table));
                return true;
            }
            else if (option == "3")
            {
                Console.Clear();
                ui.UIP("Total movies length",GetTotalMoviesLenght(table) + " mins");
                return true;
            }
            else if (option == "4")
            {
                Console.Clear();
                Console.WriteLine("Give me the title of the movie: ");
                string choosenTitle = Console.ReadLine();
                ui.UIP("The movie by your title", PrintChoosenData(table, choosenTitle));
                return true;
            }
            else if (option == "5")
            {
                Console.Clear();
                ui.UIP("Movie list", table);
                return true;
            }
            else if (option == "6")
            {
                Console.Clear();
                data.Export_Data(filename, AddNewMovie(table));
                return true;
            }
            else if (option == "7")
            {
                Console.Clear();
                Console.WriteLine("Give me the name of movie to delete:");
                string movieToDelete = Console.ReadLine();
                data.Export_Data(filename, DeleteMovie(table, movieToDelete));
                return true;
            }
            else if (option == "8")
            {
                Console.Clear();
                data.Export_Data(filename, UpdateMovie(table));
                return true;
            }
            else
                throw new KeyNotFoundException($"There is no such option! ({option})\n");
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

        public static Dictionary<string, Dictionary<string, string>> PrintChoosenData(Dictionary<string, Dictionary<string, string>> table, string choosenTitle)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var mydict = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in table)
            {
                string newkey = keyValuePair.Key;
                newkey = newkey.Replace("[", string.Empty);
                newkey = newkey.Replace("]", string.Empty);
                if (newkey == choosenTitle)
                {
                    foreach (var key2 in keyValuePair.Value)
                    {
                        dic.Add(key2.Key, key2.Value);
                        string key = keyValuePair.Key;
                        key = key.Replace("[", string.Empty);
                        key = key.Replace("]", string.Empty);
                    }
                    mydict.Add(keyValuePair.Key, dic);
                }
            }
            return mydict;
        }

        public static List<string> GetMoviesByGenre(Dictionary<string, Dictionary<string, string>> table, string input)
        {
            List<string> values = new List<string>();
            foreach (var key in table)
            {
                foreach (var key2 in key.Value)
                {
                    if (key2.Value.Equals(input))
                    {
                        string keys = key.Key;
                        keys = keys.Replace("[", string.Empty);
                        keys = keys.Replace("]", string.Empty);
                        values.Add(keys);
                    }
                }
            }
            return values;
        }

        public static Dictionary<string, Dictionary<string, string>> AddNewMovie(Dictionary<string, Dictionary<string, string>> table)
        {
            Console.WriteLine("Enter a title: ");
            string title = Console.ReadLine();
            title = "[" + title + "]";

            var inside = new Dictionary<string, string>();
            string[] options = new string[6] { "director", 
                                                "release year", 
                                                "stars",
                                                "budget",
                                                "length",
                                                "genre" 
                                                };

            foreach (string item in options)
            {
                Console.WriteLine($"Enter the {item}");
                inside.Add(item, Console.ReadLine());
            }
            table.Add(title, inside);

            return table;
        }

        public static Dictionary<string, Dictionary<string, string>> UpdateMovie(Dictionary<string, Dictionary<string, string>> table)
        {
            Console.WriteLine("Enter the movie title: ");
            string title = Console.ReadLine();
            title = "[" + title + "]";

            Console.WriteLine("Enter the movie property: ");
            string property = Console.ReadLine();

            Console.WriteLine($"Enter the new {property}: ");
            table[title][property] = Console.ReadLine();

            return table;
        }
        public static Dictionary<string, Dictionary<string, string>> DeleteMovie(Dictionary<string, Dictionary<string, string>> table, string movieToDelete)
        {
            movieToDelete = "[" + movieToDelete + "]";
            table.Remove(movieToDelete);
            return table;
        }

    }
}

