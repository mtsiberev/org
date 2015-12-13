using System;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using NLog;
using System.Web.Mvc;
using Organizations;
using Organizations.Container;
using OrganizationsWebApplication.IoC;

namespace OrganizationsWebApplication.Controllers
{
    public class ImportExportController : Controller
    {
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult MainMenu()
        {
            return View();
        }

        public JsonResult Import()
        {
            //var fileService = MvcContainer.Container.GetInstance<WcfService.Service>();
            var fileService = new TcpXmlService.XmlServiceClient("NetTcpBinding_IXmlService");

            string result = null;
            try
            {
                var stringDocument = fileService.LoadXmlFile("file");
                var xDocument = XDocument.Parse(stringDocument); 

                var xmlDocument = new XmlDocument();
                using (var reader = xDocument.CreateReader())
                {
                    xmlDocument.Load(reader);
                }
                var converted = JsonConvert.SerializeXmlNode(xmlDocument);
                result = converted.Replace("@", "");
            }
            catch (FaultException ex)
            {
                logger.Error(ex.Message);
                result = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public void Export()
        {
            var doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            var organizationsList = m_facade.GetAllOrganizations();

            foreach (var org in organizationsList)
            {
                var organization = new XElement("organization");

                organization.Add(new XAttribute("Id", org.Id));
                organization.Add(new XAttribute("Name", org.Name));

                var departmentsList = m_facade.GetAllDepartments().Where(x => x.ParentOrganization.Id == org.Id);

                foreach (var dep in departmentsList)
                {
                    var department = new XElement("department");
                    department.Add(new XAttribute("Id", dep.Id));
                    department.Add(new XAttribute("Name", dep.Name));

                    var employeesList = m_facade.GetAllEmployees().Where(y => y.ParentDepartment != null).Where(x => x.ParentDepartment.Id == dep.Id);
                    foreach (var emp in employeesList)
                    {
                        var employee = new XElement("employee");
                        employee.Add(new XAttribute("Id", emp.Id));
                        employee.Add(new XAttribute("Name", emp.Name));

                        department.Add(employee);
                    }
                    organization.Add(department);
                }
                root.Add(organization);
            }
            
            //var fileService = MvcContainer.Container.GetInstance<WcfService.Service>();
            var fileService = new TcpXmlService.XmlServiceClient("NetTcpBinding_IXmlService");

            try
            {
                //fileService.SaveXmlFile(doc, "file.xml");
                fileService.SaveXmlFile(doc.ToString(), "file.xml");
            }
            catch (FaultException ex)
            {
                logger.Error(ex.Message);
            }

        }
    }
}
