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
