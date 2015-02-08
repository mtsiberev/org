using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    /*


    class Display
    {
        public static void DisplayOrganization(Organization organization)
        {
            Console.WriteLine("Organization name: {0}   Id: {1}", organization.Name, organization.Id);
            foreach (var department in organization.GetAllDepartments() )
            {
                Console.WriteLine("Departament: Id: {0}   Name: {1}", department.Id, department.Name);
                foreach (var employee in department.GetAllEmployees() )
                {
                    Console.WriteLine("\tEmployee Id: {0}  FirstName: {1}  LastName: {2}  Date {3}  PersonId {4}",
                        employee.Id,
                        employee.FirstName,
                        employee.LastName,
                        employee.Age);                     
                }
                Console.WriteLine("\r\n");
            }
        }



        public static void ShowAllEmployeesLivingOnTheSameStreet(Department department)
        {
            var resultEmployees = department.GetAllEmployees().Select(e => new { e.address.City, e.FirstName, e.LastName }).OrderBy(e => e.City);

            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("  {0}", employee);
            }
        }



        public static void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(Department department)
        {
            var groupedEmployees = department.GetAllEmployees().GroupBy(e => e.FirstName);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }


        public static void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(Department department)
        {
            var resultEmployees = department.GetAllEmployees().Select(x => x.FirstName).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }

      
        
    }
    
     * 
     * */


}


