using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Person
    {           
        public int GetPersonId()
        {
            return Name.GetHashCode() + Age.GetHashCode();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public Address address;
    }

}
