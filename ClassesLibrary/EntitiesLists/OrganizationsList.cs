using System.Collections.Generic;

namespace Organizations.EntitiesLists
{
    public class OrganizationsList : MainList
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        public List<Organization> Content { get; private set; }

        public OrganizationsList(int currentPage, string sortType)
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

        private void RefreshMaxPage()
        {
            var entitiesCount = m_facade.GetOrganizationsCount();
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        private void RefreshContent()
        {
            Content = m_facade.GetOrganizationsForOnePage(CurrentPage, PageSize, SortType);
        }
    }

}