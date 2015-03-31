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
            var container = new RegisterByContainer().Container;
            var reports = container.GetInstance<Reports>();

            var reports2 = container.With(new Facade(null, null, null) ).GetInstance<Reports>();

            /*
            Console.WriteLine(container.WhatDoIHave() );
            
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




