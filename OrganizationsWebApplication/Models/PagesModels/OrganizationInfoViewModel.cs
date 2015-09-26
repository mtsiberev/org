using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationInfoViewModel : BaseViewModel
    {
        public OrganizationInfoViewModel(int organizationId, int currentPageNumber, int maxPageNumber, List<DepartmentViewModel> content, string name, string sortType)
        {
            ViewStateProperty.Id = organizationId;
            Content = content;
            ViewStateProperty.CurrentPageNumber = currentPageNumber;
            Name = name;
            ViewStateProperty.SortType = sortType;
            ViewStateProperty.MaxPageNumber = maxPageNumber;
            ViewStateProperty.PageType = "org_info";
        }
        
        public string Name;
        public List<DepartmentViewModel> Content { get; private set; }
    }
}