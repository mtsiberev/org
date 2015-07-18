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
            var sortType = HttpContext.Current.Request.Cookies["sort"] != null ? HttpContext.Current.Request.Cookies["sort"].Value : "asc";
            if (sortType != "desc") return list.OrderBy(x => x.Name);
            var sortedList = list.OrderByDescending(x => x.Name);
            return sortedList;
        }
    }
}