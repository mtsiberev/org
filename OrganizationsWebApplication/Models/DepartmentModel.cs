using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class SimpleEmployee
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class DepartmentModel
    {
        public List<SimpleEmployee> Employees { get; private set; }
        public string Name;

        public DepartmentModel(List<SimpleEmployee> employeeList, string name)
        {
            Employees = employeeList;
            Name = name;
        }

    }
}