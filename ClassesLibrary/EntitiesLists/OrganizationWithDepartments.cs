using System.Collections.Generic;
using Organizations.Container;

namespace Organizations.EntitiesLists
{
    public class OrganizationWithDepartments : MainList
    {
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();
        public List<Department> Content { get; private set; }

        public OrganizationWithDepartments(int organizationId, int currentPage, string sortType)
        {
            Id = organizationId;

            Init(currentPage, sortType);
        }

        protected override void RefreshMaxPage()
        {
            var entitiesCount = m_facade.GetDepartmentsCount(Id);
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        protected override void RefreshContent()
        {
            Content = m_facade.GetDepartmentsForOnePage(CurrentPage, PageSize, Id, SortType);
            Name = m_facade.GetOrganizationById(Id).Name;
        }
    }
}