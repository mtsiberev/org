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
        public ActionResult OrganizationsList(ViewCondition viewCondition)
        {
            var organizationList = m_facade.GetOrganizationsList(viewCondition.CurrentPageNumber, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View(model);
        }

        public ActionResult GoNextPage(ViewCondition viewCondition)
        {
            var nextPage = viewCondition.CurrentPageNumber + 1;

            var organizationList = m_facade.GetOrganizationsList(nextPage, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult GoPrevPage(ViewCondition viewCondition)
        {
            var prevPage = viewCondition.CurrentPageNumber - 1;

            var organizationList = m_facade.GetOrganizationsList(prevPage, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

      
        public ActionResult ChangeSortType(ViewCondition viewCondition)
        {
            string newSortType = "asc";
            if (viewCondition.SortType == "desc")
            {
                newSortType = "asc";
            }
            else if (viewCondition.SortType == "asc")
            {
                newSortType = "desc";
            }

            var organizationList = m_facade.GetOrganizationsList(viewCondition.CurrentPageNumber, newSortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult AddOrganizationMenu(ViewCondition viewCondition)
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public ActionResult AddOrganization(OrganizationViewModel organization, ViewCondition viewCondition)
        {
            m_facade.AddOrganization(new Organization(0) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(viewCondition.CurrentPageNumber, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult UpdateOrganizationMenu(int id, ViewCondition viewCondition)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationViewModel()
            {
                Id = id,
                Name = name
            };

            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationViewModel organization, ViewCondition viewCondition)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });

            var organizationList = m_facade.GetOrganizationsList(1, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }

        public ActionResult DeleteOrganization(int id, ViewCondition viewCondition)
        {
            m_facade.DeleteOrganization(id);

            var organizationList = m_facade.GetOrganizationsList(1, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationsListViewModel(organizationList);

            return View("OrganizationsList", model);
        }
    }
}
