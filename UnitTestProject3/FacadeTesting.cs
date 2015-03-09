using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Organizations;
using System.Collections.Generic;
using System.Linq;


namespace Organizations
{

    public class EntityCompare : IEqualityComparer<IEntity>
    {
        public bool Equals(IEntity entity1, IEntity entity2)
        {
            return (entity1.Id == entity2.Id);
        }

        public int GetHashCode(IEntity entity)
        {
            return entity.GetHashCode();
        }
    }

    [TestClass]
    public class TestingOfFacadeMethods
    {
        static public EntityCompare EntityComparator = new EntityCompare();

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
        public void TestingOfFindEmployeesByAgeLinQ()
        {

            var facade = new Facade(
                new Repository<Organization>(),
                new Repository<Department>(),
                new Repository<Employee>());

            var actualEmployees = new List<Employee>();
            var expectedEmployees = new List<Employee>();

            facade.AddOrganization(new Organization(1) { Name = "FirstLine" });
            facade.AddDepartment(new Department(1, facade.GetOrganizationById(1)) { Name = "IT department" });
            facade.AddDepartment(new Department(2, facade.GetOrganizationById(1)) { Name = "HR department" });

            facade.AddEmployee(new Employee(1, facade.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            facade.AddEmployee(new Employee(2, facade.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            facade.AddEmployee(new Employee(3, facade.GetDepartmentById(1)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });

            facade.AddEmployee(new Employee(4, facade.GetDepartmentById(2)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            facade.AddEmployee(new Employee(5, facade.GetDepartmentById(2)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(6, facade.GetDepartmentById(2)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });

            actualEmployees = facade.FindEmployeesByAgeLinQ(1, 21, 37);

            expectedEmployees.Add(facade.GetEmployeeById(2));
            expectedEmployees.Add(facade.GetEmployeeById(4));
            expectedEmployees.Add(facade.GetEmployeeById(5));

            var result = TestingOfFacadeMethods.CompareListOfObjects(actualEmployees, expectedEmployees, EntityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }


        [TestMethod]
        public void TestingOfFindOrganizationsByNameOfDepartmentWithPersonNumber()
        {
            var facade = new Facade(
                new Repository<Organization>(),
                new Repository<Department>(),
                new Repository<Employee>());

            var actualOrganizations = new List<Organization>();
            var expectedOrganizations = new List<Organization>();

            facade.AddOrganization(new Organization(1) { Name = "FirstLine" });
            facade.AddDepartment(new Department(1, facade.GetOrganizationById(1)) { Name = "IT department" });
            facade.AddDepartment(new Department(2, facade.GetOrganizationById(1)) { Name = "HR department" });
            facade.AddEmployee(new Employee(1, facade.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            facade.AddEmployee(new Employee(2, facade.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            facade.AddEmployee(new Employee(3, facade.GetDepartmentById(2)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });


            facade.AddOrganization(new Organization(2) { Name = "SecondLine" });
            facade.AddDepartment(new Department(3, facade.GetOrganizationById(2)) { Name = "IT department" });
            facade.AddDepartment(new Department(4, facade.GetOrganizationById(2)) { Name = "HR department" });
            facade.AddEmployee(new Employee(4, facade.GetDepartmentById(3)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            facade.AddEmployee(new Employee(5, facade.GetDepartmentById(4)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(6, facade.GetDepartmentById(4)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });


            facade.AddOrganization(new Organization(3) { Name = "ThirdLine" });
            facade.AddDepartment(new Department(5, facade.GetOrganizationById(3)) { Name = "IT department" });
            facade.AddDepartment(new Department(6, facade.GetOrganizationById(3)) { Name = "HR department" });
            facade.AddEmployee(new Employee(7, facade.GetDepartmentById(5)) { Name = "Alexey", LastName = "Pavlov", Age = 35, Address = new Address() { City = "SPB", Street = "Chehova" } });
            facade.AddEmployee(new Employee(8, facade.GetDepartmentById(5)) { Name = "Alexandr", LastName = "Kotov", Age = 29, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(9, facade.GetDepartmentById(5)) { Name = "Andrey", LastName = "Starov", Age = 28, Address = new Address() { City = "NN", Street = "Nartova" } });


            expectedOrganizations.Add(facade.GetOrganizationById(1));
            expectedOrganizations.Add(facade.GetOrganizationById(3));

            actualOrganizations = facade.FindOrganizationsByNameOfDepartmentWithPersonNumber("IT department", 2);

            var result = TestingOfFacadeMethods.CompareListOfObjects(actualOrganizations, expectedOrganizations, EntityComparator);
            Assert.AreEqual(true, result, "Not equal");
        }



        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            var facade = new Facade(
                new Repository<Organization>(),
                new Repository<Department>(),
                new Repository<Employee>());
            facade.AddOrganization(new Organization(1) { Name = "FirstLine" });
            facade.AddDepartment(new Department(1, facade.GetOrganizationById(1)) { Name = "IT department" });
            facade.AddDepartment(new Department(2, facade.GetOrganizationById(1)) { Name = "HR department" });
            facade.AddEmployee(new Employee(1, facade.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            facade.AddEmployee(new Employee(2, facade.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            facade.AddEmployee(new Employee(3, facade.GetDepartmentById(2)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });

            facade.AddOrganization(new Organization(2) { Name = "SecondLine" });
            facade.AddDepartment(new Department(3, facade.GetOrganizationById(2)) { Name = "IT department" });
            facade.AddDepartment(new Department(4, facade.GetOrganizationById(2)) { Name = "HR department" });
            facade.AddEmployee(new Employee(4, facade.GetDepartmentById(3)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            facade.AddEmployee(new Employee(5, facade.GetDepartmentById(4)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(6, facade.GetDepartmentById(4)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });

            facade.AddOrganization(new Organization(3) { Name = "ThirdLine" });
            facade.AddDepartment(new Department(5, facade.GetOrganizationById(3)) { Name = "IT department" });
            facade.AddDepartment(new Department(6, facade.GetOrganizationById(3)) { Name = "HR department" });
            facade.AddEmployee(new Employee(7, facade.GetDepartmentById(5)) { Name = "Alexey", LastName = "Pavlov", Age = 35, Address = new Address() { City = "SPB", Street = "Chehova" } });
            facade.AddEmployee(new Employee(8, facade.GetDepartmentById(5)) { Name = "Alexandr", LastName = "Kotov", Age = 29, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(9, facade.GetDepartmentById(5)) { Name = "Andrey", LastName = "Starov", Age = 28, Address = new Address() { City = "NN", Street = "Nartova" } });

            var expectedDepartment = facade.GetDepartmentById(2);
            var actualDepartment = facade.FindDepartmentWithOldestPerson();

            var result = ReferenceEquals(actualDepartment, expectedDepartment);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindEmployeeWithSubstring()
        {
            var actualEmployees = new List<Employee>();
            var expectedEmployees = new List<Employee>();

            var facade = new Facade(
                new Repository<Organization>(),
                new Repository<Department>(),
                new Repository<Employee>());
            facade.AddOrganization(new Organization(1) { Name = "FirstLine" });
            facade.AddDepartment(new Department(1, facade.GetOrganizationById(1)) { Name = "IT department" });
            facade.AddDepartment(new Department(2, facade.GetOrganizationById(1)) { Name = "HR department" });
            facade.AddEmployee(new Employee(1, facade.GetDepartmentById(1)) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "Larina" } });
            facade.AddEmployee(new Employee(2, facade.GetDepartmentById(1)) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "Gorkogo" } });
            facade.AddEmployee(new Employee(3, facade.GetDepartmentById(2)) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "SPB", Street = "Larina" } });
            facade.AddEmployee(new Employee(4, facade.GetDepartmentById(1)) { Name = "Petr", LastName = "Zuev", Age = 25, Address = new Address() { City = "SPB", Street = "Pushkina" } });
            facade.AddEmployee(new Employee(5, facade.GetDepartmentById(2)) { Name = "Evgeny", LastName = "Palev", Age = 33, Address = new Address() { City = "NN", Street = "Lenina" } });
            facade.AddEmployee(new Employee(6, facade.GetDepartmentById(1)) { Name = "Denis", LastName = "Chadov", Age = 38, Address = new Address() { City = "NN", Street = "Larina" } });

            expectedEmployees.Add(facade.GetEmployeeById(1));
            expectedEmployees.Add(facade.GetEmployeeById(2));
            expectedEmployees.Add(facade.GetEmployeeById(3));
            expectedEmployees.Add(facade.GetEmployeeById(6));

            actualEmployees = facade.FindEmployeesWithSubstring(1, "ov");

            var result = TestingOfFacadeMethods.CompareListOfObjects(actualEmployees, expectedEmployees, EntityComparator);
            Assert.AreEqual(true, result, "Not equal");

        }
    }



}
