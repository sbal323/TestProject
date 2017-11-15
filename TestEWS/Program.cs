using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestSuite
{
    class Program
    {
        static void Main(string[] args)
        {
            TestManager testManager = new TestManager();
            testManager.RunTests();
        }
    }
}
