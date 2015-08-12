using System.Linq;
using System.Web.Mvc;
using Organizations;
using Organizations.Helpers;
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
            var model = new OrganizationsList(m_facade, 1, "list", "asc");
            return View(model);
        }

        [HttpPost]
        public ActionResult OrganizationsList(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, viewType, sortType);
            return View(model);
        }

        public ActionResult GoNextPage(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var nextPage = pageNumberInOrganizationsList + 1;
            var model = new OrganizationsList(m_facade, nextPage, viewType, sortType);

            return View("OrganizationsList", model);
        }

        public ActionResult GoPrevPage(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var prevPage = pageNumberInOrganizationsList - 1;
            var model = new OrganizationsList(m_facade, prevPage, viewType, sortType);

            return View("OrganizationsList", model);
        }

        public ActionResult ChangeViewType(int pageNumberInOrganizationsList, string viewType, string sortType)
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
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, newViewType, sortType);

            return View("OrganizationsList", model);
        }

        public ActionResult ChangeSortType(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            string newSortType = "asc";
            if (sortType == "desc")
            {
                newSortType = "asc";
            }
            else if (sortType == "asc")
            {
                newSortType = "desc";
            }
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, viewType, newSortType);

            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });
            OwnershipHelper.WriteOwner();
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, viewType, sortType);
            
            return View("OrganizationsList", model);
        }

        public ActionResult UpdateOrganizationMenu(int id, int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationViewModel()
            {
                Id = id,
                Name = name
            };

            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, viewType, sortType);

            return View("OrganizationsList", model);
        }

        public ActionResult DeleteOrganization(int id, int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            m_facade.DeleteOrganization(id);
            var model = new OrganizationsList(m_facade, pageNumberInOrganizationsList, viewType, sortType);

            return View("OrganizationsList", model);
        }
    }
}
