using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Department
    {

        public Department(int id) { Id = id; employees = new List<Employee>(); }
        public int Id { get; private set; }

        public string Name { get; set; }
        public List<Employee> employees;
        /*
        public void AddEmployee(string name, int age, string city, string street)
        {
            this.employees.Add(new Employee(this.employees.Count) { Name = name, Age = age, City = city, Street = street });
        }
        */
        public int GetNewEmployeeId()
        {
            return this.employees.Count;
        }

        public void AddEmployee(Employee employee)
        {
            this.employees.Add(employee);
        }
    }
}
