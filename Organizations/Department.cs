using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Department : IEntity
    {
        private IEntity _parentEntity;
        private int _id;
        public Department(int id, IEntity parentEntity)
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

        public new int GetEntityCode()
        {
            return 1;
        }


    }
}
