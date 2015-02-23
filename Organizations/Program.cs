using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Organizations
{

    class Program
    {
        static void Main(string[] args)
        {
            Reports report = new Reports();
            report.DisplayOrganization(1);
            report.ShowAllEmployeesLivingOnTheSameStreet(1);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(1);     
        }
    }

}


