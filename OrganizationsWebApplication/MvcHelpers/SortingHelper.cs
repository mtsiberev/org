using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.MvcHelpers
{
    public static class SortingHelper
    {
        public static IEnumerable<T> GetListSortedByName<T>(IEnumerable<T> list) where T : IModel
        {
            if (HttpContext.Current.Request.Cookies["sort"] == null)
                HttpContext.Current.Response.Cookies["sort"].Value = "asc";

            if (HttpContext.Current.Request.Cookies["sort"].Value == "desc")
                return list.OrderByDescending(x => x.Name);

            return list.OrderBy(x => x.Name);
        }
    }
}