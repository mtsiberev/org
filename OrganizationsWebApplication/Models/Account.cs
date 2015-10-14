
using System.ComponentModel.DataAnnotations;

namespace OrganizationsWebApplication.Models
{
    public class Account
    {
        public Account()
        {
            OrganizationId = 0;
            DepartmentId = 0;
        }

    
        [MinLength(5, ErrorMessage = "UserName cannot be shorter than 5 characters.")]
        [MaxLength(20, ErrorMessage = "UserName cannot be longer than 20 characters.")]
        public string UserName { get; set; }

        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be longer than 20 characters.")]
        public string Password { get; set; }

        public int OrganizationId { get; set; }
        public int DepartmentId { get; set; }
    }
}