
namespace OrganizationsWebApplication.Models
{
    public class OrganizationViewModel : IModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public int PageNumberInOrganizationsList { get; set; }
    }
}
