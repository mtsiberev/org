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
        void SetParent(int argumentId) { _parentId = argumentId; }
        
        private int _id; 
        public Department(int argumentId) { _id = argumentId;  }
        public string Name { get; set; }
        
        public int Id
        {
            get
            {
                return _id;
            }
        }
    }
}
