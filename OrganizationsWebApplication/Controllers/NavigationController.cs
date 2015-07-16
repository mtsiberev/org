using System.Web.Mvc;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.MvcHelpers;

namespace OrganizationsWebApplication.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult GoBackFromDepartmentInfo(Page page)
        {
            Paginator.ClearSession(page.PageType);
            return RedirectToAction("OrganizationInfo",
                "Organization",
                new { id = page.ParentInstanceId });
        }

        public ActionResult GoBackFromOrganizationInfo(Page page)
        {
            Paginator.ClearSession(page.PageType);
            return RedirectToAction("OrganizationsList",
                "Organization");
        }

        public ActionResult Next(Page page)
        {
            page.GoNextPage();
            if (page.PageType == "organizationsPage")
            {
                return RedirectToAction("OrganizationsList", "Organization");
            }
            if (page.PageType == "departmentsPage")
            {
                return RedirectToAction("OrganizationInfo", "Organization", new { id = page.CurrentInstanceId });
            }
            if (page.PageType == "employeesPage")
            {
                return RedirectToAction("DepartmentInfo", "Department", new { id = page.CurrentInstanceId });
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Prev(Page page)
        {
            page.GoPrevPage();
            if (page.PageType == "organizationsPage")
            {
                return RedirectToAction("OrganizationsList", "Organization");
            }
            if (page.PageType == "departmentsPage")
            {
                return RedirectToAction("OrganizationInfo", "Organization", new { id = page.CurrentInstanceId });
            }
            if (page.PageType == "employeesPage")
            {
                return RedirectToAction("DepartmentInfo", "Department", new { id = page.CurrentInstanceId });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
