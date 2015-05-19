using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.Models
{
    public class DtoEmployee
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentModel
    {
        public int Id { get; set; }
        public List<DtoEmployee> Employees { get;  set; }
        public string Name { get; set; }
    }
}