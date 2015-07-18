using System.Collections.Generic;
using OrganizationsWebApplication.MvcHelpers;

namespace OrganizationsWebApplication.Models
{
    public class ListOfOrganizationsViewModel
    {
        public List<OrganizationViewModel> Organizations { get; private set; }

        public Page Page { get; private set; }

        public ListOfOrganizationsViewModel(List<OrganizationViewModel> organizationsList, Page page)
       {
            Organizations = organizationsList;
            Page = page;
        }
    }
}