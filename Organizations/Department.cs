using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Department : IEntity
    {
        private Guid _id;
        private Guid _parentId;
        public Department(Guid parentId)
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
    }
}
