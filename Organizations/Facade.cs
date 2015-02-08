using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Facade
    {
        private Repository<Organization> organizations;
        private Repository<Department> departments;
        private Repository<Employee> employees;

        public Facade()
        {
            organizations = new Repository<Organization>();
            departments = new Repository<Department>();
            employees = new Repository<Employee>();
        }
        
        public void AddOrganization(Organization organization)
        {
            organizations.Insert(organization);
        }

        public void AddDepartment(Department department)
        {
            departments.Insert(department);
        }

        public void AddEmployee(Employee employee)
        {
            employees.Insert(employee);
        }

        public int GetId()
        {
            return employees.GetNewEntityId();
        }
        

        //-----------------------------------------------------------        
        /*

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
            
        */
    }

}
