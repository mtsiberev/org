using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Reports
    {
        public Reports()
        {
            m_facade = new Facade(new Repository<Organization>(),
                new Repository<Department>(),
                new Repository<Employee>());
            m_facade.Init();
        }

        private readonly Facade m_facade;

        public void FindOrganizationsByNameOfDepartmentWithPersonNumber(string departmentName, int numberOfPerson)
        {
            var resultDepartments = m_facade.FindOrganizationsByNameOfDepartmentWithPersonNumber(departmentName,
                numberOfPerson);

            foreach (var department in resultDepartments)
            {
                Console.WriteLine("Department Id: {0},  Name: {1}", department.Id, department.Name);
            }
        }

        public void ShowEntityCode(IEntity entity)
        {
            Console.WriteLine("\tEntity code is: {0}", entity.GetEntityCode());
        }

        public void ShowAllOrganizations()
        {
            foreach (var organization in m_facade.GetAllOrganizations().ToList())
            {
                Console.WriteLine("\tOrganization Id: {0}  Name: {1}", organization.Id, organization.Name);
            }
        }

        public void ShowAllDepartmentsInOrganization(int organizationId)
        {
            foreach (var department in m_facade.GetAllDepartments().
                ToList().
                FindAll(e => e.ParentOrganization.Id == organizationId))
            {
                Console.WriteLine("\tDepartment Id: {0}  Name: {1}",
                    department.Id,
                    department.Name
                    );
            }
        }

        public void ShowAllEmployeesInOrganization(int organizationId)
        {
            foreach (var employee in m_facade.GetAllEmployees().
                ToList().
                FindAll(e => e.ParentDepartment.ParentOrganization.Id == organizationId))
            {
                Console.WriteLine("\tEmployee Id: {0}  FirstName: {1}  LastName: {2}  Age {3}",
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Age
                    );
            }
        }

        public void ShowEmployeesByAgeLinQ(int organizationId, int minAge, int maxAge)
        {
            var resultEmployees = m_facade.FindEmployeesByAgeLinQ(organizationId, minAge, maxAge);

            foreach (var employee in m_facade.GetAllEmployees().ToList())
            {
                Console.WriteLine("\tEmployee Id: {0}  FirstName: {1}  LastName: {2}  Date {3}",
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Age
                    );
            }
        }
        
        public void ShowAllEmployeesLivingOnTheSameStreet(int departmentId)
        {
            var resultEmployees = m_facade.GetAllEmployeesLivingOnTheSameStreet(departmentId);

            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("Employee: {0}  Street: {1}", employee.LastName, employee.Address.Street);
            }
        }

        public void ShowEmployeesWithSubstring(int organizationId, string subString)
        {
            var resultEmployees = m_facade.FindEmployeesWithSubstring(organizationId, subString);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("Employee Id: {0}  LastName: {1}", employee.Id, employee.LastName);
            }
        }

        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(int departmentId)
        {
            var employeesInDepartment = m_facade.GetEmployeesInDepartment(departmentId);
            var groupedEmployees = employeesInDepartment.GroupBy(e => e.Name);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }

        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLinq(int departmentId)
        {
            var employeesInDepartment = m_facade.GetEmployeesInDepartment(departmentId);
            var resultEmployees = employeesInDepartment.Select(x => x.Name).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }

        public void ShowDepartmentWithOldestPerson()
        {
            Console.WriteLine(m_facade.FindDepartmentWithOldestPerson());
        }

        public void ShowRandomEmployee()
        {
            var employee = m_facade.GetRandomEmployee();
            Console.WriteLine("Random Employee Id: {0} Name: {1} LastName: {2}", 
                employee.Id, 
                employee.Name, 
                employee.LastName);
        }
        
        public void ShowOrganizationById(int id)
        {
            var organization = m_facade.GetOrganizationById(id);
            Console.WriteLine("Organization Id: {0} Name: {1}",
                organization.Id,
                organization.Name);
        }

        public void ShowDepartmentById(int id)
        {
            var department = m_facade.GetDepartmentById(id);
            Console.WriteLine("Department Id: {0} Name: {1}",
                department.Id,
                department.Name);
        }

        public void ShowEmployeeById(int id)
        {
            var employee = m_facade.GetEmployeeById(id);
            Console.WriteLine("Employee Id: {0} Name: {1} LastName: {2}",
                employee.Id,
                employee.Name,
                employee.LastName);
        }
        
        public void ShowInitializedFacade()
        {
            ShowAllOrganizations();
            foreach (var organization in m_facade.GetAllOrganizations() )
            {
                ShowAllDepartmentsInOrganization(organization.Id);
                ShowAllEmployeesInOrganization(organization.Id);
            }
        }
        
    }

}


