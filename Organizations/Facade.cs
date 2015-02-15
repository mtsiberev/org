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

        public void Add(IEntity entity)
        {
            if (entity is Organization)
            {
                organizations.Insert(entity as Organization);
            }
            else if (entity is Department)
            {
                departments.Insert(entity as Department);
            }
            else if (entity is Employee)
            {
                employees.Insert(entity as Employee);
            }
        }

        public Organization GetOrganization(string name)
        {
            return organizations.GetByName(name);
        }

        public Organization GetOrganization(Guid id)
        {
            return organizations.GetById(id);
        }


        public Department GetDepartment(string name)
        {
            return departments.GetByName(name);
        }

        public Department GetDepartment(Guid id)
        {
            return departments.GetById(id);
        }


        public Employee GetEmployee(string name)
        {
            return employees.GetByName(name);
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.GetById(id);
        }

        public List<Employee> FindEmployeesByAgeLinQ(Guid organizationId, int minAge, int maxAge)
        {
            var resultEmployees =
                from employee in employees.GetAll()
                where (departments.GetById(employee.ParentId).ParentId == organizationId)
                where (employee.Age > minAge)
                where (employee.Age < maxAge)
                select employee;
            return resultEmployees.ToList();
        }

        public List<Organization> FindOrganizationsByNameOfDepartmentWithPersonNumber(string departmentName,
            int numberOfPerson)
        {
            var resultOrganizations =
                from department in departments.GetAll()
                where department.Name == departmentName
                let countEmployees = employees.GetAll().Count(employee => employee.ParentId == department.Id)
                where countEmployees >= numberOfPerson
                select organizations.GetById(department.ParentId);
            return resultOrganizations.ToList();
        }

        public Department FindDepartmentWithOldestPerson(Organization organization)
        {
            return departments.GetById(
                employees.GetAll().First(x => x.Age == employees.GetAll().Max(y => y.Age))
                .ParentId);
        }

        public List<Employee> FindEmployeesWithSubstring(Guid organizationId, string subString)
        {
            var resultEmployees =
                from employee in employees.GetAll()
                where (departments.GetById(employee.ParentId).ParentId == organizationId)
                where (employee.LastName.Contains(subString))
                select employee;
            return resultEmployees.ToList();
        }
    }

}
