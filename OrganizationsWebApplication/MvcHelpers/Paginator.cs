using System;
using System.Web;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.MvcHelpers
{
    public class Paginator
    {
        private const int pageSize = 6;
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

        public static Page GetPageObject(string pageType, int entitiesCount, int currentId, int parentId)
        {
            var maxPageCount = entitiesCount / pageSize;
            if ( (entitiesCount % pageSize) != 0) maxPageCount++;
            var currentPage = int.Parse(GetCurrentPage(pageType));
            if (currentPage > maxPageCount) currentPage--;
            if (currentPage == 0) currentPage = 1;

            return new Page(pageSize, currentPage, maxPageCount, pageType, currentId, parentId);
        }

        public static Page GetOrganizationsListPage(Facade facade)
        {
            const string pageType = "organizationsPage";
            int organizationsCount = facade.GetOrganizationsCount();
       
            return GetPageObject(pageType, organizationsCount, -1, -1);
        }

        public static Page GetDepartmentsListPage(Facade facade, int id)
        {
            const string pagetype = "departmentsPage";
            int departmentsCount = facade.GetDepartmentsCount(id);

            return GetPageObject(pagetype, departmentsCount, id, -1);
        }

        public static Page GetEmployeesListPage(Facade facade, int id)
        {
            const string pagetype = "employeesPage";
            int parentId = facade.GetDepartmentById(id).ParentOrganization.Id;
            int employeesCount = facade.GetEmployeesCount(id);

            return GetPageObject(pagetype, employeesCount, id, parentId);
        }
    }
}