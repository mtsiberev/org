using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationsListViewModel : BaseViewModel
    {
        public OrganizationsListViewModel(int currentPageNumber, int maxPageNumber, List<OrganizationViewModel> content, string sortType)
        {
            ViewStateProperty = new ViewState();
            Content = content;
            ViewStateProperty.CurrentPageNumber = currentPageNumber;
            ViewStateProperty.SortType = sortType;
            ViewStateProperty.MaxPageNumber = maxPageNumber;
            ViewStateProperty.PageType = "org_list";
        }

        public List<OrganizationViewModel> Content { get; private set; }
    }
}