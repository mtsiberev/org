﻿
namespace Organizations.DbEntity
{
    public class EmployeeDb : IEntityDb
    {
        private readonly int m_id;
        private readonly int m_departmentId;
        public string Name { get; set; }

        public EmployeeDb(int id, int parentDepartment)
        {
            m_id = id;
            m_departmentId = parentDepartment;
        }

        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public int ParentDepartmentId
        {
            get
            {
                return m_departmentId;
            }
        }
    }
}
