using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Employee : Person, IEntity
    {
        public int Id { get; private set; }
        public Employee(int id) { Id = id; }
        
        public void Show(){}
        
        public int GetId()
        {
            return Id;
        }
    }
}
