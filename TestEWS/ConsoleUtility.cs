using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    public class ConsoleUtility
    {
        public static void WriteSeparatorLine()
        {
            Console.WriteLine(new string('*', 60));
        }
        public static void ReadAnyKey()
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        private static void WriteColoredMessage(ConsoleColor color, string message)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }
        public static void WriteMessage(string message)
        {
            WriteColoredMessage(ConsoleColor.White, message);
        }
        public static void WriteSuccessMessage(string message)
        {
            WriteColoredMessage(ConsoleColor.Green, message);
        }
        public static void WriteErrorMessage(string message)
        {
            WriteColoredMessage(ConsoleColor.Red, message);
        }
        public static void WriteWarningMessage(string message)
        {
            WriteColoredMessage(ConsoleColor.Yellow, message);
        }
    }
}
