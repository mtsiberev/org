using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Organization : IEntity
    {
        public int Id { get; private set; }
        public Organization(int id)
        {
            Id = id;
            departments = new List<Department>();
        }

        public int GetId()
        {
            return Id; 
        }

        public void Show() { }
        
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
