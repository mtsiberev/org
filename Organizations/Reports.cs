using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Reports
    {
        public Reports()
        { 
            facade = new Facade(); 
            facade.Init();
        }

        private Facade facade;
        public  void DisplayOrganization(int organizationId)
        {
            foreach (var employee in facade.GetAllEmployees())
            {
                if (facade.GetDepartmentById(employee.ParentEntity.Id).ParentEntity.Id != organizationId)
                    continue;
                Console.WriteLine("\tEmployee Id: {0}  FirstName: {1}  LastName: {2}  Date {3}",
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Age
                    );
            }
        }
                                                       
           
        public void ShowAllEmployeesLivingOnTheSameStreet(int departmentId)
        {
            var employeesInDepartment = facade.GetAllEmployees().ToList().FindAll(e => e.ParentEntity.Id == departmentId);
            var resultEmployees = employeesInDepartment.Select(e => new { e.Address.Street, e.Name, e.LastName }).OrderBy(e => e.Street);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("  {0}", employee);
            }
        }
        
        
        public  void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(int departmentId)
        {
            var employeesInDepartment = facade.GetAllEmployees().ToList().FindAll(e => e.ParentEntity.Id == departmentId);
            var groupedEmployees = employeesInDepartment.GroupBy(e => e.Name);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }


        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(int departmentId)
        {
            var employeesInDepartment = facade.GetAllEmployees().ToList().FindAll(e => e.ParentEntity.Id == departmentId);
            var resultEmployees = employeesInDepartment.Select(x => x.Name).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }
           
        
    }
    
}


