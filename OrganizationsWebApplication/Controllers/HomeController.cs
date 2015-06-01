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
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        ////////////////////////////////////////////////////
        public ActionResult Index()
        {
            var organizations =
                from organization in m_facade.GetAllOrganizations()
                where (true)
                select new DtoOrganization() { Name = organization.Name, Id = organization.Id };
            
            var sortedOrganizations = organizations.ToList();

            if (Request.Cookies["sort"] != null)
            {
                var value = Request.Cookies["sort"].Value;
                if (value == "descending")
                {
                    sortedOrganizations = sortedOrganizations.OrderByDescending(x => x.Name).ToList();
                }
                else if (value == "ascending")
                {
                    sortedOrganizations = sortedOrganizations.OrderBy(x => x.Name).ToList();
                }
            }
            return View(new OrganizationListModels(sortedOrganizations));
        }
    }
}
