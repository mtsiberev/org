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
        private Guid _parentId;
        private Guid _id;
        public Employee(Guid parentId)
        {
            _id = Guid.NewGuid();
            _parentId = parentId;
        }
        
        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public Guid ParentId
        {
            get
            {
                return _parentId;
            }
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }


    }
}
