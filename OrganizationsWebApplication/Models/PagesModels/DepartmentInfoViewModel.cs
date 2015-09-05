using System.Collections.Generic;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Models.PagesModels
{
    public class DepartmentInfoViewModel : BaseViewModel
    {
        public DepartmentInfoViewModel(int organizationId, int id, int currentPageNumber, int maxPageNumber, List<EmployeeViewModel> content)
        {
            ParentId = organizationId;
            Id = id;
            Content = content;
            CurrentPageNumber = currentPageNumber;
            MaxPageNumber = maxPageNumber;
            PageType = "dep_info";
        }

        public string Name;
        public List<EmployeeViewModel> Content { get; private set; }
    }
}