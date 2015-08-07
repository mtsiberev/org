
namespace OrganizationsWebApplication.Models
{
    public class EmployeeViewModel : IModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}