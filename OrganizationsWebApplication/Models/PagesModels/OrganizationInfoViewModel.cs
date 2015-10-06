using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationInfoViewModel : BaseViewModel
    {
        public OrganizationInfoViewModel(int organizationId, int currentPageNumber, int maxPageNumber, List<DepartmentViewModel> content, string name, string sortType)
        {
            viewConditionProperty.Id = organizationId;
            Content = content;
            viewConditionProperty.CurrentPageNumber = currentPageNumber;
            Name = name;
            viewConditionProperty.SortType = sortType;
            viewConditionProperty.MaxPageNumber = maxPageNumber;
            viewConditionProperty.PageType = "org_info";
        }
        
        public string Name;
        public List<DepartmentViewModel> Content { get; private set; }
    }
}