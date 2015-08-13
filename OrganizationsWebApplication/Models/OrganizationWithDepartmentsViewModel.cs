using System.Collections.Generic;
using System.Linq;
using Organizations;
using Organizations.Helpers;

namespace OrganizationsWebApplication.Models
{
    public class OrganizationWithDepartmentsViewModel : MainModel
    {
        public List<DepartmentViewModel> Content { get; private set; }
      
        public OrganizationWithDepartmentsViewModel(Facade facade, int organizationId, string name, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
        {
            Id = organizationId;
            Name = name;
            PageNumberInOrganizationsList = pageNumberInOrganizationsList;
            RefreshMaxPage(facade);

            if (pageNumberInOrganizationInfo <= 0)
            {
                PageNumberInOrganizationInfo = 1;
            }
            else if (pageNumberInOrganizationInfo >= MaxPageQty)
            {
                PageNumberInOrganizationInfo = MaxPageQty;
            }
            else
            {
                PageNumberInOrganizationInfo = pageNumberInOrganizationInfo;
            }
            
            PageType = "org_info";
            ViewType = viewType;
            SortType = sortType;
            
            RefreshContent(facade);
            OwnersList = OwnershipHelper.GetOwnersListForCurrentUser();
        }

      
        private void RefreshMaxPage(Facade facade)
        {
            var entitiesCount = facade.GetDepartmentsCount(Id);
            MaxPageQty = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageQty++;

            if (MaxPageQty == 0) MaxPageQty++;
        }

        private void RefreshContent(Facade facade)
        {
            var departments =
              from department in facade.GetDepartmentsForOnePage(PageNumberInOrganizationInfo, PageSize, Id, SortType)
              select new DepartmentViewModel() { Name = department.Name, Id = department.Id };

            Content = departments.ToList();
        }
    }
}