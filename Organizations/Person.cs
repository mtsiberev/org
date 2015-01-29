using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    
    public interface IEntity
    {
        void Show();
        int GetId();
    }
       

    public class Person 
    {           
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public int Age { get; set; }
        public DateTime  BirthDate { get; set; }
        public Address address;
    }
    

}
