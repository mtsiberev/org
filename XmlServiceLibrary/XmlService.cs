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
        {
            var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePathName = Path.Combine(currentDirectory, c_fileName);
            
            try
            {
                var xDoc = XDocument.Load(filePathName);
                return xDoc.ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public void SaveXmlFile(string document, string fileName)
        {
            try
            {
                var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var filePathName = Path.Combine(currentDirectory, c_fileName);
       
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
