
namespace OrganizationsWebApplication.Models
{
    public class Account
    {
        public Account()
        {
            OrganizationId = 0;
            DepartmentId = 0;
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public int OrganizationId { get; set; }
        public int DepartmentId { get; set; }
    }
}