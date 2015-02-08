using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Organization : IEntity
    {
        private int id;    
        public Organization(int argumentId) { id = argumentId; }
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
