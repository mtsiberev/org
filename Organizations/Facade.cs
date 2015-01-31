using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Facade
    {
        public static List<Employee> FindEmployeesByAge(Organization organization, int minAge, int maxAge)
        {
            List<Employee> result = new List<Employee>();
            foreach (var department in organization.GetAllDepartments())
            {
                foreach (var employee in department.GetAllEmployees())
                {
                    if (           
                        (employee.Age > minAge)
                        &&        
                        (employee.Age < maxAge)
                        )
                    {
                        result.Add(employee);
                    };
                }
            }
            return result;
        }

        public static List<Employee> FindEmployeesByAgeLinQ(Organization organization, int minAge, int maxAge)
        {
            var resultEmployees =
                from department in organization.GetAllDepartments()
                from employee in department.GetAllEmployees()
                where (employee.Age > minAge)
                where (employee.Age < maxAge)
                select employee;
            return resultEmployees.ToList();
        }
        
        public static List<Organization> FindOrganizationsByNameOfDepartmentWithPersonNumber(List<Organization> organizations, string departmentName, int numberOfPerson)
        {
            List<Organization> resultOrganizations = new List<Organization>();
            foreach (var organization in organizations)
            {
                foreach (var department in organization.GetAllDepartments())
                {
                    if (
                        (department.Name.Contains(departmentName)) &&  
                        (department.GetAllEmployees().Count() > numberOfPerson)

                        )
                    {
                        resultOrganizations.Add(organization);
                        break;
                    }
                }
            }
            return resultOrganizations;
        }

        public static Department FindDepartmentWithOldestPerson(Organization organization)
        {
            int maximumAge = 0;
          //  List<Department> departaments = new List<Department>();
            Department departamentWithOldestEmployee = new Department(-1);

            foreach (var department in organization.GetAllDepartments())
            {
                foreach (var employee in department.GetAllEmployees())
                {      
                    if (employee.Age > maximumAge)
                    {           
                        maximumAge = employee.Age;
                        departamentWithOldestEmployee = department;
                        continue;
                    }
                }
            }
            return departamentWithOldestEmployee;
        }

        public static List<Employee> FindEmployeesWithSubstring(Organization organization, string subString)
        {
            var resultEmployees =
                from department in organization.GetAllDepartments()
                from employee in department.GetAllEmployees()
                where employee.LastName.StartsWith(subString)
                select employee;
            return resultEmployees.ToList();
        }

               
        public static void FindAllEmployeesLivingOnTheSameStreet(List<Employee> employees)
        {
            var resultEmployees = employees.Select(e => new { e.address.City, e.FirstName, e.LastName }).OrderBy(e => e.City);

            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("  {0}", employee);
            }
        }

        public static void GetAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(Department department)
        {
            var groupedEmployees = department.GetAllEmployees().GroupBy(e => e.FirstName);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }

        public static void GetAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(Department department)
        {
            var resultEmployees = department.GetAllEmployees().Select(x => x.FirstName).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }        

    }

}
