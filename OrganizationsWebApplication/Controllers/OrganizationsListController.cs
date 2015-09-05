using System.Web.Mvc;
using Organizations;
using Organizations.EntitiesLists;
using Organizations.Helpers;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationsListController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        [HttpGet]
        public ActionResult OrganizationsList()
        {
            var organizationList = m_facade.GetOrganizationsList(1, "asc");
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View(model);
        }
        
        [HttpPost]
        public ActionResult OrganizationsList(int pageNumberInOrganizationsList, string sortType)
        {
            var organizationList = m_facade.GetOrganizationsList(pageNumberInOrganizationsList, "asc");
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View(model);
        }
      
        public ActionResult GoNextPage(int pageNumberInOrganizationsList, string sortType)
        {
            var nextPage = pageNumberInOrganizationsList + 1;

            var organizationList = m_facade.GetOrganizationsList(nextPage, "asc");
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }
        
        public ActionResult GoPrevPage(int pageNumberInOrganizationsList, string viewType, string sortType)
        {
            var prevPage = pageNumberInOrganizationsList - 1;

            var organizationList = m_facade.GetOrganizationsList(prevPage, "asc");
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

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

            var organizationList = m_facade.GetOrganizationsList(pageNumberInOrganizationsList, newSortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(int pageNumberInOrganizationsList, string sortType)
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string sortType)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(1, sortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult UpdateOrganizationMenu(int id, int pageNumberInOrganizationsList, string sortType)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationViewModel()
            {
                Id = id,
                Name = name
            };

            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization, int pageNumberInOrganizationsList, string sortType)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(1, sortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult DeleteOrganization(int id, int pageNumberInOrganizationsList, string sortType)
        {
            m_facade.DeleteOrganization(id);

            var organizationList = m_facade.GetOrganizationsList(1, sortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }
    }
}
