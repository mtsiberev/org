using System;
using System.Collections.Generic;
using System.Linq;
using Organizations;
using Organizations.Helpers;

namespace OrganizationsWebApplication.Models
{
    public class DepartmentWithEmployeesViewModel : MainModel
    {
        public List<EmployeeViewModel> Content { get; private set; }
        
        public DepartmentWithEmployeesViewModel(Facade facade, int parentId, int departmentId, string name, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, int pageNumberInDepartmentInfo, string viewType, string sortType)
        {
            Id = departmentId;
            ParentId = parentId;
            Name = name;
            PageNumberInOrganizationsList = pageNumberInOrganizationsList;
            PageNumberInOrganizationInfo = pageNumberInOrganizationInfo;

            RefreshMaxPage(facade);

            if (pageNumberInDepartmentInfo <= 0)
            {
                PageNumberInDepartmentInfo = 1;
            }
            else if (pageNumberInDepartmentInfo >= MaxPageQty)
            {
                PageNumberInDepartmentInfo = MaxPageQty;
            }
            else
            {
                PageNumberInDepartmentInfo = pageNumberInDepartmentInfo;
            }
            
            PageType = "dep_info";
            ViewType = viewType;
            SortType = sortType;
            
            RefreshContent(facade);
            OwnersList = OwnershipHelper.GetOwnersListForCurrentUser();
        }

        private void RefreshMaxPage(Facade facade)
        {
            int entitiesCount = facade.GetEmployeesCount(Id);
            MaxPageQty = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageQty++;

            if (MaxPageQty == 0) MaxPageQty++;
        }

        private void RefreshContent(Facade facade)
        {
            var employees =
                from employee in facade.GetEmployeesForOnePage(PageNumberInDepartmentInfo, PageSize, Id, SortType)
                select new EmployeeViewModel() { Name = String.Join(" ", employee.Name, employee.LastName), Id = employee.Id };
            Content = employees.ToList();
        }
    }
}