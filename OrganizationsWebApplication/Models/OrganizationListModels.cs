using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;

namespace OrganizationsWebApplication.Models
{
    public class DtoOrganization
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class OrganizationListModels
    {
        public List<DtoOrganization> Organizations {  get;  private set; }

        public OrganizationListModels(List<DtoOrganization> organizationsList)
        {
            Organizations = organizationsList;
        }
        
    }
}
