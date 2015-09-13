using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class AdministrationViewModel : BaseViewModel
    {
        public List<EmployeeViewModel> Content { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
    }
}