using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = textbylines();
            var mydic = new Dictionary<string, Dictionary<string, string>>();
            

        }

        public static string[] textbylines()
        {
            string[] lines = System.IO.File.ReadAllLines("movies.ini");
            return lines;
        }
    }
}
