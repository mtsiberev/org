using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Organizations.DbEntity;
using StructureMap;
using StructureMap.Configuration.DSL;


namespace Organizations
{
    class Program
    {
        private static void Main(string[] args)
        {
            var container = RegisterByContainer.Container;
            var facade = container
                .With(new RepoOrganizationDb())
                .With(new RepoDepartmentDb())
                .With(new RepoEmployeeDb())
                .GetInstance<Facade>();
            var report = new Reports(facade);
            report.ShowAllOrganizations();
            report.ShowAllEmployeesInOrganization(1);
        }

    }
}




