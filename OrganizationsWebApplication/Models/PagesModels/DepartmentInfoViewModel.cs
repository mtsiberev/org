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
            viewCondition.ParentId = organizationId;
            viewCondition.Id = id;
            Content = content;
            viewCondition.CurrentPageNumber = currentPageNumber;
            viewCondition.MaxPageNumber = maxPageNumber;
            viewCondition.PageType = "dep_info";
            viewCondition.SortType = sortType;
        }

        public string Name;

        public List<EmployeeViewModel> Content { get; set; }
        public List<EmployeeViewModel> FreeUsersViewModel { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
    }
}