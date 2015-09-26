using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult OrganizationInfo(int id, ViewState viewState)
        {
            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, viewState.CurrentPageNumber, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View(model);
        }

        public ActionResult ChangeSortType(int id, ViewState viewState)
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

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, viewState.CurrentPageNumber, newSortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoNextPage(int id, ViewState viewState)
        {
            var nextPage = viewState.CurrentPageNumber + 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, nextPage, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(int id, ViewState viewState)
        {
            var prevPage = viewState.CurrentPageNumber - 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, prevPage, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult UpdateDepartmentMenu(int id, ViewState viewState)
        {
            var department = m_facade.GetDepartmentById(id);
            var departmentModel = new DepartmentViewModel()
            {
                Id = department.Id,
                ParentId = department.ParentOrganization.Id,
                Name = department.Name
            };

            return View(departmentModel);
        }

        public ActionResult UpdateDepartment(DepartmentViewModel department, ViewState viewState)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(departmentBm.ParentOrganization.Id, 1, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }

        public ActionResult AddDepartmentMenu(int id, ViewState viewState)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = id
            };

            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department, ViewState viewState)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, ViewState viewState)
        {
            var organization = m_facade.GetDepartmentById(id).ParentOrganization;
            m_facade.DeleteDepartment(id);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, viewState.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }
    }
}
