using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
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
        public ActionResult OrganizationsList(ViewState viewState)
        {
            var organizationList = m_facade.GetOrganizationsList(viewState.CurrentPageNumber, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View(model);
        }

        public ActionResult GoNextPage(ViewState viewState)
        {
            var nextPage = viewState.CurrentPageNumber + 1;

            var organizationList = m_facade.GetOrganizationsList(nextPage, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult GoPrevPage(ViewState viewState)
        {
            var prevPage = viewState.CurrentPageNumber - 1;

            var organizationList = m_facade.GetOrganizationsList(prevPage, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

      
        public ActionResult ChangeSortType(ViewState viewState)
        {
            string newSortType = "asc";
            if (viewState.SortType == "desc")
            {
                newSortType = "asc";
            }
            else if (viewState.SortType == "asc")
            {
                newSortType = "desc";
            }

            var organizationList = m_facade.GetOrganizationsList(viewState.CurrentPageNumber, newSortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(ViewState viewState)
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization, ViewState viewState)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(viewState.CurrentPageNumber, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult UpdateOrganizationMenu(int id, ViewState viewState)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationViewModel()
            {
                Id = id,
                Name = name
            };

            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization, ViewState viewState)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(1, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult DeleteOrganization(int id, ViewState viewState)
        {
            m_facade.DeleteOrganization(id);

            var organizationList = m_facade.GetOrganizationsList(1, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }
    }
}
