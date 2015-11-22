using System.Collections.Generic;
using Organizations.Container;

namespace Organizations.EntitiesLists
{
    public class OrganizationsList : MainList
    {
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();
        public List<Organization> Content { get; private set; }

        public OrganizationsList(int currentPage, string sortType)
        {
            Init(currentPage, sortType);
        }

        protected override void RefreshMaxPage()
        {
            var entitiesCount = m_facade.GetOrganizationsCount();
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        protected override void RefreshContent()
        {
            Content = m_facade.GetOrganizationsForOnePage(CurrentPage, PageSize, SortType);
        }
    }

}