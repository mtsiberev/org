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
        public int Id { get; set; }
        public List<DtoDepartment> Departments { get; set; }
        public string Name { get; set; }
    }
}