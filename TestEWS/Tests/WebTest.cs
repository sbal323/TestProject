using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestSuite;

namespace Tests
{
    class WebTest : ITest
    {
        void ITest.Run()
        {
            Console.WriteLine("Notify('" + HttpUtility.JavaScriptStringEncode("Some's text from Test's test") + "');");
        }


        string ITest.Title
        {
            get { return "Web helpers Test"; }
        }
    }
}
