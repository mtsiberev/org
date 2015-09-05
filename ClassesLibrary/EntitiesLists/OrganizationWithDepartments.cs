using System.Collections.Generic;

namespace Organizations.EntitiesLists
{
    public class OrganizationWithDepartments : MainList
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        public List<Department> Content { get; private set; }

        public OrganizationWithDepartments(int organizationId, int currentPage, string sortType)
        {
            Id = organizationId;
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
            var entitiesCount = m_facade.GetDepartmentsCount(Id);
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        private void RefreshContent()
        {
            Content = m_facade.GetDepartmentsForOnePage(CurrentPage, PageSize, Id, SortType);
            Name = m_facade.GetOrganizationById(Id).Name;
        }
    }
}