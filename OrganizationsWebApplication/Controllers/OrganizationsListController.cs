using System.Web.Mvc;
using System.Web.Routing;
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
        public ActionResult OrganizationsList(int pageNumberInOrganizationsList)
        {
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            return View(model);
        }

        public ActionResult GoNextPage(int pageNumberInOrganizationsList)
        {
            var nextPage = pageNumberInOrganizationsList + 1;
            var model = new OrganizationsList(m_facade, nextPage);
            return View("OrganizationsList", model);
        }

        public ActionResult GoPrevPage(int pageNumberInOrganizationsList)
        {
            var prevPage = pageNumberInOrganizationsList - 1;
            var model = new OrganizationsList(m_facade, prevPage);
            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(int pageNumberInOrganizationsList)
        {
            var model = new OrganizationViewModel()
            {
                PageNumberInOrganizationsList = pageNumberInOrganizationsList
            };
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });

            var model = new OrganizationsList(m_facade, organization.PageNumberInOrganizationsList);
            return View("OrganizationsList", model);
        }
        
        public ActionResult UpdateOrganizationMenu(int id, int pageNumberInOrganizationsList)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organizationWithDepartmentsModel = new OrganizationViewModel()
            {
                Id = id,
                Name = name,
                PageNumberInOrganizationsList = pageNumberInOrganizationsList
            };

            return View(organizationWithDepartmentsModel);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });
            var model = new OrganizationsList(m_facade, organization.PageNumberInOrganizationsList);

            return View("OrganizationsList", model);
        }
       
        public ActionResult DeleteOrganization(int id, int pageNumberInOrganizationsList)
        {
            m_facade.DeleteOrganization(id);
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList);
            return View("OrganizationsList", model);
        }
    }
}
