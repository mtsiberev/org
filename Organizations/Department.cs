using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Department : IEntity
    {
        private int _parentId;
        public Department(int parentId)
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

        public string Name { get; set; }
    }
}
