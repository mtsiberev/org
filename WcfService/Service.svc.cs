using System;
using System.IO;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

using System.ServiceModel;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        private const string c_dataFolder = "~/App_Data/";
        private const string c_fileName = "xmlFile.xml";
        

        public XDocument LoadXmlFile(string fileName)
        {
            var filePathName = Path.Combine(HostingEnvironment.MapPath(c_dataFolder), c_fileName);

            try
            {
                var xDoc = XDocument.Load(filePathName);
                return xDoc;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SaveXmlFile(XDocument document, string fileName)
        {
            try
            {
                var filePathName = Path.Combine(HostingEnvironment.MapPath(c_dataFolder), c_fileName);
                document.Save(filePathName);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

