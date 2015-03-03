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
            report.ShowAllEmployeesInOrganization(1);
            report.ShowAllEmployeesLivingOnTheSameStreet(1);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(1);

            report.ShowEntityCode(new Organization(1) );
            report.ShowEntityCode(new Department(1, null) );
            report.ShowEntityCode(new Employee(1, null) );
        }
    }

}


