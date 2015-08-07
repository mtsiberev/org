using System.Collections.Generic;
using System.Linq;
using Organizations;

namespace OrganizationsWebApplication.Models
{
    public class OrganizationsList : MainModel
    {
        public List<OrganizationViewModel> Content { get; set; }
        
        public OrganizationsList(Facade facade, int pageNumberInOrganizationsList)
        {
            RefreshMaxPage(facade);

            if (pageNumberInOrganizationsList <= 0)
            {
                PageNumberInOrganizationsList = 1;
            }
            else if (pageNumberInOrganizationsList >= MaxPageQty)
            {
                PageNumberInOrganizationsList = MaxPageQty;
            }
            else
            {
                PageNumberInOrganizationsList = pageNumberInOrganizationsList;
            }

            RefreshContent(facade);
            PageType = "org_list";
        }

        private void RefreshMaxPage(Facade facade)
        {
            var entitiesCount = facade.GetOrganizationsCount();
            MaxPageQty = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageQty++;

            if (MaxPageQty == 0) MaxPageQty++;
        }

        private void RefreshContent(Facade facade)
        {
            var organizations =
                 from organization in facade.GetOrganizationsForOnePage(PageNumberInOrganizationsList, PageSize)
                 select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
            Content = organizations.ToList();
        }
    }

}