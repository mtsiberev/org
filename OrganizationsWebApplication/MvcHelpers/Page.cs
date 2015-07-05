using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationsWebApplication.MvcHelpers
{
    public class Page
    {
        public Page(int pageSize, int currentPageNumber)
        {
            PageSize = pageSize;
            CurrentPageNumber = currentPageNumber;
        }

        public int PageSize { get; private set; }
        public int CurrentPageNumber { get; private set; }
    }
}