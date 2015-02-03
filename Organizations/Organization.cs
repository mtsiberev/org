using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Organization : IEntity
    {
        private int id;   
        public Organization(int argumentId){ id = argumentId; departments = new List<Department>(); }
        
        public int Id
        {
            get
            {
                return id;
            }
        }    
        
        public string Name { get; set; }
        private List<Department> departments;
        
        public int GetNewDepartmentId()
        {
            return this.departments.Count;
        }

        public void AddDepartment(Department department)
        {
            this.departments.Add(department);
        }

        public Department GetDepartmentById(int index)
        {
            return this.departments.Find(x => x.Id == index);
        }

        public List<Department> GetAllDepartments()
        {
            return departments;
        }

    }
}
