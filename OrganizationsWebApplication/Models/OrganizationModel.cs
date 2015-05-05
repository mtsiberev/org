using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class DtoDepartment
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    
    public class OrganizationModel
    {
        public List<DtoDepartment> Departments { get; private set; }
        public string Name;

        public OrganizationModel(List<DtoDepartment> departmentsList, string name)
        {
            Departments = departmentsList;
            Name = name;
        }

    }
}