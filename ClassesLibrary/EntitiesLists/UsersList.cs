using System.Collections.Generic;
using System.Linq;

namespace Organizations.EntitiesLists
{
    public class UsersList : MainList
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        public List<Employee> Content { get; private set; }
        public int ParentId { get; protected set; }

        public UsersList(int currentPage, string sortType)
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
            var usersList = m_facade.GetAllEmployees().ToList();
            var entitiesCount = usersList.ToList().Count;
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        private void RefreshContent()
        {
            Content = m_facade.GetEmployeesForOnePage(CurrentPage, PageSize, 0, SortType);
        }
    }
}




