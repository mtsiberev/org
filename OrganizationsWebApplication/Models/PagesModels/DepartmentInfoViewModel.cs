using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class DepartmentInfoViewModel : BaseViewModel
    {

        public DepartmentInfoViewModel()
        {
        }

        public DepartmentInfoViewModel(int organizationId, int id, int currentPageNumber, int maxPageNumber, List<EmployeeViewModel> content, string sortType)
        {
            viewConditionProperty.ParentId = organizationId;
            viewConditionProperty.Id = id;
            Content = content;
            viewConditionProperty.CurrentPageNumber = currentPageNumber;
            viewConditionProperty.MaxPageNumber = maxPageNumber;
            viewConditionProperty.PageType = "dep_info";
            viewConditionProperty.SortType = sortType;
        }

        public string Name;

        public List<EmployeeViewModel> Content { get; set; }
        public FreeUsersViewModel FreeUsersViewModel { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
    }
}