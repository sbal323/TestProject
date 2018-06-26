using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    public static class TestExtensions
    {
        public static void PrintTestName(this ITest test)
        {
            ConsoleUtility.WriteSeparatorLine();
            ConsoleUtility.WriteMessage(String.Format("{0}{1}", new String(' ', 25), test.Title));
            ConsoleUtility.WriteSeparatorLine();
        }
    }
}
