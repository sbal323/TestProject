using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEWS
{
    public interface ITest
    {
        void Run();
        string Title { get; }
    }
}
