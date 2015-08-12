using System.Collections.Generic;
using System.Linq;
using Organizations;
using Organizations.Helpers;

namespace OrganizationsWebApplication.Models
{
    public class OrganizationsList : MainModel
    {
        public List<OrganizationViewModel> Content { get; private set; }
        public List<KeyValuePair<int, int>> OwnersList { get; private set; }

        public OrganizationsList(Facade facade, int pageNumberInOrganizationsList, string viewType, string sortType)
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

            PageType = "org_list";
            ViewType = viewType;
            SortType = sortType;

            RefreshContent(facade);
            OwnersList = OwnershipHelper.GetOwnersListForCurrentUser();
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
                 from organization in facade.GetOrganizationsForOnePage(PageNumberInOrganizationsList, PageSize, SortType)
                 select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
            Content = organizations.ToList();
        }
    }

}