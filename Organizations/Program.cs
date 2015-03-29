using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StructureMap;
using StructureMap.Configuration.DSL;


namespace Organizations
{
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {
            Container = new Container(x =>
            {
                x.For<Reports>().Singleton().Use<Reports>();
                x.For<Facade>().Singleton().Use<Facade>();
                x.For<IRepository<Organization>>().Singleton().Use<Repository<Organization>>();
                x.For<IRepository<Department>>().Singleton().Use<Repository<Department>>();
                x.For<IRepository<Employee>>().Singleton().Use<Repository<Employee>>();
            });
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            var container = new RegisterByContainer().Container;
        
            var organizationsRepository = container.GetInstance<Repository<Organization>>();
            var departmentsRepository = container.GetInstance<Repository<Department>>();
            var employeesRepository = container.GetInstance<Repository<Employee>>();
            
            var facade = container
                .With(organizationsRepository)
                .With(departmentsRepository)
                .With(employeesRepository)
                .GetInstance<Facade>();
            facade.Init();

            var report = container
                .With(facade)
                .GetInstance<Reports>();
            
            Console.WriteLine(container.WhatDoIHave() );
            
            report.ShowInitializedFacade();

            report.ShowEntityCode(new Organization(1));
            report.ShowAllOrganizations();
            report.ShowOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);

            report.ShowEntityCode(new Department(1, null));
            report.ShowAllDepartmentsInOrganization(1);
            report.ShowDepartmentWithOldestPerson();

            report.ShowEntityCode(new Employee(1, null));
            report.ShowAllEmployeesInOrganization(1);
            report.ShowAllEmployeesOrederedByStreet(1);
            report.ShowEmployeesWithSubstring(1, "ov");
            report.ShowEmployeesByAge(1, 20, 40);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);

            for (int i = 0; i < 5; i++)
                report.ShowRandomEmployee();
        }

    }
}




