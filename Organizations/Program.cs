using System;
using System.Configuration;
using System.IO;
using Organizations.Container;

namespace Organizations
{
    class Program
    {
        private static void Main(string[] args)
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);

            var facade = ContainerWrapper.Container.GetInstance<Facade>();
            var report = new Reports(facade);
            //report.ShowAllOrganizations();
            //report.ShowAllEmployeesInOrganization(325);
            facade.GetAllEmployees();
                           
        }

    }
}
