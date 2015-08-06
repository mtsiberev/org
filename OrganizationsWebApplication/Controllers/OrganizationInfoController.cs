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
            int organizationId = 0,
            int pageNumberInOrganizationsList = 0,
            int pageNumberInOrganizationInfo = 0)
        {
            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                organizationId,
                m_facade.GetOrganizationById(organizationId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo);
            //var sortedDepartments = SortingHelper.GetListSortedByName(departments);
            return View(model);
        }

        public ActionResult GoNextPage(
            int organizationId = 0,
            int pageNumberInOrganizationsList = 0,
            int pageNumberInOrganizationInfo = 0)
        {
            var nextPage = pageNumberInOrganizationInfo + 1;

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              organizationId,
              m_facade.GetOrganizationById(organizationId).Name,
              pageNumberInOrganizationsList,
              nextPage);

            return View("OrganizationInfo", model);
        }

        public ActionResult GoPrevPage(
            int organizationId = 0,
            int pageNumberInOrganizationsList = 0,
            int pageNumberInOrganizationInfo = 0)
        {
            var prevPage = pageNumberInOrganizationInfo - 1;

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              organizationId,
              m_facade.GetOrganizationById(organizationId).Name,
              pageNumberInOrganizationsList,
              prevPage);

            return View("OrganizationInfo", model);
        }


        public ActionResult UpdateDepartmentMenu(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo)
        {
            var department = m_facade.GetDepartmentById(id);
            var departmentModel = new DepartmentViewModel()
            {
                Id = department.Id,
                ParentId = department.ParentOrganization.Id,
                Name = department.Name,
                PageNumberInOrganizationsList = pageNumberInOrganizationsList,
                PageNumberInOrganizationInfo = pageNumberInOrganizationInfo
            };
            return View(departmentModel);
        }

        public ActionResult UpdateDepartment(DepartmentViewModel department)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);

            var model = new OrganizationWithDepartmentsViewModel(
              m_facade,
              department.ParentId,
              m_facade.GetOrganizationById(department.ParentId).Name,
              department.PageNumberInOrganizationsList,
              department.PageNumberInOrganizationInfo);

            return View("OrganizationInfo", model);
        }


        public ActionResult AddDepartmentMenu(int organizationId, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo)
        {
            var departmentModel = new DepartmentViewModel()
            {
                ParentId = organizationId,
                PageNumberInOrganizationsList = pageNumberInOrganizationsList,
                PageNumberInOrganizationInfo = pageNumberInOrganizationInfo
            };
            return View(departmentModel);
        }
        
        public ActionResult AddDepartment(DepartmentViewModel department)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                department.ParentId,
                m_facade.GetOrganizationById(department.ParentId).Name,
                department.PageNumberInOrganizationsList,
                department.PageNumberInOrganizationInfo);

            return View("OrganizationInfo", model);
        }

        public ActionResult DeleteDepartment(int id, int pageNumberInOrganizationsList, int pageNumberInOrganizationInfo)
        {
            var parentId = m_facade.GetDepartmentById(id).ParentOrganization.Id;
            m_facade.DeleteDepartment(id);

            var model = new OrganizationWithDepartmentsViewModel(
                m_facade,
                parentId,
                m_facade.GetOrganizationById(parentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo);

            return View("OrganizationInfo", model);
        }
    }
}
