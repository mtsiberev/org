using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Department : IEntity
    {
        private int parentId;
        void setParent(int id) { parentId = id; }
        
        private int id; 
        public Department(int argumentId) { id = argumentId;  }
        public string Name { get; set; }
        
        public int Id
        {
            get
            {
                return id;
            }
        }
    }
}
