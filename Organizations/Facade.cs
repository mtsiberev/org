using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Organizations
{
    public class Facade
    {        
        private readonly IRepository<Organization> m_organizationsRepository;
        private readonly IRepository<Department> m_departmentsRepository;
        private readonly IRepository<Employee> m_employeesRepository;

        [DefaultConstructor]
        public Facade(IRepository<Organization> organizations, IRepository<Department> departments, IRepository<Employee> employees)
        {
            m_organizationsRepository = organizations;
            m_departmentsRepository = departments;
            m_employeesRepository = employees;
        }
       
        public void AddOrganization(Organization entity)
        {
            m_organizationsRepository.Insert(entity);            
        }

        public void AddDepartment(Department entity)
        {
            m_departmentsRepository.Insert(entity);
        }

        public void AddEmployee(Employee entity)
        {
            m_employeesRepository.Insert(entity);
        }
                      
        public Organization GetOrganizationById(int id)
        {
            return m_organizationsRepository.GetById(id);
        }

        public Department GetDepartmentById(int id)
        {
            return m_departmentsRepository.GetById(id);
        }

        public Employee GetEmployeeById(int id)
        {
            return m_employeesRepository.GetById(id);
        }

        public List<Organization> GetAllOrganizations()
        {
            return m_organizationsRepository.GetAll().ToList(); 
        }

        public List<Department> GetAllDepartments()
        {
            return m_departmentsRepository.GetAll().ToList();
        }

        public List<Employee> GetAllEmployees()
        {
            return m_employeesRepository.GetAll().ToList();
        }

        public IEntity GetRandomByEntityCode(int entityCode) 
        {
            switch (entityCode)
            {
                case 0:
                    return m_organizationsRepository.GetRandom();

                case 1:
                    return m_departmentsRepository.GetRandom();

                case 2:
                    return m_employeesRepository.GetRandom();
                 
                default:
                    return null;
            }
        }

        public Employee GetRandomEmployee()
        {
            return m_employeesRepository.GetRandom();
        }

        public void Init()
        {
            m_organizationsRepository.Insert(new Organization(1) { Name = "FirstLine" });
            m_departmentsRepository.Insert(new Department(1, this.GetOrganizationById(1)) { Name = "IT department" });
            m_departmentsRepository.Insert(new Department(2, this.GetOrganizationById(1)) { Name = "HR department" });

            m_employeesRepository.Insert(new Employee(1, this.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            m_employeesRepository.Insert(new Employee(2, this.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            m_employeesRepository.Insert(new Employee(3, this.GetDepartmentById(1)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });

            m_employeesRepository.Insert(new Employee(4, this.GetDepartmentById(2)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            m_employeesRepository.Insert(new Employee(5, this.GetDepartmentById(2)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            m_employeesRepository.Insert(new Employee(6, this.GetDepartmentById(2)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });
        }
        
        public List<Employee> GetEmployeesInDepartment(int departmentId)
        {
            return GetAllEmployees().ToList().
                FindAll(e => e.ParentDepartment.Id == departmentId);
        }
        
        public List<Employee> OrderEmployeesByStreet(int departmentId)
        {
            var employeesInDepartment = GetEmployeesInDepartment(departmentId);
            return employeesInDepartment.OrderBy(e => e.Address.Street).ToList();
        }
                        
        public List<Employee> FindEmployeesByAge(int organizationId, int minAge, int maxAge)
        {
            var resultEmployees =
                from employee in m_employeesRepository.GetAll()
                where (employee.ParentDepartment.ParentOrganization.Id == organizationId)
                where (employee.Age >= minAge)
                where (employee.Age <= maxAge)
                select employee;
            return resultEmployees.ToList();
        }
               
        public List<Organization> FindOrganizationsByNameOfDepartmentWithPersonNumber(string departmentName, int numberOfPerson)
        {
            var resultOrganizations =
                from department in m_departmentsRepository.GetAll()
                where department.Name == departmentName
                let countEmployees =
                    m_employeesRepository.GetAll().Count(employee => employee.ParentDepartment.Id == department.Id)
                where countEmployees >= numberOfPerson
                select department.ParentOrganization;
            return resultOrganizations.ToList();
        }               

        public Department FindDepartmentWithOldestPerson()
        {
            var resultEmployee = m_employeesRepository.GetAll().First();
            foreach (var employee in m_employeesRepository.GetAll().
                Where(employee => resultEmployee.Age < employee.Age))
            {
                resultEmployee = employee;
            }
            return resultEmployee.ParentDepartment;
        }
        
        public List<Employee> FindEmployeesWithSubstring(int organizationId, string subString)
        {
            var resultEmployees =
               from employee in m_employeesRepository.GetAll()
               where (employee.ParentDepartment.ParentOrganization.Id == organizationId)
               where (employee.LastName.Contains(subString))
               select employee;
            return resultEmployees.ToList();
        }

    }

}
