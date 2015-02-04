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
        private int parentId;
        void setParent(int id) { parentId = id; }

        private int id;
        public Employee(int argumentId) { id = argumentId; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address address;

        public int Id
        {
            get
            {
                return id;
            }
        }
    }
}
