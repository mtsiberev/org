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

    
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "IT department"} );
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "HR department" } );
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "R&D department" } );
            organizations[0].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "sales department" } );

            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "IT department" } );
            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "HR department" } );
            organizations[1].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "sales department" } );

            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "HR department"} );
            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "R&D department"} );
            organizations[2].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "sales department"} );

            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "IT department"} );
            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "HR department"} );
            organizations[3].AddDepartment(new Department(organizations[0].GetNewDepartmentId() ) { Name = "R&D department" } );

            Department pDep = organizations[0].departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrov", Age = 20, address = new Address() { City = "NN", Street = "larina" } });

            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { Name = "Pirogov", Age = 21, address = new Address() { City = "M", Street = "repina" } });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Kotov", Age = 23, address = new Address() { City = "SPB",Street =  "pushkina"} });

            pDep = organizations[0].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Dolinin", Age = 57, address = new Address() { City ="SPB", Street = "lenina"}} ); //oldman
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Laptev", Age = 26, address = new Address() { City ="M",Street =  "chekhova"} });

            pDep = organizations[0].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Petrikov",Age =  31, address = new Address() { City ="NN",Street =  "larina"}} );
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Mihailov", Age = 33, address = new Address() { City ="NN",Street =  "larina"} });

            pDep = organizations[0].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Tolchin", Age = 34,address = new Address() { City = "NN", Street = "larina"} });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Dolinin", Age = 57,address = new Address() { City = "SPB",Street =  "lenina"}} ); //oldman
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Parinov", Age = 35, address = new Address() { City ="SPB",Street =  "pushkina"}} );
            /////////////////////second organization
            pDep = organizations[1].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Anotin", Age = 45, address = new Address() { City ="SPB",Street =  "pushkina"}} );
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Demidov", Age = 24, address = new Address() { City ="M", Street = "chekhova"} });

            pDep = organizations[1].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Okarin",Age =  47, address = new Address() { City ="SPB",Street =  "pushkina"}} );

            pDep = organizations[1].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Chehov", Age = 48,address = new Address() { City = "M",Street =  "pechkina"} });
            //////////////////////third organization
            pDep = organizations[2].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Teplov",Age =  51, address = new Address() { City ="NN", Street = "larina"}} );
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Remezov",Age =  52,address = new Address() { City = "SPB",Street =  "pushkina"}} );
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Alexeev",Age =  53, address = new Address() { City ="SPB",Street =  "pechkina"}} );

            pDep = organizations[2].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Aleshin", Age = 54, address = new Address() { City ="NN",Street =  "larina"} });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Belkin", Age = 55,address = new Address() { City = "M",Street =  "stalina"} });

            pDep = organizations[2].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Selin",Age =  32,address = new Address() { City = "M", Street = "bunina"} });
            //////////////////////fourth organization
            pDep = organizations[3].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Weller", Age = 55, address = new Address() { City ="NN",Street =  "larina"} });

            pDep = organizations[3].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Burov", Age = 37, address = new Address() { City ="NN",Street =  "engelsa"} });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Baganov", Age = 45, address = new Address() { City ="NN", Street = "lermontova"}} );

            pDep = organizations[3].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) {Name = "Agarin", Age = 29,address = new Address() { City = "NN", Street = "tolstoga"}} );
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { Name = "Brasov", Age = 50, address = new Address() { City = "SPB", Street = "gorkoga" } });

           // Reports.ShowAll(organizations[0]);
            
            Department pDepNew = organizations[0].departments[0];
                
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "Agarin", Age = 2, address = new Address() { City = "N1", Street = "tolstoga" } } );
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "QAgarin", Age = 249, address = new Address() { City = "N2", Street = "tolstoga" } } );
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "EAgarin", Age = 269, address = new Address() { City = "N3", Street = "tolstoga" } });
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "ZAAgarin", Age = 729, address = new Address() { City = "N2", Street = "tolstoga" } });
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "RAgarin", Age = 299, address = new Address() { City = "N3", Street = "tolstoga" } });
            pDepNew.AddEmployee( new Employee(pDep.GetNewEmployeeId() ) { Name = "HAgarin", Age = 249, address = new Address() { City = "N1", Street = "tolstoga" } });
            pDepNew.AddEmployee(new Employee(pDep.GetNewEmployeeId() )  { Name = "LHAgarin", Age = 3249, address = new Address() { City = "N1", Street = "tolstoga" } });

            Reports.FindAllEmployeesLivingOnTheSameStreet(pDepNew.employees);
            

        }
    }
}


