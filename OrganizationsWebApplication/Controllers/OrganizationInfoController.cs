using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models.EntitiesModels;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult OrganizationInfo(int organizationId, int pageNumberInOrganizationInfo, string sortType)
        {
            var organizationInfo = m_facade.GetOrganizationWithDepartments(organizationId, pageNumberInOrganizationInfo, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View(model);
        }
        
        public ActionResult ChangeSortType(int organizationId, int pageNumberInOrganizationInfo, string sortType)
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

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organizationId, pageNumberInOrganizationInfo, newSortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }
        
        public ActionResult GoNextPage(int organizationId, int pageNumberInOrganizationInfo, string sortType)
        {
            var nextPage = pageNumberInOrganizationInfo + 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organizationId, nextPage, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(int organizationId, int pageNumberInOrganizationInfo, string sortType)
        {
            var prevPage = pageNumberInOrganizationInfo - 1;

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organizationId, prevPage, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult UpdateDepartmentMenu(int id, int pageNumberInOrganizationInfo, string sortType)
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

        public ActionResult UpdateDepartment(DepartmentViewModel department, int pageNumberInOrganizationInfo, string sortType)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(departmentBm.ParentOrganization.Id, 1, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }
        
        public ActionResult AddDepartmentMenu(int organizationId, int pageNumberInOrganizationInfo, string sortType)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = organizationId
            };

            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department, int pageNumberInOrganizationInfo, string sortType)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);
            
            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, int pageNumberInOrganizationInfo, string sortType)
        {
            var organization = m_facade.GetDepartmentById(id).ParentOrganization;
            m_facade.DeleteDepartment(id);

            var organizationInfo = m_facade.GetOrganizationWithDepartments(organization.Id, 1, sortType);
            var model = EntitiesListToView.GetOrganizationInfoViewModel(organizationInfo);

            return View("OrganizationInfo", model);
        }
    }
}
