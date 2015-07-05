using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.MvcHelpers;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();


        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/account/login");
            }
            

            var page = Paginator.GetPageObject(m_facade.GetOrganizationsCount() );
            var currentPageNumber = page.CurrentPageNumber;
            var pageSize = page.PageSize;


            if (Request.Cookies["sort"] != null)
            {
                var sortType = Request.Cookies["sort"].Value;
                if (sortType == "descending")
                {
                    var sortedOrganizations =
                        from organization in m_facade.GetOrganizationsForOnePage(currentPageNumber, pageSize, "DESC")
                        where (true)
                        select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
                    return View(new ListOfOrganizationsViewModel(sortedOrganizations.ToList()));
                }

                if (sortType == "ascending")
                {
                    var sortedOrganizations =
                       from organization in m_facade.GetOrganizationsForOnePage(currentPageNumber, pageSize, "ASC")
                       where (true)
                       select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
                    return View(new ListOfOrganizationsViewModel(sortedOrganizations.ToList()));
                }
            }

            var defaultSortedOrganizations =
                  from organization in m_facade.GetOrganizationsForOnePage(currentPageNumber, pageSize, "DESC")
                  where (true)
                  select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };
            return View(new ListOfOrganizationsViewModel(defaultSortedOrganizations.ToList()));
        }
    }
}
