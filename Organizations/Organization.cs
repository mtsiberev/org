using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Organization
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<Department> departments;
        public Organization(int id)
        {
            Id = id;
            departments = new List<Department>();
        }

        public int GetNewDepartamentId()
        {
            return this.departments.Count;
        }

        public void AddDepartment(Department departament)
        {
            this.departments.Add(departament);
        }

    }
}
