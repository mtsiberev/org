using System.Collections.Generic;

namespace OrganizationsWebApplication.Models
{
    public class OrganizationWithDepartmentsViewModel
    {
        public Page Page { get; set; }
        public int Id { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
        public string Name { get; set; }
    }
}