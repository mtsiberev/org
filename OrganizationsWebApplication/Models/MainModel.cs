using Organizations;

namespace OrganizationsWebApplication.Models
{
    public abstract class MainModel
    {
        public int Id { get; protected set; }
        public int ParentId { get; protected set; }
        public string Name { get; protected set; }

        public int PageNumberInOrganizationsList { get; protected set; }
        public int PageNumberInOrganizationInfo { get; protected set; }
        public int PageNumberInDepartmentInfo { get; protected set; }


        public const int PageSize = 6;
        public string PageType { get; protected set; }

        public int MaxPageQty { get; protected set; }
        public abstract void RefreshMaxPage(Facade facade);
        public abstract void RefreshContent(Facade facade);

        public string SortType;
        public string ViewType;
        public string ModelType;
    }
}