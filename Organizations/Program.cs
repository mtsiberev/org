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
    public class RegisterAsPlugin
    {
        public IContainer Container;

        public RegisterAsPlugin()
        {
            Container = new Container(x =>
            {
                x.For<IRepository<Organization>>().Use<Repository<Organization>>().Named("Organization");
                x.For<IRepository<Department>>().Use<Repository<Department>>().Named("Department");
                x.For<IRepository<Employee>>().Use<Repository<Employee>>().Named("Employee"); ;
            });
        }
    }
    
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {
            Container = new Container(x =>
            {
                x.For<IRepository<Organization>>().Use<Repository<Organization>>();
                x.For<IRepository<Department>>().Use<Repository<Department>>();
                x.For<IRepository<Employee>>().Use<Repository<Employee>>();
            });
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            var report = new Reports();
            report.ShowInitializedFacade();

            report.ShowEntityCode(new Organization(1));
            report.ShowAllOrganizations();
            report.ShowOrganizationsByNameOfDepartmentWithPersonNumber("IT", 2);
            
            report.ShowEntityCode(new Department(1, null));
            report.ShowAllDepartmentsInOrganization(1);
            report.ShowDepartmentWithOldestPerson();
            
            report.ShowEntityCode(new Employee(1, null));
            report.ShowAllEmployeesInOrganization(1);
            report.ShowAllEmployeesLivingOnTheSameStreet(1);
            report.ShowEmployeesWithSubstring(1, "ov");
            report.ShowEmployeesByAge(1, 20, 40);
            report.ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(1);
          
            for (int i = 0; i < 5; i++)
                report.ShowRandomEmployee();
            /*
            var container = new RegisterAsPlugin().Container;
            var employeeRepositoryTest = container.GetInstance<IRepository<Employee>>("Employee");
            
            var container2 = new RegisterByContainer().Container;
            var organizationRepository = container2.GetInstance<IRepository<Organization>>();
            var departmentRepository = container2.GetInstance<IRepository<Department>>();
            var employeeRepository = container2.GetInstance<IRepository<Employee>>();
            
            var facade = new Facade(organizationRepository, departmentRepository, employeeRepository);
            facade.Init();
            facade.GetAllEmployees();
             */
        }

    }
}




