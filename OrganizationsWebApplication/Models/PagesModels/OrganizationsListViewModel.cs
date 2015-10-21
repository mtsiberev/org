using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationsListViewModel : BaseViewModel
    {
        public OrganizationsListViewModel(int currentPageNumber, int maxPageNumber, List<OrganizationViewModel> content, string sortType)
        {
            viewCondition = new ViewCondition();
            Content = content;
            viewCondition.CurrentPageNumber = currentPageNumber;
            viewCondition.SortType = sortType;
            viewCondition.MaxPageNumber = maxPageNumber;
            viewCondition.PageType = "org_list";
        }
        public List<OrganizationViewModel> Content { get; private set; }
    }
}