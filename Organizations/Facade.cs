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

        public void AddOrganization(Organization entity)
        {                
            organizations.Insert(entity);            
        }

        public void AddDepartment(Department entity)
        {
            departments.Insert(entity);
        }

        public void AddEmployee(Employee entity)
        {
            employees.Insert(entity);
        }
                      
        public Organization GetOrganizationbyId(int id)
        {
            return organizations.GetById(id);
        }

        public Department GetDepartmentById(int id)
        {
            return departments.GetById(id);
        }

        public Employee GetEmployeeById(int id)
        {
            return employees.GetById(id);
        }

        public IEnumerable<Organization> GetAllOrganizations()
        { 
            return organizations.GetAll(); 
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return departments.GetAll();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees.GetAll();
        }



        public void Init()
        {
            organizations.Insert(new Organization(1) { Name = "FirstLine" });
            departments.Insert(new Department(1, this.GetOrganizationbyId(1)) { Name = "IT department" });
            departments.Insert(new Department(2, this.GetOrganizationbyId(1)) { Name = "HR department" });

            employees.Insert(new Employee(1, this.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            employees.Insert(new Employee(2, this.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            employees.Insert(new Employee(3, this.GetDepartmentById(1)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });

            employees.Insert(new Employee(4, this.GetDepartmentById(2)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            employees.Insert(new Employee(5, this.GetDepartmentById(2)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            employees.Insert(new Employee(6, this.GetDepartmentById(2)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });
        }
        
        public List<Employee> FindEmployeesByAgeLinQ(int organizationId, int minAge, int maxAge)
        {
            var resultEmployees =
                from employee in employees.GetAll()
                where (departments.GetById(employee.ParentEntity.Id).ParentEntity.Id == organizationId)
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
                let countEmployees = employees.GetAll().Count(employee => employee.ParentEntity.Id == department.Id)
                where countEmployees >= numberOfPerson
                select organizations.GetById(department.ParentEntity.Id);
            return resultOrganizations.ToList();
        }               

        public Department FindDepartmentWithOldestPerson()
        {
            return departments.GetById(
                employees.GetAll().First(x => x.Age == employees.GetAll().Max(y => y.Age))
                .ParentEntity.Id);
        }
        
        public List<Employee> FindEmployeesWithSubstring(int organizationId, string subString)
        {
            var resultEmployees =
                from employee in employees.GetAll()
                where (departments.GetById(employee.ParentEntity.Id).ParentEntity.Id == organizationId)
                where (employee.LastName.Contains(subString))
                select employee;
            return resultEmployees.ToList();
        }
    }

}
