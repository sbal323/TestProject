using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEWS
{
    public class TestManager
    {
        private List<ITest> GetTest()
        {
            List<ITest> tests = new List<ITest>();
            //Register tests
            //tests.Add(new Tests.LinqTest());
            //tests.Add(new Tests.WebTest());
            //tests.Add(new Tests.EWSTest());
            //tests.Add(new Tests.MessageCardTest());
            tests.Add(new Tests.CorpSiteIntegrationTest());
            return tests;
        }
        public void RunTests()
        {
            foreach (ITest test in GetTest())
            {
                Console.WriteLine(new String('*', 45));
                Console.WriteLine(String.Format("  {0}", test.Title));
                Console.WriteLine(new String('*', 45));
                test.Run();
                Console.WriteLine("\nPress any key...\n");
                Console.ReadKey();
            }
        }
    }
}
