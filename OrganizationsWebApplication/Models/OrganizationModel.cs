using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class DtoDepartment
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }

    public class OrganizationModel
    {
        public int Id;
        public List<DtoDepartment> Departments { get; private set; }
        public string Name;

        public OrganizationModel(int id, List<DtoDepartment> departmentsList, string name)
        {
            Id = id;
            Departments = departmentsList;
            Name = name;
        }

    }
}