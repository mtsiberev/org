using System.Collections.Generic;

namespace OrganizationsWebApplication.Models
{
    public class ListOfOrganizationsViewModel
    {
        public List<OrganizationViewModel> Organizations {  get;  private set; }

        public ListOfOrganizationsViewModel(List<OrganizationViewModel> organizationsList)
        {
            Organizations = organizationsList;
        }
        
    }
}