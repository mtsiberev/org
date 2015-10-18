using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class OrganizationInfoViewModel : BaseViewModel
    {
        public OrganizationInfoViewModel(int organizationId, int currentPageNumber, int maxPageNumber, List<DepartmentViewModel> content, string name, string sortType)
        {
            viewCondition.Id = organizationId;
            Content = content;
            viewCondition.CurrentPageNumber = currentPageNumber;
            Name = name;
            viewCondition.SortType = sortType;
            viewCondition.MaxPageNumber = maxPageNumber;
            viewCondition.PageType = "org_info";
        }
        
        public string Name;
        public List<DepartmentViewModel> Content { get; private set; }
    }
}