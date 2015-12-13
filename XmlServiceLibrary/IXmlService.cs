using System.ServiceModel;
using System.Xml.Linq;

namespace XmlServiceLibrary
{
    [ServiceContract]
    public interface IXmlService
    {
        [OperationContract]
        //void SaveXmlFile(XDocument document, string fileName);
        void SaveXmlFile(string document, string fileName);
        
        [OperationContract]
        //XDocument LoadXmlFile(string fileName);
        string LoadXmlFile(string fileName);
    
    }
}
