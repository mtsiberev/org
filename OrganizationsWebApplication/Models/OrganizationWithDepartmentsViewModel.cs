using System.Collections.Generic;
using System.Linq;
using Organizations;

namespace OrganizationsWebApplication.Models
{
    public class OrganizationWithDepartmentsViewModel : MainModel
    {
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
        }

        public List<DepartmentViewModel> Content { get; private set; }

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