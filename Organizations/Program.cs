using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Organizations
{

    class Program
    {
        private static void Main(string[] args)
        {
            var report = new Reports();
            report.ShowInitializedFacade();

            report.ShowEntityCode(new Organization(1));
            report.ShowAllOrganizations();
            report.ShowOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);
            
            report.ShowEntityCode(new Department(1, null));
            report.ShowAllDepartmentsInOrganization(1);
            report.ShowDepartmentWithOldestPerson();
            
            report.ShowEntityCode(new Employee(1, null));
            report.ShowAllEmployeesInOrganization(1);
            report.ShowAllEmployeesLivingOnTheSameStreet(1);
            report.ShowEmployeesWithSubstring(1, "ov");
            report.ShowEmployeesByAge(1, 20, 40);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);
          
            for (int i = 0; i < 5; i++)
                report.ShowRandomEmployee();
        }

    }
}




