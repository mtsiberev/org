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
            string viewType)
        {
            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo);
           model.ViewType = viewType;
            return View(model);
        }
        

        public ActionResult ChangeViewType(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType)
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
         pageNumberInOrganizationInfo);

            model.ViewType = newViewType;

            return View("OrganizationInfo", model);
        }

        
        public ActionResult GoNextPage(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType)
        {
            var nextPage = pageNumberInOrganizationInfo + 1;

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              organizationId,
              m_facade.GetOrganizationById(organizationId).Name,
              pageNumberInOrganizationsList,
              nextPage);

            model.ViewType = viewType;

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(
            int organizationId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            string viewType)
        {
            var prevPage = pageNumberInOrganizationInfo - 1;

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              organizationId,
              m_facade.GetOrganizationById(organizationId).Name,
              pageNumberInOrganizationsList,
              prevPage);

            model.ViewType = viewType;

            return View("OrganizationInfo", model);
        }

        public ActionResult UpdateDepartmentMenu(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType)
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

        public ActionResult UpdateDepartment(DepartmentViewModel department, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                department.ParentId,
                m_facade.GetOrganizationById(department.ParentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo
                );
            model.ViewType = viewType;

            return View("OrganizationInfo", model);
        }


        public ActionResult AddDepartmentMenu(int organizationId, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = organizationId
            };

            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                department.ParentId,
                m_facade.GetOrganizationById(department.ParentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo);

            model.ViewType = viewType;

            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo, string viewType)
        {
            var parentId = m_facade.GetDepartmentById(id).ParentOrganization.Id;
            m_facade.DeleteDepartment(id);

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                parentId,
                m_facade.GetOrganizationById(parentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo);

            model.ViewType = viewType;

            return View("OrganizationInfo", model);
        }
    }
}
