using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Employee : Person
    {
        public int Id { get; private set; }

        public Employee(int id)//Id выдается отделом
        {
            Id = id;
        }
    }
}
