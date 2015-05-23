using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using Organizations.DbEntity;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Organizations
{
    class Program
    {
        private static void Main(string[] args)
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);

            var facade = RegisterByContainer.Container.GetInstance<Facade>();
            var report = new Reports(facade);
            report.ShowAllOrganizations();
            report.ShowAllEmployeesInOrganization(1);
        }

    }
}
