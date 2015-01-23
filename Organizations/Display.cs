using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    class Display
    {
        public static void ShowAll(Organization organization)
        {
            Console.WriteLine("Organization name: {0}   Id: {1}", organization.Name, organization.Id);
            foreach (var department in organization.departments)
            {
                Console.WriteLine("Departament: Id: {0}   Name: {1}", department.Id, department.Name);
                foreach (var employee in department.employees)
                {
                    Console.WriteLine("\tEmployee Id: {0}  Name: {1}  Age {2}  PersonId {3}",
                        employee.Id,
                        employee.Name,
                        employee.Age,
                        employee.GetPersonId() );
                }
                Console.WriteLine("\r\n");
            }
        }




    }

}
