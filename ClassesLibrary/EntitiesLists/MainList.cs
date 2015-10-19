namespace Organizations.EntitiesLists
{
    public abstract class MainList
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected const int PageSize = 6;
        public int CurrentPage { get; protected set; }
        public int MaxPageNumber { get; protected set; }
        public string SortType { get; protected set; }

        protected abstract void RefreshMaxPage();
        protected abstract void RefreshContent();

        protected void Init(int currentPage, string sortType)
        {
            RefreshMaxPage();
            if (currentPage <= 0)
            {
                CurrentPage = 1;
            }
            else if (currentPage >= MaxPageNumber)
            {
                CurrentPage = MaxPageNumber;
            }
            else
            {
                CurrentPage = currentPage;
            }

            SortType = sortType;
            RefreshContent();
        }
    }
}