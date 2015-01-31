using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OrganizationsNS;
using System.Collections.Generic;
using System.Linq;


namespace OrganizationsNS
{

    public class EntityCompare : IEqualityComparer<IEntity>
    {
        public bool Equals(IEntity entity1, IEntity entity2)
        {
            return (entity1.GetId() == entity2.GetId());
        }

        public int GetHashCode(IEntity entity)
        {
            return entity.GetHashCode();
        }
    }


    [TestClass]
    public class TestingOfFacadeMethods
    {
        static public EntityCompare entityComparator = new EntityCompare();

        static public bool CompareListOfObjects<T>(List<T> expectedInstances, List<T> actualInstances, IEqualityComparer<T> cmp)
        {
            foreach (var employee in actualInstances)
            {
                if (!expectedInstances.Contains(employee, cmp))
                    return false;
            }

            foreach (var employee in expectedInstances)
            {
                if (!actualInstances.Contains(employee, cmp))
                    return false;
            }
            return true;
        }


        [TestMethod]
        public void TestingOfFindEmployeesByAge()
        {
            Repository organizationsRepository = new Repository();
            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();

            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "FirstLine" });
            Organization organization = organizationsRepository.GetEntityById(0);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId() ) { Name = "HR department" });
            Department department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov2", Age = 25, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov3", Age = 30, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov4", Age = 35, address = new Address() { City = "NN", Street = "larina" } });

            actualEmployees = Facade.FindEmployeesByAge(organizationsRepository.GetEntityById(0), 23, 32);

            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov2"));
            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov3"));

            bool result = TestingOfFacadeMethods.CompareListOfObjects(actualEmployees, expectedEmployees, entityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }


        [TestMethod]
        public void TestingOfFindEmpsByAgeLinQ()
        {
            Repository organizationsRepository = new Repository();
            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();

            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "FirstLine" });
            Organization organization = organizationsRepository.GetEntityById(0);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            Department department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov2", Age = 25, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov3", Age = 30, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov4", Age = 35, address = new Address() { City = "NN", Street = "larina" } });

            actualEmployees = Facade.FindEmployeesByAge(organizationsRepository.GetEntityById(0), 23, 32);

            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov2"));
            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov3"));

            bool result = TestingOfFacadeMethods.CompareListOfObjects(actualEmployees, expectedEmployees, entityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }


        [TestMethod]
        public void TestingOfFindOrganizationsByNameOfDepartmentWithPersonNumber()
        {
            List<Organization> actualOrganizations = new List<Organization>();
            List<Organization> expectedOrganizations = new List<Organization>();
            ///////////////////////////////////////////////////////////
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
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey1", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            
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
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey1", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            
          
            expectedOrganizations.Add(organizationsRepository.GetEntityById(0));
            expectedOrganizations.Add(organizationsRepository.GetEntityById(2));
            //////////////////////////////////////////////////////////
            actualOrganizations = Facade.FindOrganizationsByNameOfDepartmentWithPersonNumber(organizationsRepository.GetAllOrganizations(), "IT", 3);

            bool result = TestingOfFacadeMethods.CompareListOfObjects(actualOrganizations, expectedOrganizations, entityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            Department actualDepartment = new Department(-1);
            Department expectedDepartment = new Department(-1);

            Repository organizationsRepository = new Repository();

            //FirstLine organization
            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "FirstLine" });
            Organization organization = organizationsRepository.GetEntityById(0);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            Department department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 25, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 31, address = new Address() { City = "NN", Street = "larina" } });

            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "R&D department" });
            department = organization.GetDepartmentById(1);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Petr", LastName = "Petrov1", Age = 29, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Mikhail", LastName = "Petrov1", Age = 38, address = new Address() { City = "NN", Street = "larina" } });
            //
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Ivan", LastName = "Petrov1", Age = 40, address = new Address() { City = "NN", Street = "larina" } });

            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "IT department" });
            department = organization.GetDepartmentById(2);
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "John", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", Age = 29, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", Age = 37, address = new Address() { City = "NN", Street = "larina" } });


            actualDepartment = Facade.FindDepartmentWithOldestPerson(organizationsRepository.GetEntityById(0));
            expectedDepartment = organizationsRepository.GetEntityById(0).GetDepartmentById(1);

            bool result = ReferenceEquals(actualDepartment, expectedDepartment);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindEmployeeWithSubstring()
        {
            Repository organizationsRepository = new Repository();
            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();

            organizationsRepository.Insert(new Organization(organizationsRepository.GetNewEntityId()) { Name = "FirstLine" });
            Organization organization = organizationsRepository.GetEntityById(0);
            organization.AddDepartment(new Department(organization.GetNewDepartmentId()) { Name = "HR department" });
            Department department = organization.GetDepartmentById(0);
            department.AddEmployee(new Employee(department.GetNewEmployeeId() ) { FirstName = "Sergey", LastName = "Petrov1", Age = 20, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId() ) { FirstName = "Dmitry", LastName = "Petrov2", Age = 25, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId() ) { FirstName = "Alexey", LastName = "Kotorov3", Age = 30, address = new Address() { City = "NN", Street = "larina" } });
            department.AddEmployee(new Employee(department.GetNewEmployeeId() ) { FirstName = "Alexey", LastName = "Petrov4", Age = 35, address = new Address() { City = "NN", Street = "larina" } });

            actualEmployees = Facade.FindEmployeesWithSubstring(organization, "Pet");

            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov1"));
            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov2"));
            expectedEmployees.Add(department.GetAllEmployees().Find(x => x.LastName == "Petrov4"));
                        
            bool result = TestingOfFacadeMethods.CompareListOfObjects(actualEmployees, expectedEmployees, entityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }        
    }


}
