
namespace OrganizationsWebApplication.Models
{
    public class ViewCondition
    {
        public int Id { get; set; }
        public string SortType { get; set; }
        public int CurrentPageNumber { get; set; }
        public int MaxPageNumber { get; set; }
        public string PageType { get; set; }
        public string ViewType = "list";
        public int ParentId { get; set; }
    }
}