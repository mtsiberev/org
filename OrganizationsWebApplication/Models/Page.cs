namespace OrganizationsWebApplication.Models
{
    public class Page
    {
        public Page() { }

        public Page(int pageSize, int currentPageNumber, int maxNumber, string pagetype, int currentInstanceId, int parentInstanceId)
        {
            PageSize = pageSize;
            PageType = pagetype;
            MaxPageNumber = maxNumber;
            CurrentPageNumber = currentPageNumber;
            CurrentInstanceId = currentInstanceId;
            ParentInstanceId = parentInstanceId;
        }
        
        public int PageSize { get; set; }
        public int CurrentPageNumber { get; set; }
        public int MaxPageNumber { get; set; }
        public string PageType { get; set; }
        public int CurrentInstanceId { get; set; }
        public int ParentInstanceId { get; set; }
        
        public void GoNextPage()
        {
            CurrentPageNumber++;
            if (CurrentPageNumber > MaxPageNumber) CurrentPageNumber = MaxPageNumber;
            System.Web.HttpContext.Current.Session[PageType] = CurrentPageNumber.ToString();
        }

        public void GoPrevPage()
        {
            CurrentPageNumber--;
            if (CurrentPageNumber == 0) CurrentPageNumber = 1;
            System.Web.HttpContext.Current.Session[PageType] = CurrentPageNumber.ToString();
        }
    }
}