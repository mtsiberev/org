using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class AdministrationViewModel : BaseViewModel
    {
        public AdministrationViewModel(int currentPageNumber, int maxPageNumber, List<EmployeeViewModel> content, string sortType)
        {
            Content = content;
            viewCondition.CurrentPageNumber = currentPageNumber;
            viewCondition.MaxPageNumber = maxPageNumber;
            viewCondition.PageType = "admin_info";
            viewCondition.SortType = sortType;
        }
        public List<EmployeeViewModel> Content { get; set; }
    }
}