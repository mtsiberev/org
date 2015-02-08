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
        void SetParent(int argumentId) { _parentId = argumentId; }

        private int _id;
        public Employee(int argumentId) { _id = argumentId; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address;

        public int Id
        {
            get
            {
                return _id;
            }
        }
    }
}
