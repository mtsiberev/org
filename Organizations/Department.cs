using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Department : IEntity
    {
        public Department(int id) { Id = id; employees = new List<Employee>(); }
        public int Id { get; private set; }

        public string Name { get; set; }
        private List<Employee> employees;

        public int GetNewEmployeeId()
        {
            return this.employees.Count;
        }
        
        public void Show() { }
        
        public int GetId() { return Id; }
        
        public void AddEmployee(Employee employee)
        {
            this.employees.Add(employee);
        }

        public Employee GetEmployeeById(int index)
        {
            return this.employees.Find(x => x.Id == index);
        }


        public List<Employee> GetAllEmployees()
        {
            return employees;
        }


    }
}
