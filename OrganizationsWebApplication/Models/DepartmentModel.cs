using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class DtoEmployee
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class DepartmentModel
    {
        public List<DtoEmployee> Employees { get; private set; }
        public string Name;

        public DepartmentModel(List<DtoEmployee> employeesList, string name)
        {
            Employees = employeesList;
            Name = name;
        }

    }
}