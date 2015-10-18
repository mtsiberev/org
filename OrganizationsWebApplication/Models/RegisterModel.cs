using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizationsWebApplication.LocalResource;

namespace OrganizationsWebApplication.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            OrganizationId = 0;
            DepartmentId = 0;
        }

        [Remote("IsUserNameExist", "Account", HttpMethod = "POST",
            ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "UserExists"
            )]
        public string UserName { get; set; }

        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "PasswordConfirmationError"
            )]
        public string ConfirmPassword { get; set; }

        public int OrganizationId { get; set; }
        public int DepartmentId { get; set; }
    }
}