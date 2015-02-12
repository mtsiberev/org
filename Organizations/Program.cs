using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Organizations
{

    class Program
    {
        static void Main(string[] args)
        {
            var facade = new Facade();

            facade.Add(new Organization() { Name = "FirstLine" });
            facade.Add(new Department(facade.GetOrganization("FirstLine").Id) { Name = "HR department" });
            facade.Add(new Department(facade.GetOrganization("FirstLine").Id) { Name = "R&D department" });
            facade.Add(new Department(facade.GetOrganization("FirstLine").Id) { Name = "IT department" });

            facade.Add(new Employee(facade.GetDepartment("IT department").Id) { Name = "Ivan", LastName = "Petrov", Age = 20, Address = new Address() { City = "NN", Street = "larina" } });
            facade.Add(new Employee(facade.GetDepartment("IT department").Id) { Name = "Dmitry", LastName = "Sidorov", Age = 30, Address = new Address() { City = "NN", Street = "larina" } });
            facade.Add(new Employee(facade.GetDepartment("IT department").Id) { Name = "Mikhail", LastName = "Ivanov", Age = 40, Address = new Address() { City = "NN", Street = "larina" } });
            
        }
    }


}


