using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationsListViewModel : BaseViewModel
    {
        public OrganizationsListViewModel(int currentPageNumber, int maxPageNumber, List<OrganizationViewModel> content, string sortType)
        {
            Content = content;
            CurrentPageNumber = currentPageNumber;
            SortType = sortType;
            MaxPageNumber = maxPageNumber;
            PageType = "org_list";
        }

        public List<OrganizationViewModel> Content { get; private set; }
    }
}