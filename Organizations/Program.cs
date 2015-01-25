using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace OrganizationsNS
{

    class Program
    {
        static void Main(string[] args)
        {

            List<Organization> organizations = new List<Organization>();

            for (int i = 0; i < 4; i++)
            {
                organizations.Add(new Organization(i) { Name = (i.ToString() + "Line") });
            }

            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "IT department" });
            Department pDep = organizations[0].departments.Find(x => x.Name.Contains("IT department"));


            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov1", BirthDate = new DateTime(1995, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov1", BirthDate = new DateTime(1985, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov1", BirthDate = new DateTime(1975, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Mikhail", LastName = "Petrov1", BirthDate = new DateTime(1965, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Sergey", LastName = "Petrov2", BirthDate = new DateTime(1995, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Dmitry", LastName = "Petrov2", BirthDate = new DateTime(1985, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Alexey", LastName = "Petrov2", BirthDate = new DateTime(1975, 1, 1), address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { FirstName = "Mikhail", LastName = "Petrov2", BirthDate = new DateTime(1965, 1, 1), address = new Address() { City = "NN", Street = "larina" } });


            Display.GetAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(pDep);


            /*
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "HR department" });
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "R&D department" });
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "sales department" });

            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "IT department" });
            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "HR department" });
            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "sales department" });

            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "HR department" });
            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "R&D department" });
            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "sales department" });

            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "IT department" });
            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "HR department" });
            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId()) { Name = "R&D department" });

            Department pDep = organizations[0].departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1), address = new Address() { City = "NN", Street = "larina" } });

            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Pirogov", BirthDate = new DateTime(1994, 1, 1), address = new Address() { City = "M", Street = "repina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Kotov", BirthDate = new DateTime(1993, 1, 1), address = new Address() { City = "SPB", Street = "pushkina" } });
            /*
            pDep = organizations[0].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Dolinin", Age = 57, address = new Address() { City = "SPB", Street = "lenina" } }); //oldman
          

            pDep = organizations[0].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrikov", Age = 31, address = new Address() { City = "NN", Street = "larina" } });
          

            pDep = organizations[0].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Tolchin", Age = 34, address = new Address() { City = "NN", Street = "larina" } });
       
            /////////////////////second organization
            pDep = organizations[1].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Anotin", Age = 45, address = new Address() { City = "SPB", Street = "pushkina" } });
         
            pDep = organizations[1].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Okarin", Age = 47, address = new Address() { City = "SPB", Street = "pushkina" } });

         
            //////////////////////third organization
            pDep = organizations[2].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Teplov", Age = 51, address = new Address() { City = "NN", Street = "larina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Remezov", Age = 52, address = new Address() { City = "SPB", Street = "pushkina" } });
     

            pDep = organizations[2].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Aleshin", Age = 54, address = new Address() { City = "NN", Street = "larina" } });
         
            pDep = organizations[2].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Selin", Age = 32, address = new Address() { City = "M", Street = "bunina" } });
            //////////////////////fourth organization
            pDep = organizations[3].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Weller", Age = 55, address = new Address() { City = "NN", Street = "larina" } });

            pDep = organizations[3].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Burov", Age = 37, address = new Address() { City = "NN", Street = "engelsa" } });
          
            pDep = organizations[3].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Agarin", Age = 29, address = new Address() { City = "NN", Street = "tolstoga" } });
           
           // Reports.ShowAll(organizations[0]);
            
            Department pDepNew = organizations[0].departments[0];

            pDepNew.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Agarin", Age = 2, address = new Address() { City = "N1", Street = "tolstoga" } });
            pDepNew.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "QAgarin", Age = 249, address = new Address() { City = "N2", Street = "tolstoga" } });
         
           // Reports.FindAllEmployeesLivingOnTheSameStreet(pDepNew.employees);
            */

        }
    }
}


