using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationInfoViewModel : BaseViewModel
    {
        public OrganizationInfoViewModel(int organizationId, int currentPageNumber, int maxPageNumber, List<DepartmentViewModel> content, string name, string sortType)
        {
            Id = organizationId;
            Content = content;
            CurrentPageNumber = currentPageNumber;
            Name = name;
            SortType = sortType;
            MaxPageNumber = maxPageNumber;
            PageType = "org_info";
        }
        
        public string Name;
        public List<DepartmentViewModel> Content { get; private set; }
    }
}