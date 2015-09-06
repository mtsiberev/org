
using System.Collections.Generic;
using Organizations;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class RegisterViewModel
    {
        public Account Account { get; set; }
        public List<OrganizationViewModel> OrganizationsList { get; set; }
        public List<DepartmentViewModel> DepartmentsList { get; set; }
    }
}