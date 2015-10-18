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
            
            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
                new { id, viewCondition.CurrentPageNumber, SortType = newSortType});
        }

        public ActionResult GoNextPage(int id, ViewCondition viewCondition)
        {
            var nextPage = viewCondition.CurrentPageNumber + 1;
        
            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
                new { id, CurrentPageNumber = nextPage, viewCondition.SortType });

        }

        public ActionResult GoPrevPage(int id, ViewCondition viewCondition)
        {
            var prevPage = viewCondition.CurrentPageNumber - 1;

            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
                new { id, CurrentPageNumber = prevPage, viewCondition.SortType });
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
      
            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
                new { id = department.ParentId, viewCondition.CurrentPageNumber, viewCondition.SortType });
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
            
            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
               new { id = organization.Id, viewCondition.CurrentPageNumber, viewCondition.SortType });
        }

        public ActionResult DeleteDepartment(int id, ViewCondition viewCondition)
        {
            var organization = m_facade.GetDepartmentById(id).ParentOrganization;
            m_facade.DeleteDepartment(id);

            return RedirectToAction("OrganizationInfo", "OrganizationInfo",
             new { id = organization.Id, viewCondition.CurrentPageNumber, viewCondition.SortType });
        }
    }
}
