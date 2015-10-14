using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class AdministrationViewModel : BaseViewModel
    {
        public AdministrationViewModel(int currentPageNumber, int maxPageNumber, List<EmployeeViewModel> content, string sortType)
        {
            Content = content;
            viewConditionProperty.CurrentPageNumber = currentPageNumber;
            viewConditionProperty.MaxPageNumber = maxPageNumber;
            viewConditionProperty.PageType = "admin_info";
            viewConditionProperty.SortType = sortType;
        }

        public List<EmployeeViewModel> Content { get; set; }
    }
}