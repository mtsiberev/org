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
            var organization = new Organization(1);
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                m_factory.CreateMock<IRepository<Organization>>();

            mockRepositoryOrganization.Expects.One.MethodWith(_ => _.GetById(1)).WillReturn(organization);

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                null,
                null);

            Assert.AreEqual(organization, facade.GetOrganizationById(1));
        }

        //GetAllOrganizations
        [TestMethod]
        public void TestingOfFacadeGetAllOrganizations()
        {
            var organization1 = new Organization(1);
            var organization2 = new Organization(2);
            var organizations = new List<Organization> { organization1, organization2 };

            Mock<IRepository<Organization>> mockRepositoryOrganization =
                 m_factory.CreateMock<IRepository<Organization>>();

            mockRepositoryOrganization.Expects.One.Method(_ => _.GetAll()).
                WillReturn(organizations);

            var facade = new Facade(
               mockRepositoryOrganization.MockObject,
               null,
               null);

            CollectionAssert.AreEqual(organizations, facade.GetAllOrganizations().ToList());
        }

        //GetRandomOrganization
        [TestMethod]
        public void TestingOfFacadeGetRandomEmployee()
        {
            var employee = new Employee(1, null);

            Mock<IRepository<Employee>> mockRepositoryEmployee =
              m_factory.CreateMock<IRepository<Employee>>();

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetRandom()).WillReturn(employee);

            var facade = new Facade(
                null,
                null,
              mockRepositoryEmployee.MockObject);

            Assert.AreEqual(employee, facade.GetRandomEmployee());
        }

        //GetAllEmployeesLivingOnTheSameStreet
        [TestMethod]
        public void TestingOfGetAllEmployeesLivingOnTheSameStreet()
        {
            var expectedEmployees = new List<Employee>
            {
                new Employee(1, new Department(1, null)) {Address = new Address() { Street = "Gagarina"}},
                new Employee(2, new Department(1, null)) {Address = new Address() { Street = "Gagarina"}},
                new Employee(3, new Department(1, null)) {Address = new Address() { Street = "Gorkogo"}}
            };

            Mock<IRepository<Employee>> mockRepositoryEmployee =
              m_factory.CreateMock<IRepository<Employee>>();

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(expectedEmployees);

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
            var fakeDepartment = new Department(1, new Organization(1));
            var expectedEmployee = new Employee(2, fakeDepartment) { Age = 25 };

            var fakeEmployees = new List<Employee>
            {
                new Employee(1, fakeDepartment) {Age = 20},
                expectedEmployee,
                new Employee(3, fakeDepartment) {Age = 30},
            };

            Mock<IRepository<Employee>> mockRepositoryEmployee =
             m_factory.CreateMock<IRepository<Employee>>();

            mockRepositoryEmployee.Expects.One.Method(_ => _.GetAll()).WillReturn(fakeEmployees);

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            Assert.AreEqual(expectedEmployee, facade.FindEmployeesByAge(1, 20, 30).First());
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

            var facade = new Facade(
                null,
                null,
                mockRepositoryEmployee.MockObject);

            mockRepositoryEmployee.Expects.Any.Method(_ => _.GetAll()).WillReturn(fakeEmployees);
            var resultDepartment = facade.FindDepartmentWithOldestPerson();
            Assert.AreEqual(fakeDepartment3, resultDepartment);
        }
    }
}
