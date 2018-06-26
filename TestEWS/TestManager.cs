using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    public class TestManager
    {
        private List<ITest> GetTest()
        {
            List<ITest> tests = new List<ITest>();
            //Register tests
            tests.Add(new Tests.LinqTest());
            //tests.Add(new Tests.WebTest());
            //tests.Add(new Tests.EWSTest());
            //tests.Add(new Tests.MessageCardTest());
            //tests.Add(new Tests.CorpSiteIntegrationTest());
            tests.Add(new Tests.CombineEmailTest());
            //tests.Add(new Tests.ScheduleSerializationTest());
            return tests;
        }
        public void RunTests()
        {
            foreach (ITest test in GetTest())
            {
                test.PrintTestName();
                test.Run();
                PauseTest();
            }
        }
        private void PauseTest()
        {
            Console.WriteLine("\nPress any key...\n");
            Console.ReadKey();
        }
    }
}
