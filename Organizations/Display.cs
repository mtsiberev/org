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
        
    }

}


