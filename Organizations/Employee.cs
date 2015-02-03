using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public interface IEntity
    {
        int Id { get; }
    }

    public class Employee : IEntity
    {
        private int id;
        public Employee(int argumentId) { id = argumentId; }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address address;
        
    }
}
