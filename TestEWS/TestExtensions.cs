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
            Console.WriteLine(new String('*', 45));
            Console.WriteLine(String.Format("{0}{1}", new String(' ', 15), test.Title));
            Console.WriteLine(new String('*', 45));
        }
    }
}
