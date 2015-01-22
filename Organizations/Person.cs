using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{

    public class Person
    {
        // public Person(){}               
        public int GetPersonId()
        {
            return Name.GetHashCode() + Age.GetHashCode();
        }
        public string Name { get; set; }
        public int Age { get; set; }
        //static Random rand = new Random((DateTime.Now.Millisecond));//решение проблемы одинаковых случайных чисел
        public Address address;
    }

}
