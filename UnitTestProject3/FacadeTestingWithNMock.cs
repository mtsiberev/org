using System;
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
            /*
            var organization = new Organization(1);
            Mock<IRepository<Organization>> mockRepositoryOrganization =
                m_factory.CreateMock<IRepository<Organization>>();
          
           mockRepositoryOrganization.Expects.One.Method(_ => _.GetById(1)).WillReturn(organization);

            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                null,
                null);

            facade.GetOrganizationById(1);
            Assert.AreEqual(organization, facade.GetOrganizationById(1));
            */
        }

        
    }
}
