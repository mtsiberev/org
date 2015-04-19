using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StructureMap;
using StructureMap.Configuration.DSL;


namespace Organizations
{

    class Program
    {
        private static void Main(string[] args)
        {
            var ado = new AdoHelper();
            ado.OpenSqlConnection();
            //ado.GetEmployeeById(1);
            /*
            var reports = RegisterByContainer.Container.GetInstance<Reports>();
          
            reports.ShowInitializedFacade();
            reports.ShowEntityCode(new Organization(1));
            reports.ShowAllOrganizations();
            reports.ShowOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);

            reports.ShowEntityCode(new Department(1, null));
            reports.ShowAllDepartmentsInOrganization(1);
            reports.ShowDepartmentWithOldestPerson();

            reports.ShowEntityCode(new Employee(1, null));
            reports.ShowAllEmployeesInOrganization(1);
            reports.ShowAllEmployeesOrederedByStreet(1);
            reports.ShowEmployeesWithSubstring(1, "ov");
            reports.ShowEmployeesByAge(1, 27, 38);
            reports.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);

            for (int i = 0; i < 5; i++)
                reports.ShowRandomEmployee();
            */
        }

    }
}




