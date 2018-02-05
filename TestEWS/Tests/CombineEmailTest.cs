using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TestSuite.Tests
{
    class CombineEmailTest : ITest
    {
        Dictionary<string, string> frenchSymbolReplacement = new Dictionary<string, string>()
        {
            {"À","A"},
            {"Á","A"},
            {"Â","A"},
            {"Ã","A"},
            {"Ä","A"},
            {"Å","A"},
            {"Ç","C"},
            {"È","E"},
            {"É","E"},
            {"Ê","E"},
            {"Ë","E"},
            {"Ì","I"},
            {"Í","I"},
            {"Î","I"},
            {"Ï","I"},
            {"Ñ","N"},
            {"Ò","O"},
            {"Ó","O"},
            {"Ô","O"},
            {"Õ","O"},
            {"Ö","O"},
            {"Ø","O"},
            {"Ù","U"},
            {"Ú","U"},
            {"Û","U"},
            {"Ü","U"},
            {"Ý","Y"},
            {"ß","S"},
            {"à","a"},
            {"á","a"},
            {"â","a"},
            {"ã","a"},
            {"ä","a"},
            {"å","a"},
            {"ç","c"},
            {"è","e"},
            {"é","e"},
            {"ê","e"},
            {"ë","e"},
            {"ì","i"},
            {"í","i"},
            {"î","i"},
            {"ï","i"},
            {"ñ","n"},
            {"ò","o"},
            {"ó","o"},
            {"ô","o"},
            {"õ","o"},
            {"ö","o"},
            {"ø","o"},
            {"ù","u"},
            {"ú","u"},
            {"û","u"},
            {"ü","u"},
            {"ý","y"},
            {"ÿ","y"},
            {"Š","S"},
            {"š","s"},
            {"Ÿ","Y"},
            {"Ž","Z"},
            {"ž","z"},
            {"ƒ","f"}
        };
    void ITest.Run()
        {
            Dictionary<string, string> employees = new Dictionary<string, string>();
            employees.Add("Jean-Pierre", "Bélanger");
            employees.Add("Iain W.", "Croft's");
            employees.Add("ÑØã", "Êžñÿ");
            foreach (var empl in employees)
            {
                ConsoleUtility.WriteMessage($"E-mail address: {CombineEmail(empl.Key, empl.Value)}");
            }

        }
        public string CombineEmail(string firstName, string lastName)
        {
            return (CleanSpecialSymbols(firstName) + "." + CleanSpecialSymbols(lastName) + "@test.ca").Replace("..", ".");
        }

        private string CleanSpecialSymbols(string text)
        {
            string result;
            Regex patternEmpty = new Regex("[ \\-']");
            result = patternEmpty.Replace(text, string.Empty);
            return frenchSymbolReplacement.Aggregate(result, (current, value) => current.Replace(value.Key, value.Value));
        }

        string ITest.Title
        {
            get { return "Combine Email Test"; }
        }
    }
}
