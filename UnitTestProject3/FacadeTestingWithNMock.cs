using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;


namespace Organizations
{
    /// <summary>
    /// Summary description for FacadeTestingWithNMock
    /// </summary>
    [TestClass]
    public class FacadeTestingWithNMock
    {
        MockFactory m_factory = new MockFactory();

        [TestMethod]
        public void TestingWithNMockFacadeAddOrganization()
        {
            Mock<IRepository<Organization>> mockRepositoryOrganization = m_factory.CreateMock<IRepository<Organization>>();
            var organization = new Organization(1);

            mockRepositoryOrganization.Expects.One.Method(_ => _.Insert(null)).
                With(Is.Match<Organization>(_ => _ == organization));

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                null,
                null);

            facade.AddOrganization(organization);
        }

        [TestMethod]
        public void TestingWithNMockFacadeAddDepartment()
        {
            Mock<IRepository<Department>> mockRepositoryDepartment = m_factory.CreateMock<IRepository<Department>>();
            var department = new Department(1, null);

            mockRepositoryDepartment.Expects.One.Method(_ => _.Insert(null)).
                With(Is.Match<Department>(_ => _ == department));

            var facade = new Facade(
                null,
                mockRepositoryDepartment.MockObject,
                null);

            facade.AddDepartment(department);
        }

        [TestMethod]
        public void TestingWithNMockFacadeAddEmployee()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee = m_factory.CreateMock<IRepository<Employee>>();
            var employee = new Employee(1, null);

            mockRepositoryEmployee.Expects.One.Method(_ => _.Insert(null)).
                With(Is.Match<Employee>(_ => _ == employee));

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            facade.AddEmployee(employee);
        }

        //GetOrganizationbyId
        [TestMethod]
        public void TestingOfFacadeGetOrganizationById()
        {
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                m_factory.CreateMock<IRepository<Organization>>();

            var organization = new Organization(1);

            mockRepositoryOrganization.Expects.One.MethodWith(_ => _.GetById(1)).WillReturn(organization);

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                null,
                null);

            Assert.AreEqual(organization, facade.GetOrganizationById(1));
        }

        //GetDepartmentById
        [TestMethod]
        public void TestingOfFacadeGetDepartmentById()
        {
            Mock<IRepository<Department>> mockRepositoryDepartment =
                m_factory.CreateMock<IRepository<Department>>();

            var department = new Department(1, null);

            mockRepositoryDepartment.Expects.One.MethodWith(_ => _.GetById(1)).WillReturn(department);

            var facade = new Facade(
               null,
                 mockRepositoryDepartment.MockObject,
                null);

            Assert.AreEqual(department, facade.GetDepartmentById(1));
        }


        //GetEmployeeById
        public void TestingOfFacadeGetEmployeeById()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
                m_factory.CreateMock<IRepository<Employee>>();

            var employee = new Employee(1, null);

            mockRepositoryEmployee.Expects.One.MethodWith(_ => _.GetById(1)).WillReturn(employee);

            var facade = new Facade(
               null,
               null,
               mockRepositoryEmployee.MockObject
               );

            Assert.AreEqual(employee, facade.GetEmployeeById(1));
        }

        //GetAllOrganizations
        [TestMethod]
        public void TestingOfFacadeGetAllOrganizations()
        {
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                 m_factory.CreateMock<IRepository<Organization>>();

            var organization1 = new Organization(1);
            var organization2 = new Organization(2);
            var organizations = new List<Organization> { organization1, organization2 };

            mockRepositoryOrganization.Expects.One.Method(_ => _.GetAll()).
                WillReturn(organizations);

            var facade = new Facade(
               mockRepositoryOrganization.MockObject,
               null,
               null);

            CollectionAssert.AreEqual(organizations, facade.GetAllOrganizations().ToList());
        }

        //GetAllDepartments
        [TestMethod]
        public void TestingOfFacadeGetAllDepartments()
        {
            Mock<IRepository<Department>> mockRepositoryDepartment =
                 m_factory.CreateMock<IRepository<Department>>();

            var department1 = new Department(1, null);
            var department2 = new Department(2, null);
            var departments = new List<Department> { department1, department2 };

            mockRepositoryDepartment.Expects.One.Method(_ => _.GetAll()).
                WillReturn(departments);

            var facade = new Facade(
                null,
               mockRepositoryDepartment.MockObject,
               null);

            CollectionAssert.AreEqual(departments, facade.GetAllDepartments().ToList());
        }

        //GetAllEmployees
        [TestMethod]
        public void TestingOfFacadeGetAllEmployees()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
                 m_factory.CreateMock<IRepository<Employee>>();

            var employee1 = new Employee(1, null);
            var employee2 = new Employee(2, null);
            var employees = new List<Employee> { employee1, employee2 };

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).
                WillReturn(employees);

            var facade = new Facade(
                null,
                null,
               mockRepositoryEmployee.MockObject);

            CollectionAssert.AreEqual(employees, facade.GetAllEmployees().ToList());
        }

        //GetRandomEmployee
        [TestMethod]
        public void TestingOfFacadeGetRandomEmployee()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
              m_factory.CreateMock<IRepository<Employee>>();

            var employee = new Employee(1, null);

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetRandom()).WillReturn(employee);

            var facade = new Facade(
                null,
                null,
              mockRepositoryEmployee.MockObject);

            Assert.AreEqual(employee, facade.GetRandomEmployee());
        }

        //GetAllEmployeesLivingOnTheSameStreet-------------------
        [TestMethod]
        public void TestingOfGetAllEmployeesLivingOnTheSameStreet()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
              m_factory.CreateMock<IRepository<Employee>>();

            var employee1 = new Employee(1, new Department(1, null)) { Address = new Address() { Street = "Gagarina" } };
            var employee2 = new Employee(1, new Department(1, null)) { Address = new Address() { Street = "Larina" } };
            var employee3 = new Employee(1, new Department(1, null)) { Address = new Address() { Street = "Gagarina" } };
            var employee4 = new Employee(1, new Department(1, null)) { Address = new Address() { Street = "Larina" } };

            var returnedEmployees = new List<Employee>
            {
                employee1, employee2, employee3, employee4
            };

            var expectedEmployees = new List<Employee>
            {
                employee1, employee3, employee2, employee4
            };

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(returnedEmployees);

            var facade = new Facade(
               null,
               null,
             mockRepositoryEmployee.MockObject);

            var resultEmployees = facade.GetAllEmployeesLivingOnTheSameStreet(1);
            CollectionAssert.AreEqual(resultEmployees, expectedEmployees);
        }

        //FindEmployeesByAge
        [TestMethod]
        public void TestingOfFindEmployeesByAge()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
             m_factory.CreateMock<IRepository<Employee>>();

            var fakeDepartment = new Department(1, new Organization(1));
            var expectedEmployee = new Employee(2, fakeDepartment) { Age = 25 };

            var fakeEmployees = new List<Employee>
            {
                new Employee(1, fakeDepartment) {Age = 20},
                expectedEmployee,
                new Employee(3, fakeDepartment) {Age = 30},
            };

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(fakeEmployees);

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            Assert.AreEqual(expectedEmployee, facade.FindEmployeesByAge(1, 20, 30).First());
        }

        //FindOrganizationsByNameOfDepartmentWithPersonNumber
        [TestMethod]
        public void TestingOfFindOrganizationsByNameOfDepartmentWithPersonNumber()
        {
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                m_factory.CreateMock<IRepository<Organization>>();

            Mock<IRepository<Department>> mockRepositoryDepartment =
                m_factory.CreateMock<IRepository<Department>>();

            Mock<IRepository<Employee>> mockRepositoryEmployee =
                m_factory.CreateMock<IRepository<Employee>>();

            var organization1 = new Organization(1);
            var organization2 = new Organization(2);
            var organizations = new List<Organization> { organization1, organization2 };

            var department1 = new Department(1, organization1) { Name = "IT" };
            var department2 = new Department(2, organization1) { Name = "HR" };
            var department3 = new Department(3, organization2) { Name = "IT" };
            var department4 = new Department(4, organization2) { Name = "HR" };
            var departments = new List<Department> { department1, department2, department3, department4 };

            var employee1 = new Employee(1, department1);
            var employee2 = new Employee(2, department2);
            var employee3 = new Employee(3, department2);
            var employee4 = new Employee(4, department3);
            var employee5 = new Employee(5, department1);
            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5 };

            mockRepositoryOrganization.Expects.Any.Method(_ => _.GetAll()).WillReturn(organizations);
            mockRepositoryDepartment.Expects.Any.Method(_ => _.GetAll()).WillReturn(departments);
            mockRepositoryEmployee.Expects.Any.Method(_ => _.GetAll()).WillReturn(employees);

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                mockRepositoryDepartment.MockObject,
                mockRepositoryEmployee.MockObject);

            var actualOrganizations = facade.FindOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);
            var expectedOrganizations = new List<Organization> { organization1 };

            CollectionAssert.AreEqual(actualOrganizations, expectedOrganizations);
        }

        //FindDepartmentWithOldestPerson
        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            var fakeDepartment1 = new Department(1, null);
            var fakeDepartment2 = new Department(2, null);
            var fakeDepartment3 = new Department(3, null);

            var fakeEmployees = new List<Employee>
            {
                new Employee(1, fakeDepartment1) {Age = 20},
                new Employee(2, fakeDepartment2) {Age = 25},
                new Employee(3, fakeDepartment3) {Age = 30},
            };

            Mock<IRepository<Employee>> mockRepositoryEmployee =
             m_factory.CreateMock<IRepository<Employee>>();

            mockRepositoryEmployee.Expects.Any.Method(_ => _.GetAll()).WillReturn(fakeEmployees);

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            var resultDepartment = facade.FindDepartmentWithOldestPerson();
            Assert.AreEqual(fakeDepartment3, resultDepartment);
        }

        //FindEmployeesWithSubstring
        [TestMethod]
        public void TestingOfFindEmployeesWithSubstring()
        {
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                m_factory.CreateMock<IRepository<Organization>>();

            Mock<IRepository<Department>> mockRepositoryDepartment =
                m_factory.CreateMock<IRepository<Department>>();

            Mock<IRepository<Employee>> mockRepositoryEmployee =
                m_factory.CreateMock<IRepository<Employee>>();

            var organization = new Organization(1);
            var department = new Department(1, organization);

            var employee1 = new Employee(1, department) { LastName = "Molotov" };
            var employee2 = new Employee(2, department) { LastName = "Pavlov" };
            var employee3 = new Employee(3, department) { LastName = "Sergeev" };
            var employee4 = new Employee(4, department) { LastName = "Zotov" };
            var employee5 = new Employee(5, department) { LastName = "Aleeev" };
            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5 };

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(employees);

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                mockRepositoryDepartment.MockObject,
                mockRepositoryEmployee.MockObject);

            var expectedEmployee = new List<Employee> { employee1, employee2, employee4 };
            var actualEmployee = facade.FindEmployeesWithSubstring(1, "ov");

            CollectionAssert.AreEqual(actualEmployee, expectedEmployee);
        }

        //GetEmployeesInDepartment
        [TestMethod]
        public void TestingOfGetEmployeesInDepartment()
        {
            Mock<IRepository<Employee>> mockRepositoryEmployee =
          m_factory.CreateMock<IRepository<Employee>>();

            var department1 = new Department(1, null);
            var department2 = new Department(2, null);

            var employee1 = new Employee(1, department1);
            var employee2 = new Employee(2, department2);
            var employee3 = new Employee(3, department2);
            var employees = new List<Employee> { employee1, employee2, employee3 };

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(employees);

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            var expectedEmployees = new List<Employee> { employee2, employee3 };
            var actualEmployees = facade.GetEmployeesInDepartment(2);

            CollectionAssert.AreEqual(expectedEmployees, actualEmployees);
        }

    }
}
