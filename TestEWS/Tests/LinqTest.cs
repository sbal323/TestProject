using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Tests
{
    class LinqTest: ITest
    {
        void ITest.Run()
        {
            List<Employee> employees = new List<Employee>();
            Department topDep = new Department { Id = 1, Name = "Top" };
            Department otherDep = new Department { Id = 1, Name = "Other" };
            employees.Add(new Employee() { Id = 1, EmployeeDepartment = topDep });
            employees.Add(new Employee() { Id = 2, EmployeeDepartment = topDep });

            employees.Where(x => x.Id == 2).ToList().ForEach(x => { x.EmployeeDepartment = otherDep; });

            Console.WriteLine("Test completed successfully!");
        }

        string ITest.Title
        {
            get { return "LINQ Test"; }
        }
    }
}
