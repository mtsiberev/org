using System;
using System.Linq;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class OrganizationInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult OrganizationInfo(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType,
            string sortType)
        {
            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                viewType,
                sortType);
         
            return View(model);
        }
        
        public ActionResult ChangeViewType(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType,
            string sortType)
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

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                newViewType,
                sortType);

            return View("OrganizationInfo", model);
        }
        
        public ActionResult ChangeSortType(
         int organizationId,
         int pageNumberInOrganizationsList,
         int pageNumberInOrganizationInfo,
         string viewType,
         string sortType)
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

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                viewType,
                newSortType);

            return View("OrganizationInfo", model);
        }
        
        public ActionResult GoNextPage(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType,
            string sortType)
        {
            var nextPage = pageNumberInOrganizationInfo + 1;

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              organizationId,
              m_facade.GetOrganizationById(organizationId).Name,
              pageNumberInOrganizationsList,
              nextPage,
              viewType,
              sortType);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType,
            string sortType)
        {
            var prevPage = pageNumberInOrganizationInfo - 1;

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                prevPage,
                viewType,
                sortType);

            return View("OrganizationInfo", model);
        }

        public ActionResult UpdateDepartmentMenu(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
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

        public ActionResult UpdateDepartment(DepartmentViewModel department, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                department.ParentId,
                m_facade.GetOrganizationById(department.ParentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                viewType, 
                sortType);

            return View("OrganizationInfo", model);
        }
        
        public ActionResult AddDepartmentMenu(int organizationId, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = organizationId
            };

            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                department.ParentId,
                m_facade.GetOrganizationById(department.ParentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                viewType,
                sortType);

            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType, string sortType)
        {
            var parentId = m_facade.GetDepartmentById(id).ParentOrganization.Id;
            m_facade.DeleteDepartment(id);

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                parentId,
                m_facade.GetOrganizationById(parentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                viewType,
                sortType);

            return View("OrganizationInfo", model);
        }
    }
}
