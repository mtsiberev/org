
namespace OrganizationsWebApplication.Models
{
    public class EmployeeViewModel : IModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }

        public int PageNumberInOrganizationsList { get; set; }
        public int PageNumberInOrganizationInfo { get; set; }
        public int PageNumberInDepartmentInfo { get; set; }
    }
}