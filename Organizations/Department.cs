using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Department : IEntity
    {
        private Organization _parentOrganization;
        private int _id;
        public Department(int id, Organization parentOrganization)
        {
            _id = id;
            _parentOrganization = parentOrganization;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public Organization ParentOrganization
        {
            get
            {
                return _parentOrganization;
            }
        }

        public string Name { get; set; }

        public new int GetEntityCode()
        {
            return 1;
        }


    }
}
