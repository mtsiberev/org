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

        public ActionResult OrganizationInfo(int id, ViewCondition viewCondition)
        {
            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, viewCondition.CurrentPageNumber, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View(model);
        }

        public ActionResult ChangeSortType(int id, ViewCondition viewCondition)
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

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, viewCondition.CurrentPageNumber, newSortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoNextPage(int id, ViewCondition viewCondition)
        {
            var nextPage = viewCondition.CurrentPageNumber + 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, nextPage, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(int id, ViewCondition viewCondition)
        {
            var prevPage = viewCondition.CurrentPageNumber - 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(id, prevPage, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult UpdateDepartmentMenu(int id, ViewCondition viewCondition)
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

        public ActionResult UpdateDepartment(DepartmentViewModel department, ViewCondition viewCondition)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(departmentBm.ParentOrganization.Id, 1, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }

        public ActionResult AddDepartmentMenu(int id, ViewCondition viewCondition)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = id
            };

            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department, ViewCondition viewCondition)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, ViewCondition viewCondition)
        {
            var organization = m_facade.GetDepartmentById(id).ParentOrganization;
            m_facade.DeleteDepartment(id);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, viewCondition.SortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }
    }
}
