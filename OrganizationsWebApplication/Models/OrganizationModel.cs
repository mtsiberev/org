using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class SimpleDepartment
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    
    public class OrganizationModel
    {
        public List<SimpleDepartment> Departments { get; private set; }
        public string Name;

        public OrganizationModel(List<SimpleDepartment> organizationsList, string name)
        {
            Departments = organizationsList;
            Name = name;
        }

    }
}