using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.MvcHelpers
{
    public class Paginator
    {
        private static int pageSize = 6;

        public static Page GetPageObject(int entitiesQty)
        {
            SetPageSize();
            SetMaxPageNumber(entitiesQty);
            return new Page(pageSize, GetCurrentPageNumber());
        }

        private static void SetPageSize()
        {
            var pageSizeCookie = new HttpCookie("pageSize") { Value = pageSize.ToString() };
            HttpContext.Current.Response.Cookies.Add(pageSizeCookie);
        }
        
        private static int GetCurrentPageNumber()
        {
            if (HttpContext.Current.Request.Cookies["pageNumber"] == null)
            {
                HttpContext.Current.Response.Cookies["pageNumber"].Value = "1";
            }

            var stringPageNumber = HttpContext.Current.Request.Cookies["pageNumber"].Value;
            return Convert.ToInt32(stringPageNumber);
        }
        

        private static void SetMaxPageNumber(int entitiesQty)
        {
            var entitiesCount = entitiesQty;
            var maxPageCount = entitiesCount / pageSize;
            if ((entitiesCount % pageSize) != 0) maxPageCount++;

            if (HttpContext.Current.Request.Cookies["maxPageNumber"] == null)
            {
                var maxPageNumber = new HttpCookie("maxPageNumber") { Value = maxPageCount.ToString() };
                HttpContext.Current.Response.Cookies.Add(maxPageNumber);
            }

            HttpContext.Current.Response.Cookies["maxPageNumber"].Value = maxPageCount.ToString();
        }

    }
}