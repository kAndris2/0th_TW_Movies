using System;
using System.Collections.Generic;

namespace main
{
    public class UI
    {
        public UI(string message, List<string> result)
        {
            Console.WriteLine($"{message}:\n");
            for (int i = 0; i < result.Count; i++)
                Console.WriteLine(result[i]);
        }

        public UI(string message, Dictionary<string, Dictionary<string, string>> result)
        {
            Console.WriteLine($"{message}:\n");
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in result)
            {
                Console.WriteLine();
                Console.WriteLine(keyValuePair.Key);

                foreach (KeyValuePair<string, string> key2 in keyValuePair.Value)
                    Console.WriteLine(key2.Key + ": " + key2.Value);
            }
        }

        public UI(string message, string result)
        {
            Console.WriteLine($"{message}:\n");
            Console.WriteLine(result);
        }

        public UI(string message, int result)
        {
            Console.WriteLine($"{message}:\n");
            Console.WriteLine(result);
        }
    }
}
