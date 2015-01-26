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
            foreach (var department in organization.departments)
            {
                foreach (var employee in department.employees)
                {
                    if (
                        ((DateTime.Now.Year - employee.BirthDate.Year) > minAge)
                        &&
                        ((DateTime.Now.Year - employee.BirthDate.Year) < maxAge)
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
                from department in organization.departments
                from employee in department.employees
                where (DateTime.Now.Year - employee.BirthDate.Year) > minAge
                where (DateTime.Now.Year - employee.BirthDate.Year) < maxAge
                select employee;
            return resultEmployees.ToList();
        }


        public static List<Organization> FindOrganizationsByNameOfDepartmentWithPersonNumber(List<Organization> organizations, string departmentName, int numberOfPerson)
        {
            List<Organization> resultOrganizations = new List<Organization>();
            foreach (var organization in organizations)
            {
                foreach (var department in organization.departments)
                {
                    if (
                        (department.Name.Contains(departmentName)) &&
                        (department.employees.Count() > numberOfPerson)
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
            List<Department> departaments = new List<Department>();
            Department departamentWithOldestEmployee = new Department(-1);

            foreach (var department in organization.departments)
            {
                foreach (var employee in department.employees)
                {
                    if ((DateTime.Now.Year - employee.BirthDate.Year) > maximumAge)
                    {
                        maximumAge = (DateTime.Now.Year - employee.BirthDate.Year);
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
                from department in organization.departments
                from employee in department.employees
                where employee.LastName.StartsWith(subString)
                select employee;
            return resultEmployees.ToList();
        }

    }

}
