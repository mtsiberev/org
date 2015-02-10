using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Organization : IEntity
    {
        private Guid _id;
        private Guid _parentId;
        public Organization()
        {
            _parentId = Guid.NewGuid(); 
            _id = Guid.NewGuid();
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
