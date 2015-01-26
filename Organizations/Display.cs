using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    class Display
    {
        public static void DisplayOrganization(Organization organization)
        {
            Console.WriteLine("Organization name: {0}   Id: {1}", organization.Name, organization.Id);
            foreach (var department in organization.departments)
            {
                Console.WriteLine("Departament: Id: {0}   Name: {1}", department.Id, department.Name);
                foreach (var employee in department.employees)
                {
                    Console.WriteLine("\tEmployee Id: {0}  FirstName: {1}  LastName: {2}  Date {3}  PersonId {4}",
                        employee.Id,
                        employee.FirstName,
                        employee.LastName,
                        employee.BirthDate);
                }
                Console.WriteLine("\r\n");
            }
        }


        public static void FindAllEmployeesLivingOnTheSameStreet(List<Employee> employees)
        {
            var resultEmployees = employees.Select(e => new { e.address.City, e.FirstName, e.LastName }).OrderBy(e => e.City);

            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("  {0}", employee);
            }
        }

        // get all unique First Names of employees in a specified department
        public static void GetAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(Department department)
        {
            var groupedEmployees = department.employees.GroupBy(e => e.FirstName);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }

        public static void GetAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(Department department)
        {
            var resultEmployees = department.employees.Select(x => x.FirstName).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
         
        }



    }

}


