using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationsListController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        [HttpGet]
        public ActionResult OrganizationsList()
        {
            var model = new OrganizationsList(m_facade, 1);
            return View(model);
        }

        [HttpPost]
        public ActionResult OrganizationsList(int pageNumberInOrganizationsList, string viewType)
        {
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            model.ViewType = viewType;

            return View(model);
        }

        public ActionResult GoNextPage(int pageNumberInOrganizationsList, string viewType)
        {
            var nextPage = pageNumberInOrganizationsList + 1;
            var model = new OrganizationsList(m_facade, nextPage);
            model.ViewType = viewType;

            return View("OrganizationsList", model);
        }

        public ActionResult GoPrevPage(int pageNumberInOrganizationsList, string viewType)
        {
            var prevPage = pageNumberInOrganizationsList - 1;
            var model = new OrganizationsList(m_facade, prevPage);
            model.ViewType = viewType;

            return View("OrganizationsList", model);
        }

        public ActionResult ChangeViewType(int pageNumberInOrganizationsList, string viewType)
        {
            string newViewType = "list";
            if (viewType == "list")
            {
                newViewType = "grid";
            }
            else if (viewType == "grid")
            {
                newViewType = "list";
            }

            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            model.ViewType = newViewType;

            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(int pageNumberInOrganizationsList, string viewType)
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string viewType)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            model.ViewType = viewType;

            return View("OrganizationsList", model);
        }

        public ActionResult UpdateOrganizationMenu(int id, int pageNumberInOrganizationsList, string viewType)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationViewModel()
            {
                Id = id,
                Name = name
            };
            
            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string viewType)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            model.ViewType = viewType;

            return View("OrganizationsList", model);
        }

        public ActionResult DeleteOrganization(int id, int pageNumberInOrganizationsList, string viewType)
        {
            m_facade.DeleteOrganization(id);
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            model.ViewType = viewType;

            return View("OrganizationsList", model);
        }
    }
}
