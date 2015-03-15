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
            var organizationWrong = new Organization(2);
            var organizationCopy = organization;
        
            //mockRepositoryOrganization.Expects.One.MethodWith(_ => _.Insert(organization));
            mockRepositoryOrganization.Expects.One.Method(_ => _.Insert(null)).
                With(Is.Match<Organization>(_ => _ == organizationCopy));
            
            var facade = new Facade(
                mockRepositoryOrganization.MockObject,
                null,
                null);

            facade.AddOrganization(organization);
        }
        

    }
}
