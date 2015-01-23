using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Reports
    {
        public static List<Employee> FindEmployeesByAge(Organization organization, int minAge, int maxAge)
        {
            List<Employee> result = new List<Employee>();
            foreach (var department in organization.departments)
            {
                foreach (var employee in department.employees)
                {
                    if ((employee.Age > minAge) && (employee.Age < maxAge))
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
                where employee.Age > minAge
                where employee.Age < maxAge
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
                from department in organization.departments
                from employee in department.employees
                where employee.Name.StartsWith(subString)
                select employee;
            return resultEmployees.ToList();
        }

        //вспомогательный метод. сообщает содержится ли сотрудник в отделе, отличном от передаваемого в аргументе
        //позволяет проверить числится ли сотрудник в более чем одном отделе
        static bool IsContainedInSeveralDepartaments(Organization organization, Employee findEmployee, Department departmentWithEmployee)
        {
            foreach (var department in organization.departments)
            {
                foreach (var employee in department.employees)
                {
                    if (
                        (findEmployee.GetPersonId() == employee.GetPersonId()) &&
                        (department.Id != departmentWithEmployee.Id)
                        )
                        return true;
                }
            }
            return false;
        }

        public static List<Employee> FindEmployeesWorkingInSeveralDepartments(Organization organization)
        {
            var resultEmplyees =
                from department in organization.departments
                from employee in department.employees
                where Reports.IsContainedInSeveralDepartaments(organization, employee, department)
                select employee;
            return resultEmplyees.ToList();
        }
        
        public static void FindAllEmployeesLivingOnTheSameStreet(List<Employee> employees)
        {
            var resultEmployees = employees.Select(e => new { e.address.City, e.Name }).OrderBy(e => e.City);

            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("  {0}", employee);
            }
        }
    }

}
