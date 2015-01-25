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
            return (FirstName + LastName).GetHashCode() + BirthDate.GetHashCode();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public int Age { get; set; }
        public DateTime  BirthDate { get; set; }
        public Address address;
    }
    

}
