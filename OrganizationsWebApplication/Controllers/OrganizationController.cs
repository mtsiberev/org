using System.Linq;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.MvcHelpers;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();


        public ActionResult OrganizationsList()
        {
            Page page = Paginator.GetOrganizationsListPage(m_facade);

            var organizations =
                    from organization in m_facade.GetOrganizationsForOnePage(page.CurrentPageNumber, page.PageSize, page.CurrentInstanceId)
                    select new OrganizationViewModel() { Name = organization.Name, Id = organization.Id };

            var sortedOrganizations = SortingHelper.GetListSortedByName(organizations);

            return View(new ListOfOrganizationsViewModel(sortedOrganizations.ToList(), page));
        }

        public ActionResult OrganizationInfo(int id = 0)
        {
            Page page = Paginator.GetDepartmentsListPage(m_facade, id);

            var name = m_facade.GetOrganizationById(id).Name;

            var departments =
               from department in m_facade.GetDepartmentsForOnePage(page.CurrentPageNumber, page.PageSize, page.CurrentInstanceId)
               select new DepartmentViewModel() { Name = department.Name, Id = department.Id };

            var sortedDepartments = SortingHelper.GetListSortedByName(departments);

            return View(new OrganizationWithDepartmentsViewModel() { Id = id, Departments = sortedDepartments.ToList(), Name = name, Page = page });
        }

        public ActionResult AddOrganizationMenu()
        {
            return View();
        }

        public ActionResult AddOrganization(string name)
        {
            m_facade.AddOrganization(new Organization(0) { Name = name });
            return RedirectToAction("OrganizationsList", "Organization");
        }

        public ActionResult UpdateOrganizationMenu(int id)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organizationWithDepartmentsModel = new OrganizationWithDepartmentsViewModel() { Id = id, Departments = null, Name = name };
            return View(organizationWithDepartmentsModel);
        }

        public ActionResult UpdateOrganization(OrganizationWithDepartmentsViewModel organization)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });
            return RedirectToAction("OrganizationsList", "Organization");
        }

        public ActionResult DeleteOrganization(int id = 0)
        {
            m_facade.DeleteOrganization(id);
            return RedirectToAction("OrganizationsList", "Organization");
        }
    }
}
