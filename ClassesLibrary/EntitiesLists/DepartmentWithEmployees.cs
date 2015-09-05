﻿using System.Collections.Generic;

namespace Organizations.EntitiesLists
{
    public class DepartmentWithEmployees : MainList
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        public List<Employee> Content { get; private set; }
        public int ParentId { get; protected set; }

        public DepartmentWithEmployees(int departmentId, int currentPage, string sortType)
        {   
            Id = departmentId;
            var department = m_facade.GetDepartmentById(Id);
            Name = department.Name;
            ParentId = department.ParentOrganization.Id;
          
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
            var entitiesCount = m_facade.GetEmployeesCount(Id);
            MaxPageNumber = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageNumber++;
            if (MaxPageNumber == 0) MaxPageNumber++;
        }

        private void RefreshContent()
        {
            Content = m_facade.GetEmployeesForOnePage(CurrentPage, PageSize, Id, SortType);
        }
    }
}