using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Employee : IEntity
    {
        private int _parentId;
        public Employee(int parentId)
        {
            _parentId = parentId;
        }

        private int _id;
        public void SetId(int instanceId) { _id = instanceId; }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address;
    }
}
