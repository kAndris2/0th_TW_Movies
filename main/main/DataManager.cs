using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class DataManager
    {
        public Dictionary<string, Dictionary<string, string>> Import_Data(String filename)
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
            return mydict;
        }

        public void Export_Data(String filename, Dictionary<string, Dictionary<string, string>> table)
        {
            string text = "";

            foreach (KeyValuePair<string, Dictionary<string, string>> key in table)
            {
                text += "\n" + key.Key + "\n";
                foreach (KeyValuePair<string, string> key2 in key.Value)
                {
                    text += key2.Key + "=" + key2.Value + "\n";
                }
            }
            System.IO.File.WriteAllText(filename, text);
        }
    }
}
