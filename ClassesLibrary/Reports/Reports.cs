using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Organizations
{
    public class Reports
    {
        [DefaultConstructor]
        public Reports()
            :this(new Facade())
        {
            m_facade.Init();
        }
        
        public Reports(Facade facade)
        {
            m_facade = facade;
        }

        private readonly Facade m_facade;

        public void ShowOrganizationsByNameOfDepartmentWithPersonNumber(string departmentName, int numberOfPerson)
        {
            var resultDepartments = m_facade.FindOrganizationsByNameOfDepartmentWithPersonNumber(departmentName,
                numberOfPerson);

            foreach (var department in resultDepartments)
            {
                Console.WriteLine("\nDepartment Id: {0},  Name: {1}", department.Id, department.Name);
            }
        }

        public void ShowEntityCode(IEntity entity)
        {
            Console.WriteLine("\nEntity {0} has code: {1}",
                entity.GetType().Name, entity.GetEntityCode());
        }

        public void ShowAllOrganizations()
        {
            foreach (var organization in m_facade.GetAllOrganizations().ToList())
            {
                Console.WriteLine("\nOrganization Id: {0}  Name: {1}", organization.Id, organization.Name);
            }
        }

        public void ShowAllDepartmentsInOrganization(int organizationId)
        {
            foreach (var department in m_facade.GetAllDepartments().
                ToList().
                FindAll(e => e.ParentOrganization.Id == organizationId))
            {
                Console.WriteLine("\nDepartment Id: {0}  Name: {1}",
                    department.Id,
                    department.Name
                    );
            }
        }

        public void ShowAllEmployeesInOrganization(int organizationId)
        {
            Console.WriteLine("\nOrganization with Id: {0} contains the following employees:", organizationId);
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

        public void ShowEmployeesByAge(int organizationId, int minAge, int maxAge)
        {
            var resultEmployees = m_facade.FindEmployeesByAge(organizationId, minAge, maxAge);
            Console.WriteLine("\nEmployees in range {0} - {1} of age:", minAge, maxAge);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("Employee Id: {0}  FirstName: {1}  LastName: {2}  Age {3}",
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Age
                    );
            }
        }

        public void ShowAllEmployeesOrederedByStreet(int departmentId)
        {
            var resultEmployees = m_facade.OrderEmployeesByStreet(departmentId);
            Console.WriteLine("\nDepartment with Id: {0} contains employees living on same street:", departmentId);
            string street = null;
            foreach (var employee in resultEmployees)
            {
                if ((street == null) || (employee.Address.Street != street))
                {
                    street = employee.Address.Street;
                    Console.WriteLine("On the street {0} are living the following employees:", employee.Address.Street);
                }
                Console.WriteLine("Employee: {0}", employee.LastName);
            }
        }

        public void ShowEmployeesWithSubstring(int organizationId, string subString)
        {
            var resultEmployees = m_facade.FindEmployeesWithSubstring(organizationId, subString);
            Console.WriteLine("\nEmployees which have substring '{0}' in LastName:", subString);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("Employee Id: {0}  LastName: {1}", employee.Id, employee.LastName);
            }
        }

        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(int departmentId)
        {
            var employeesInDepartment = m_facade.GetEmployeesInDepartment(departmentId);
            var resultEmployees = employeesInDepartment.Select(x => x.Name).Distinct();
            Console.WriteLine("\nUnique names of employees in department with Id {0}:", departmentId);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }

        public void ShowDepartmentWithOldestPerson()
        {
            var department = m_facade.FindDepartmentWithOldestPerson();
            Console.WriteLine("\nDepartment with oldest person has Id: {0} Name:  {1}",
                department.Id, department.Name);
        }

        public void ShowRandomEmployee()
        {
            var employee = m_facade.GetRandomEmployee();
            Console.WriteLine("Random Employee Id: {0} Name: {1} LastName: {2}",
                employee.Id,
                employee.Name,
                employee.LastName);
        }

        public void ShowInitializedFacade()
        {
            ShowAllOrganizations();
            foreach (var organization in m_facade.GetAllOrganizations())
            {
                ShowAllDepartmentsInOrganization(organization.Id);
                ShowAllEmployeesInOrganization(organization.Id);
            }
        }

    }

}


