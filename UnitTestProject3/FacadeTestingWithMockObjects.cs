using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Organizations
{

    [TestClass]
    public class FacadeTestingWithMockObjects
    {

        [TestMethod]
        public void TestingOfFacadeAddOrganization()
        {
            var organization = new Organization(1);
            var mockOrganizations = new MockOrganizationRepository();
            var facade = new Facade(
                mockOrganizations,
                null,
                null);

            facade.AddOrganization(organization);
            Assert.AreEqual(true, mockOrganizations.InsertIsCalled);
            Assert.AreEqual(organization, mockOrganizations.LastInsertedOrganization);
        }

        [TestMethod]
        public void TestingOfFacadeAddDepartment()
        {
            var department = new Department(1, null);
            var mockDepartments = new MockDepartmentRepository();
            var facade = new Facade(
                null,
                mockDepartments,
                null);

            facade.AddDepartment(department);
            Assert.AreEqual(true, mockDepartments.InsertIsCalled);
            Assert.AreEqual(department, mockDepartments.LastInsertedDepartment);
        }

        [TestMethod]
        public void TestingOfFacadeAddEmployee()
        {
            var employee = new Employee(1, null);
            var mockEmployees = new MockEmployeeRepository();
            var facade = new Facade(
                null,
                null,
                mockEmployees);

            facade.AddEmployee(employee);

            Assert.AreEqual(true, mockEmployees.InsertIsCalled);
            Assert.AreEqual(employee, mockEmployees.LastInsertedEmployee);
        }

        //GetOrganizationbyId
        [TestMethod]
        public void TestingOfFacadeGetOrganizationById()
        {
            var organization = new Organization(1);
            var mockOrganizations = new MockOrganizationRepository(organization);
            var facade = new Facade(
                mockOrganizations,
                null,
                null);

            Assert.AreEqual(organization, facade.GetOrganizationById(1));
            Assert.AreEqual(true, mockOrganizations.GetByIdIsCalled);
        }

        //GetDepartmentById
        [TestMethod]
        public void TestingOfFacadeGetDepartmentById()
        {
            var department = new Department(1, null);
            var mockDepartments = new MockDepartmentRepository(department);
            var facade = new Facade(
                null,
                mockDepartments,
                null);

            Assert.AreEqual(department, facade.GetDepartmentById(1));
            Assert.AreEqual(true, mockDepartments.GetByIdIsCalled);
        }

        //GetEmployeeById
        [TestMethod]
        public void TestingOfFacadeGetEmployeeById()
        {
            var employee = new Employee(1, null);
            var mockEmployees = new MockEmployeeRepository(employee);
            var facade = new Facade(
                null,
                null,
                mockEmployees);

            Assert.AreEqual(employee, facade.GetEmployeeById(1));
            Assert.AreEqual(true, mockEmployees.GetByIdIsCalled);
        }

        //GetAllOrganizations
        [TestMethod]
        public void TestingOfFacadeGetAllOrganizations()
        {
            var organization1 = new Organization(1);
            var organization2 = new Organization(2);
            var organizations = new List<Organization> { organization1, organization2 };

            var mockOrganizations = new MockOrganizationRepository(organizations);
            var facade = new Facade(
                mockOrganizations,
                null,
                null);

            Assert.AreEqual(organizations, facade.GetAllOrganizations());
        }
        //GetAllDepartments
        [TestMethod]
        public void TestingOfFacadeGetAllDepartments()
        {
            var department1 = new Department(1, null);
            var department2 = new Department(2, null);
            var departments = new List<Department> { department1, department2 };

            var mockDepartments = new MockDepartmentRepository(departments);
            var facade = new Facade(
                null,
                mockDepartments,
                null);

            Assert.AreEqual(departments, facade.GetAllDepartments());
        }
        //GetAllEmployees
        [TestMethod]
        public void TestingOfFacadeGetAllEmployees()
        {
            var employee1 = new Employee(1, null);
            var employee2 = new Employee(2, null);
            var employees = new List<Employee> { employee1, employee2 };

            var mockEmployees = new MockEmployeeRepository(employees);
            var facade = new Facade(
                null,
                null,
                mockEmployees);

            Assert.AreEqual(employees, facade.GetAllEmployees());
        }

        //GetRandomEmployee
        [TestMethod]
        public void TestingOfFacadeGetRandomEmployee()
        {
            var employee1 = new Employee(1, null);
            var employee2 = new Employee(2, null);
            var employees = new List<Employee> { employee1, employee2 };

            var mockEmployees = new MockEmployeeRepository(employees) { CheckRandomEmployeeIsCalled = true };

            var facade = new Facade(
                null,
                null,
                mockEmployees);

            facade.GetRandomEmployee();
            Assert.AreEqual("GetRandomEmployee", mockEmployees.CallingMethod);
        }


        //GetAllEmployeesLivingOnTheSameStreet
        [TestMethod]
        public void TestingOfGetAllEmployeesLivingOnTheSameStreet()
        {
            var department1 = new Department(1, null);
            var employee1 = new Employee(1, department1) { Address = new Address() { Street = "Gagarina" } };
            var employee2 = new Employee(2, department1) { Address = new Address() { Street = "Gorkogo" } };
            var employee3 = new Employee(3, department1) { Address = new Address() { Street = "Gagarina" } };

            var employees = new List<Employee> { employee1, employee2, employee3 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var facade = new Facade(
                null,
                null,
                mockEmployees);
            var actualEmployees = facade.GetAllEmployeesLivingOnTheSameStreet(1);
            var expectedEmployees = new List<Employee> { employee1, employee3, employee2 };
            CollectionAssert.AreEqual(actualEmployees, expectedEmployees);
        }

        //FindEmployeesByAgeLinQ
        [TestMethod]
        public void TestingOfFindEmployeesByAgeLinQ()
        {
            var organization = new Organization(1);
            var mockOrganizations = new MockOrganizationRepository(organization);

            var department1 = new Department(1, organization);
            var department2 = new Department(2, organization);
            var departments = new List<Department> { department1, department2 };
            var mockDepartments = new MockDepartmentRepository(departments);

            var employee1 = new Employee(1, department1) { Age = 20 };
            var employee2 = new Employee(2, department1) { Age = 21 };
            var employee3 = new Employee(3, department2) { Age = 21 };
            var employee4 = new Employee(4, department2) { Age = 22 };
            var employees = new List<Employee> { employee1, employee2, employee3, employee4 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);

            var actualEmployees = facade.FindEmployeesByAgeLinQ(1, 20, 22);
            var expectedEmployees = new List<Employee> { employee2, employee3 };

            CollectionAssert.AreEqual(actualEmployees, expectedEmployees);
        }

        //FindOrganizationsByNameOfDepartmentWithPersonNumber
        [TestMethod]
        public void TestingOfFindOrganizationsByNameOfDepartmentWithPersonNumber()
        {
            var organization1 = new Organization(1);
            var organization2 = new Organization(2);
            var organizations = new List<Organization> { organization1, organization2 };
            var mockOrganizations = new MockOrganizationRepository(organizations);

            var department1 = new Department(1, organization1) { Name = "IT" };
            var department2 = new Department(2, organization1) { Name = "HR" };
            var department3 = new Department(3, organization2) { Name = "IT" };
            var department4 = new Department(4, organization2) { Name = "HR" };

            var departments = new List<Department> { department1, department2, department3, department4 };
            var mockDepartments = new MockDepartmentRepository(departments);

            var employee1 = new Employee(1, department1);
            var employee2 = new Employee(2, department2);
            var employee3 = new Employee(3, department2);
            var employee4 = new Employee(4, department3);
            var employee5 = new Employee(5, department1);

            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);

            var actualOrganizations = facade.FindOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);
            var expectedOrganizations = new List<Organization> { organization1 };

            CollectionAssert.AreEqual(actualOrganizations, expectedOrganizations);
        }

        //FindDepartmentWithOldestPerson
        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            var organization = new Organization(1);
            var mockOrganizations = new MockOrganizationRepository(organization);

            var department1 = new Department(1, organization) { Name = "IT" };
            var department2 = new Department(2, organization) { Name = "HR" };
            var department3 = new Department(3, organization) { Name = "RD" };

            var departments = new List<Department> { department1, department2, department3 };
            var mockDepartments = new MockDepartmentRepository(departments);

            var employee1 = new Employee(1, department1) { Age = 25 };
            var employee2 = new Employee(2, department2) { Age = 30 };
            var employee3 = new Employee(3, department3) { Age = 35 };
            var employee4 = new Employee(4, department1) { Age = 40 };
            var employee5 = new Employee(5, department2) { Age = 45 };

            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);

            var actualDepartment = facade.FindDepartmentWithOldestPerson();
            var expectedDepartment = department2;

            Assert.AreEqual(actualDepartment, expectedDepartment);
        }

        //FindEmployeesWithSubstring
        [TestMethod]
        public void TestingOfFindEmployeesWithSubstring()
        {
            var organization = new Organization(1);
            var mockOrganizations = new MockOrganizationRepository(organization);

            var department = new Department(1, organization);
            var mockDepartments = new MockDepartmentRepository(department);

            var employee1 = new Employee(1, department) { LastName = "Molotov"};
            var employee2 = new Employee(2, department) {LastName = "Pavlov"};
var employee3 = new Employee(3, department) { LastName = "Sergeev"};
            var employee4 = new Employee(4, department) { LastName = "Zotov"};
            
            var employee5 = new Employee(5, department) { LastName = "Aleeev"};
            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);

            var expectedEmployee = new List<Employee> {employee1, employee2, employee4};
            var actualEmployee = facade.FindEmployeesWithSubstring(1, "ov");

            CollectionAssert.AreEqual(actualEmployee, expectedEmployee);
        }

        //GetEmployeesInDepartment
        [TestMethod]
        public void TestingOfGetEmployeesInDepartment()
        {
            var department1 = new Department(1, null);
            var department2 = new Department(2, null);
            var departments = new List<Department> { department1, department2 };
            var mockDepartments = new MockDepartmentRepository(departments);

            var employee1 = new Employee(1, department1);
            var employee2 = new Employee(2, department2);
            var employee3 = new Employee(3, department2);
            var employees = new List<Employee> { employee1, employee2, employee3 };
            var mockEmployees = new MockEmployeeRepository(employees);

            var expectedEmployees = new List<Employee> { employee2, employee3 };

            var facade = new Facade(
                null,
                mockDepartments,
                mockEmployees);
            var actualEmployees = facade.GetEmployeesInDepartment(2);

            CollectionAssert.AreEqual(expectedEmployees, actualEmployees);
        }
    }

}
