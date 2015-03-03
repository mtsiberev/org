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
        private Department _parentDepartment;
        private int _id;
        public Employee(int id, Department parentDepartment)
        {
            _id = id;
            _parentDepartment = parentDepartment;
        }
        
        public int Id
        {
            get
            {
                return _id;
            }
        }

        public Department ParentDepartment
        {
            get
            {
                return _parentDepartment;
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
