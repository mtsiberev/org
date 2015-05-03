using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using Organizations.DbEntity;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        //private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        private Facade m_facade = RegisterByContainer.Container
                .With(new RepoOrganizationDb())
                .With(new RepoDepartmentDb())
                .With(new RepoEmployeeDb())
                .GetInstance<Facade>();

        public ActionResult Index()
        {
            var organizations =
                from organization in m_facade.GetAllOrganizations()
                where (true)
                select new DtoOrganization() { Name = organization.Name, Id = organization.Id };

            return View(new OrganizationListModels(organizations.ToList()));
        }

        public ActionResult OrganizationInfo(int id = 0)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var departments =
                from department in m_facade.GetAllDepartments()
                where department.ParentOrganization.Id == id
                select new DtoDepartment() { Name = department.Name, Id = department.Id };

            return View(new OrganizationModel(departments.ToList(), name));
        }

        public ActionResult DepartmentInfo(int id = 0)
        {
            var name = m_facade.GetDepartmentById(id).Name;
            var employees =
               from employee in m_facade.GetEmployeesInDepartment(id)
               select new DtoEmployee()
               {
                   Name = String.Join(" ", employee.Name, employee.LastName),
                   Id = employee.Id
               };

            return View(new DepartmentModel(employees.ToList(), name));
        }

    }
}
