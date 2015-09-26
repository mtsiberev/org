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
            ViewStateProperty.ParentId = organizationId;
            ViewStateProperty.Id = id;
            Content = content;
            ViewStateProperty.CurrentPageNumber = currentPageNumber;
            ViewStateProperty.MaxPageNumber = maxPageNumber;
            ViewStateProperty.PageType = "dep_info";
            ViewStateProperty.SortType = sortType;
        }

        public string Name;

        public List<EmployeeViewModel> Content { get; set; }
        public FreeUsersViewModel FreeUsersViewModel { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
    }
}