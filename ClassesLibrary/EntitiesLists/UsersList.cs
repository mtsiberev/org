using System.Collections.Generic;
using System.Linq;
using Organizations.Container;

namespace Organizations.EntitiesLists
{
    public class UsersList : MainList
    {
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();
        public List<Employee> Content { get; private set; }
        public int ParentId { get; protected set; }

        public UsersList(int currentPage, string sortType)
        {
            Init(currentPage, sortType);
        }

        protected override void RefreshMaxPage()
        {
            var usersList = m_facade.GetAllEmployees().ToList();
            var entitiesCount = usersList.ToList().Count;
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        protected override void RefreshContent()
        {
            Content = m_facade.GetEmployeesForOnePage(CurrentPage, PageSize, 0, SortType);
        }
    }
}




