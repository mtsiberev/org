﻿using System;
using System.Collections.Generic;
using System.Linq;
using Organizations;

namespace OrganizationsWebApplication.Models
{
    public class DepartmentWithEmployeesViewModel : MainModel
    {
        public List<EmployeeViewModel> Content { get; private set; }

        public DepartmentWithEmployeesViewModel(Facade facade, int parentId, int departmentId, string name, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, int pageNumberInDepartmentInfo)
        {
            Id = departmentId;
            ParentId = parentId;
            Name = name;
            PageNumberInOrganizationsList = pageNumberInOrganizationsList;
            PageNumberInOrganizationInfo = pageNumberInOrganizationInfo;

            RefreshMaxPage(facade);

            if (pageNumberInDepartmentInfo <= 0)
            {
                PageNumberInDepartmentInfo = 1;
            }
            else if (pageNumberInDepartmentInfo >= MaxPageQty)
            {
                PageNumberInDepartmentInfo = MaxPageQty;
            }
            else
            {
                PageNumberInDepartmentInfo = pageNumberInDepartmentInfo;
            }

            RefreshContent(facade);
            PageType = "dep_info";
        }

        public override sealed void RefreshMaxPage(Facade facade)
        {
            int entitiesCount = facade.GetEmployeesCount(Id);
            MaxPageQty = entitiesCount / PageSize;
            if ((entitiesCount % PageSize) != 0) MaxPageQty++;

            if (MaxPageQty == 0) MaxPageQty++;
        }

        public override sealed void RefreshContent(Facade facade)
        {
            var employees =
                from employee in facade.GetEmployeesForOnePage(PageNumberInDepartmentInfo, PageSize, Id)
                select new EmployeeViewModel() { Name = String.Join(" ", employee.Name, employee.LastName), Id = employee.Id };
            Content = employees.ToList();
        }
    }
}