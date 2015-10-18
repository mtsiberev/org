using System.Web.Mvc;
using OrganizationsWebApplication.LocalResource;

namespace OrganizationsWebApplication.Models
{
    public class Account
    {
        [Remote("IsUserNameNotExist", "Account", HttpMethod = "POST",
            ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "UserNotExists"
            )]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}