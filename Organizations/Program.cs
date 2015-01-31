using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace OrganizationsNS
{

    class Program
    {
        static void Main(string[] args)
        {
            Repository organizationsRepository = new Repository();

            //FirstLine organization
            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "FirstLine" });
            Organization organization = organizationsRepository.GetEntityById(0);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            Department department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "R&D department" });
            department = organization.GetDepartmentById(1);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Petr", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Mikhail", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Ivan", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "IT department" });
            department = organization.GetDepartmentById(2);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "John", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });

            //SecondLine organization
            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "SecondLine" });
            organization = organizationsRepository.GetEntityById(1);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "R&D department" });
            department = organization.GetDepartmentById(1);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "IT department" });
            department = organization.GetDepartmentById(2);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            //ThirdLine organization
            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "ThirdLine" });
            organization = organizationsRepository.GetEntityById(2);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });

            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "R&D department" });
            department = organization.GetDepartmentById(1);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });

            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "IT department" });
            department = organization.GetDepartmentById(2);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });


            //department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", BirthDate = new DateTime(1985, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            //department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", BirthDate = new DateTime(1975, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            
        }
    }


}


