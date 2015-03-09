﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Organizations
{
    public class MockOrganizationRepository : IRepository<Organization>
    {
        private readonly List<Organization> m_data;
        public Organization LastInsertedOrganization { get; private set; }
        public Organization LastDeletedOrganization { get; private set; }
        public bool InsertIsCalled { get; private set; }
        public bool DeleteIsCalled { get; private set; }
        public bool GetByIdIsCalled { get; private set; }

        public MockOrganizationRepository()
        {
            m_data = new List<Organization>();
        }

        public MockOrganizationRepository(List<Organization> organizations)
        {
            m_data = organizations;
        }

        public MockOrganizationRepository(Organization org)
        {
            LastInsertedOrganization = org;
        }

        public void Insert(Organization entity)
        {
            LastInsertedOrganization = entity;
            InsertIsCalled = true;
        }

        public void Delete(Organization entity)
        {
            LastDeletedOrganization = entity;
            DeleteIsCalled = true;
        }

        public IEnumerable<Organization> GetAll()
        {
            return m_data;
        }

        public Organization GetById(int id)
        {
            GetByIdIsCalled = true;
            return m_data != null ? m_data.Single(e => e.Id.Equals(id)) : LastInsertedOrganization;
        }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////
    /// </summary>

    public class MockDepartmentRepository : IRepository<Department>
    {
        private readonly List<Department> m_data;
        public Department LastInsertedDepartment { get; private set; }
        public Department LastDeletedDepartment { get; private set; }
        public bool InsertIsCalled { get; private set; }
        public bool DeleteIsCalled { get; private set; }
        public bool GetByIdIsCalled { get; private set; }

        public MockDepartmentRepository()
        {
            m_data = new List<Department>();
        }

        public MockDepartmentRepository(List<Department> departments)
        {
            m_data = departments;
        }

        public MockDepartmentRepository(Department dep)
        {
            LastInsertedDepartment = dep;
        }

        public void Insert(Department entity)
        {
            LastInsertedDepartment = entity;
            InsertIsCalled = true;
        }

        public void Delete(Department entity)
        {
            LastDeletedDepartment = entity;
            DeleteIsCalled = true;
        }

        public IEnumerable<Department> GetAll()
        {
            return m_data;
        }

        public Department GetById(int id)
        {
            GetByIdIsCalled = true;
            return m_data != null ? m_data.Single(e => e.Id.Equals(id)) : LastInsertedDepartment;
        }
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////
    /// </summary>

    public class MockEmployeeRepository : IRepository<Employee>
    {
        private readonly List<Employee> m_data;
        public Employee LastInsertedEmployee { get; private set; }
        public Employee LastDeletedEmployee { get; private set; }
        public bool InsertIsCalled { get; private set; }
        public bool DeleteIsCalled { get; private set; }
        public bool GetByIdIsCalled { get; private set; }
        public bool CheckRandomEmployeeIsCalled { get; set; }
        public string CallingMethod { get; private set; }

        public MockEmployeeRepository()
        {
            m_data = new List<Employee>();
        }

        public MockEmployeeRepository(List<Employee> organizations)
        {
            m_data = organizations;
        }

        public MockEmployeeRepository(Employee emp)
        {
            LastInsertedEmployee = emp;
        }

        public void Insert(Employee entity)
        {
            LastInsertedEmployee = entity;
            InsertIsCalled = true;
        }

        public void Delete(Employee entity)
        {
            LastDeletedEmployee = entity;
            DeleteIsCalled = true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_data;
        }

        public Employee GetById(int id)
        {
            if (CheckRandomEmployeeIsCalled)
            {
                var stackTrace = new StackTrace();           
                var stackFrames = stackTrace.GetFrames();  
                CallingMethod = stackFrames[1].GetMethod().Name;
            }
            GetByIdIsCalled = true;
            return m_data != null ? m_data.Single(e => e.Id.Equals(id)) : LastInsertedEmployee;
        }
    }
    
}
