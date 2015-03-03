﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Reports
    {
        public Reports()
        {
            facade = new Facade(new Repository<Organization>(), 
                new Repository<Department>(), 
                new Repository<Employee>() );
            facade.Init();
        }

        private Facade facade;

        public void ShowEntityCode(IEntity entity)
        {
            Console.WriteLine("\tEntity code is: {0}", entity.GetEntityCode());
        }

        public void ShowAllEmployeesInOrganization(int organizationId)
        {        
            foreach(var employee in facade.GetAllEmployees().
                ToList().
                FindAll(e => e.ParentDepartment.ParentOrganization.Id == organizationId))
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
            var resultEmployees = facade.GetAllEmployeesLivingOnTheSameStreet(departmentId);
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine("Employee: {0}   Street: {1}", employee.LastName, employee.Address.Street);              
            }
        }
        
        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartment(int departmentId)
        {
            var employeesInDepartment = facade.GetEmployeesInDepartment(departmentId);
            var groupedEmployees = employeesInDepartment.GroupBy(e => e.Name);
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
            }
        }

        public void ShowAllUniqueFirstNamesOfEmployeesInSpecifiedDepartmentLINQ(int departmentId)
        {
            var employeesInDepartment = facade.GetEmployeesInDepartment(departmentId);
            var resultEmployees = employeesInDepartment.Select(x => x.Name).Distinct();
            foreach (var employee in resultEmployees)
            {
                Console.WriteLine(employee);
            }
        }

    }

}


