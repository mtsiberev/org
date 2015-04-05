using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Employee : IEntity
    {
        private readonly Department m_parentDepartment;
        private readonly int m_id;
        public Employee(int id, Department parentDepartment)
        {
            m_id = id;
            m_parentDepartment = parentDepartment;
        }
        
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public Department ParentDepartment
        {
            get
            {
                return m_parentDepartment;
            }
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        public new int GetEntityCode()
        {
            return 2;
        }
    }
}
