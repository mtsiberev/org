using OrganizationsWebApplication.Models.PagesModels;

namespace OrganizationsWebApplication.Models.EntitiesModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}