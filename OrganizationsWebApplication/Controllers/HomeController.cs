using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        ////////////////////////////////////////////////////
        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/account/login");
            }
            
            if (Request.Cookies["pageNumber"] == null)
            {
                Response.Cookies["pageNumber"].Value = "1";
            }
           
            var stringPageNumber = Request.Cookies["pageNumber"].Value;
            var currentPageNumber = Convert.ToInt32(stringPageNumber);
            ///////////////////////////////////////////////////////
            var pageSizeCookie = new HttpCookie("pageSize") { Value = "6" };
            Response.Cookies.Add(pageSizeCookie);

            int pageSize = 0;
            if (Request.Cookies["pageSize"] != null)
            {
                var str = Request.Cookies["pageSize"].Value;
                pageSize = Convert.ToInt32(str);
            }
            ///////////////////////////////////////////////////////
            var maxPageNumber = new HttpCookie("maxPageNumber");
            var organizationsCount = m_facade.GetOrganizationsCount();

            var maxPageCount = organizationsCount / pageSize;
            if ((organizationsCount % pageSize) != 0) maxPageCount++;

            maxPageNumber.Value = maxPageCount.ToString();
            Response.Cookies.Add(maxPageNumber);
            ////////////////////////////////////////////////////////// 
            if (Request.Cookies["sort"] != null)
            {
                var sortType = Request.Cookies["sort"].Value;
                if (sortType == "descending")
                {
                    var sortedOrganizations =
                        from organization in m_facade.GetEntitiesForOnePage(currentPageNumber, pageSize, "DESC")
                        where (true)
                        select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
                    return View(new ListOfOrganizationsViewModel(sortedOrganizations.ToList()));
                }
                if (sortType == "ascending")
                {
                    var sortedOrganizations =
                       from organization in m_facade.GetEntitiesForOnePage(currentPageNumber, pageSize, "ASC")
                       where (true)
                       select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
                    return View(new ListOfOrganizationsViewModel(sortedOrganizations.ToList()));
                }
            }
            var defaultSortedOrganizations =
                  from organization in m_facade.GetEntitiesForOnePage(currentPageNumber, pageSize, "DESC")
                  where (true)
                  select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
            return View(new ListOfOrganizationsViewModel(defaultSortedOrganizations.ToList()));
        }
    }
}
