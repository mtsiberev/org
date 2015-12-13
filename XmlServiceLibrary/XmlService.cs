using System;
using System.IO;
using System.ServiceModel;
using System.Xml.Linq;
using System.Web.Hosting;

namespace XmlServiceLibrary
{
    public class XmlService : IXmlService
    {
        private const string c_dataFolder = "~/App_Data/";
        private const string c_fileName = "xmlFile.xml";
        


        public string LoadXmlFile(string fileName)
        //public XDocument LoadXmlFile(string fileName)
        {
            var filePathName = Path.Combine(HostingEnvironment.MapPath(c_dataFolder), c_fileName);

            try
            {
                var xDoc = XDocument.Load(filePathName);
                //return xDoc;
                return xDoc.ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }



       // public void SaveXmlFile(XDocument document, string fileName)
        public void SaveXmlFile(string document, string fileName)
        {
            try
            {
                var filePathName = Path.Combine(HostingEnvironment.MapPath(c_dataFolder), c_fileName);
                //document.Save(filePathName);

                var doc = XDocument.Parse(document); 
                doc.Save(filePathName);
             
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);  
            }
        }
      
    }
}
