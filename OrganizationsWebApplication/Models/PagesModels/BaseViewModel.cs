
namespace OrganizationsWebApplication.Models.PagesModels
{
    public class BaseViewModel
    {
        public int Id;
        public string SortType;
        public int CurrentPageNumber;
        public int MaxPageNumber;
        public string PageType;
        public string ViewType = "list";
        public int ParentId;
    }
}