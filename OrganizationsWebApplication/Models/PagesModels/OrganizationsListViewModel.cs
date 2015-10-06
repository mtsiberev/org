using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationsListViewModel : BaseViewModel
    {
        public OrganizationsListViewModel(int currentPageNumber, int maxPageNumber, List<OrganizationViewModel> content, string sortType)
        {
            viewConditionProperty = new ViewCondition();
            Content = content;
            viewConditionProperty.CurrentPageNumber = currentPageNumber;
            viewConditionProperty.SortType = sortType;
            viewConditionProperty.MaxPageNumber = maxPageNumber;
            viewConditionProperty.PageType = "org_list";
        }

        public List<OrganizationViewModel> Content { get; private set; }
    }
}