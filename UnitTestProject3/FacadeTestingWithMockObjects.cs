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
            MockRepository<Organization> mockOrganizations = new MockRepository<Organization>();
            MockRepository<Department> mockDepartments = new MockRepository<Department>();
            MockRepository<Employee> mockEmployees = new MockRepository<Employee>();
            var organization = new Organization(1);

            Facade facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);            
            facade.AddOrganization(organization);

            Assert.AreEqual(mockOrganizations.LastInsertedEntity.Id, organization.Id);
        }

        [TestMethod]
        public void TestingOfGetEmployeesInDepartment()
        {
            MockRepository<Organization> mockOrganizations = new MockRepository<Organization>();
            MockRepository<Department> mockDepartments = new MockRepository<Department>();
            MockRepository<Employee> mockEmployees = new MockRepository<Employee>();
            var organization = new Organization(1);
            mockOrganizations.Insert(organization);

            var department1 = new Department(1, organization);
            var department2 = new Department(2, organization);
            mockDepartments.Insert(department1);
            mockDepartments.Insert(department2);
            
            var employee1 = new Employee(1, department1);
            var employee2 = new Employee(2, department2);
            var employee3 = new Employee(3, department2);
            mockEmployees.Insert(employee1);
            mockEmployees.Insert(employee2);
            mockEmployees.Insert(employee3);

            var expectedEmployees = new List<Employee>();
            expectedEmployees.Add(employee2);
            expectedEmployees.Add(employee3);

            Facade facade = new Facade(
                mockOrganizations,
                mockDepartments,
                mockEmployees);
            var actualEmployees = facade.GetEmployeesInDepartment(2);

            CollectionAssert.AreEqual(expectedEmployees, actualEmployees);
        }

    }
}
