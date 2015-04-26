using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Organizations.DbEntity;
using StructureMap;
using StructureMap.Configuration.DSL;


namespace Organizations
{
    class Program
    {
        private static void Main(string[] args)
        {
            var emplsDb = new RepositoryDb<EmployeeDb>();
            var emp1 = emplsDb.GetById(1);
            Console.WriteLine("Employee Id: {0} Name: {1}", emp1.Id, emp1.Name);
            emp1.Name = "Jones";
            emplsDb.Insert(emp1);
            emplsDb.Delete(emp1);

            Console.WriteLine("\nList after deleting and inserting:");
            var list = emplsDb.GetAll();
            foreach (var emp in list)
            {
                Console.WriteLine("Employee Id: {0} Name: {1}", emp.Id, emp.Name);
            }
            
            /*
            var repDep = new RepositoryDb<DepartmentDb>();
            var depById = repDep.GetById(1);
            if (depById != null)
            {
                Console.WriteLine(depById.Id);
                Console.WriteLine(depById.Name);
            }

            var list2 = repDep.GetAll();
            foreach (var dep in list2)
            {
                Console.WriteLine(dep.Id);
                Console.WriteLine(dep.ParentOrganization);
                Console.WriteLine(dep.Name);
            }
            */

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




