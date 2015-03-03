using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Facade
    {        
        private IRepository<Organization> _organizationsRepository;
        private IRepository<Department> _departmentsRepository;
        private IRepository<Employee> _employeesRepository;
        
        public Facade(IRepository<Organization> organizations, IRepository<Department> departments, IRepository<Employee> employees)
        {
            _organizationsRepository = organizations;
            _departmentsRepository = departments;
            _employeesRepository = employees;
        }
       
        public void AddOrganization(Organization entity)
        {
            _organizationsRepository.Insert(entity);            
        }

        public void AddDepartment(Department entity)
        {
            _departmentsRepository.Insert(entity);
        }

        public void AddEmployee(Employee entity)
        {
            _employeesRepository.Insert(entity);
        }
                      
        public Organization GetOrganizationbyId(int id)
        {
            return _organizationsRepository.GetById(id);
        }

        public Department GetDepartmentById(int id)
        {
            return _departmentsRepository.GetById(id);
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeesRepository.GetById(id);
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            return _organizationsRepository.GetAll(); 
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentsRepository.GetAll();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeesRepository.GetAll();
        }

        public Employee GetRandomEmployee() 
        {
            Random rand = new Random((DateTime.Now.Millisecond) );
            var i = rand.Next(0, _employeesRepository.GetAll().Count() );
            return _employeesRepository.GetById(i);
        } 


        public void Init()
        {
            _organizationsRepository.Insert(new Organization(1) { Name = "FirstLine" });
            _departmentsRepository.Insert(new Department(1, this.GetOrganizationbyId(1)) { Name = "IT department" });
            _departmentsRepository.Insert(new Department(2, this.GetOrganizationbyId(1)) { Name = "HR department" });

            _employeesRepository.Insert(new Employee(1, this.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            _employeesRepository.Insert(new Employee(2, this.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            _employeesRepository.Insert(new Employee(3, this.GetDepartmentById(1)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });

            _employeesRepository.Insert(new Employee(4, this.GetDepartmentById(2)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            _employeesRepository.Insert(new Employee(5, this.GetDepartmentById(2)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            _employeesRepository.Insert(new Employee(6, this.GetDepartmentById(2)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });
        }
        
        public List<Employee> GetEmployeesInDepartment(int departmentId)
        {
            return this.GetAllEmployees().ToList().
                FindAll(e => e.ParentDepartment.Id == departmentId);
        }


        public List<Employee> GetAllEmployeesLivingOnTheSameStreet(int departmentId)
        {
            var employeesInDepartment = GetEmployeesInDepartment(departmentId);
            return employeesInDepartment.OrderBy(e => e.Address.Street).ToList();
        }

                        
        public List<Employee> FindEmployeesByAgeLinQ(int organizationId, int minAge, int maxAge)
        {
            var resultEmployees =
                from employee in _employeesRepository.GetAll()
                where (_departmentsRepository.GetById(employee.ParentDepartment.Id).ParentOrganization.Id == organizationId)
                where (employee.Age > minAge)
                where (employee.Age < maxAge)
                select employee;
            return resultEmployees.ToList();
        }
               
        public List<Organization> FindOrganizationsByNameOfDepartmentWithPersonNumber(string departmentName,
            int numberOfPerson)
        {
            var resultOrganizations =
                from department in _departmentsRepository.GetAll()
                where department.Name == departmentName
                let countEmployees = _employeesRepository.GetAll().Count(employee => employee.ParentDepartment.Id == department.Id)
                where countEmployees >= numberOfPerson
                select _organizationsRepository.GetById(department.ParentOrganization.Id);
            return resultOrganizations.ToList();
        }               

        public Department FindDepartmentWithOldestPerson()
        {
            return _departmentsRepository.GetById(
                _employeesRepository.GetAll().First(x => x.Age == _employeesRepository.GetAll().Max(y => y.Age))
                .ParentDepartment.Id);
        }
        
        public List<Employee> FindEmployeesWithSubstring(int organizationId, string subString)
        {
            var resultEmployees =
                from employee in _employeesRepository.GetAll()
                where (_departmentsRepository.GetById(employee.ParentDepartment.Id).ParentOrganization.Id == organizationId)
                where (employee.LastName.Contains(subString))
                select employee;
            return resultEmployees.ToList();
        }
    }

}
