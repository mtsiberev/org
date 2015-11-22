using System.Collections.Generic;
using Organizations.Container;

namespace Organizations.EntitiesLists
{
    public class DepartmentWithEmployees : MainList
    {
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();
        public List<Employee> Content { get; private set; }
        public int ParentId { get; protected set; }

        public DepartmentWithEmployees(int departmentId, int currentPage, string sortType)
        {   
            Id = departmentId;
            var department = m_facade.GetDepartmentById(Id);
            Name = department.Name;
            ParentId = department.ParentOrganization.Id;

            Init(currentPage, sortType);
        }

        protected override void RefreshMaxPage()
        {
            var entitiesCount = m_facade.GetEmployeesCount(Id);
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        protected override void RefreshContent()
        {
            Content = m_facade.GetEmployeesForOnePage(CurrentPage, PageSize, Id, SortType);
        }
    }
}