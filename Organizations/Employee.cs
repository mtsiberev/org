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
        private IEntity _parentEntity;
        private int _id;
        public Employee(int id, IEntity parentEntity)
        {
            _id = id;
            _parentEntity = parentEntity;
        }
        
        public int Id
        {
            get
            {
                return _id;
            }
        }

        public IEntity ParentEntity
        {
            get
            {
                return _parentEntity;
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
