using System;
using System.Web;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.MvcHelpers
{
    public class Paginator
    {
        /*
        private const int c_pageSize = 6;

        public static void ClearSession(string pageType)
        {
            HttpContext.Current.Session.Remove(pageType);
        }

        private static string GetCurrentPage(string pageType)
        {
            var valueString = HttpContext.Current.Session[pageType] as String;
            if (valueString != null) return HttpContext.Current.Session[pageType] as String;

            const string startPageValue = "1";
            HttpContext.Current.Session[pageType] = startPageValue;
            return startPageValue;
        }

        public static Page GetPageObject(string pageType, int entitiesCount)
        {
            var maxPageCount = entitiesCount / c_pageSize;
            if ((entitiesCount % c_pageSize) != 0) maxPageCount++;
            var currentPage = int.Parse(GetCurrentPage(pageType));
            if (currentPage > maxPageCount) currentPage--;
            if (currentPage == 0) currentPage = 1;

            return new Page(c_pageSize, currentPage, maxPageCount);
        }



        public static Page GetFirstOrganizationsListPage(Facade facade)
        {
            int entitiesCount = facade.GetOrganizationsCount();
            var maxPageCount = entitiesCount / c_pageSize;
            if ((entitiesCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount, "org_list");
        }
        

        public static Page GetFirstOrganizationInfoPage(Facade facade, int organizationId)
        {
            int entitiesCount = facade.GetDepartmentsCount(organizationId);
            var maxPageCount = entitiesCount / c_pageSize;
            if ((entitiesCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount, "org_info");
        }
        

        public static Page GetFirstDepartmentInfoPage(Facade facade, int departmentInfo)
        {
            int entitiesCount = facade.GetEmployeesCount(departmentInfo);
            var maxPageCount = entitiesCount / c_pageSize;
            if ((entitiesCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount, "dep_info");
        }

      
        public static Page GetNextPage(Page page, Facade facade, string type, int parentId)
        {
            int nextPageNumber = page.CurrentPageNumber + 1;
            return GetPage(facade, type, parentId, nextPageNumber);
        }


        public static Page GetPage(Facade facade, string type, int parentId, int currentPage)
        {
            int entitiesCount = 0;
            if (type == "org_list")
            {
                entitiesCount = facade.GetOrganizationsCount();
            }
            if (type == "org_info")
            {
                entitiesCount = facade.GetDepartmentsCount(parentId);
            }
            if (type == "dep_info")
            {
                entitiesCount = facade.GetEmployeesCount(parentId);
            }
            var maxPageCount = entitiesCount / c_pageSize;
            if ((entitiesCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, currentPage, maxPageCount, type);
        }


    */


        /*
        public static Page GetOrganizationsListPage(Facade facade)
        {
            int organizationsCount = facade.GetOrganizationsCount();
            var maxPageCount = organizationsCount / c_pageSize;
            if ((organizationsCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount);
        }

        public static Page GetDepartmentsListPage(Facade facade, int organizationId)
        {
            int departmentsCount = facade.GetDepartmentsCount(organizationId);
            var maxPageCount = departmentsCount / c_pageSize;
            if ((departmentsCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount);
        }

        public static Page GetEmployeesListPage(Facade facade, int departmentId)
        {
            int employeesCount = facade.GetEmployeesCount(departmentId);
            var maxPageCount = employeesCount / c_pageSize;
            if ((employeesCount % c_pageSize) != 0) maxPageCount++;

            return new Page(c_pageSize, 1, maxPageCount);
        }
        */
    }
}