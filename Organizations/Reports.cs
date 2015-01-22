using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsNS
{
    public class Reports
    {
        public static void ShowAll(Organization org)
        {
            Console.WriteLine("Organization name: {0}   Id: {1}", org.Name, org.Id);
            foreach (var dep_var in org.departments)
            {
                Console.WriteLine("Departament: Id: {0}   Name: {1}", dep_var.Id, dep_var.Name);
                foreach (var emp_var in dep_var.employees)
                {
                    Console.WriteLine("\tEmployee Id: {0}  Name: {1}  Age {2}  PersonId {3}",
                        emp_var.Id,
                        emp_var.Name,
                        emp_var.Age,
                        emp_var.GetPersonId());
                }
                Console.WriteLine("\r\n");
            }
        }

        public static List<Employee> FindEmpsByAge(Organization org, int min, int max)//old version
        {
            List<Employee> result = new List<Employee>();
            foreach (var dep_var in org.departments)
            {
                foreach (var emp_var in dep_var.employees)
                {
                    if ((emp_var.Age > min) && (emp_var.Age < max))
                    {
                        result.Add(emp_var);
                    };
                }
            }
            return result;
        }

        public static List<Employee> FindEmpsByAgeLinQ(Organization org, int min, int max)
        {
            var emps_by_range =
                from dep in org.departments
                from emp in dep.employees
                where emp.Age > min
                where emp.Age < max
                select emp;
            return emps_by_range.ToList();
        }

        //поиск организации, в которой в отделе, имя которого передается вторым аргументов числится количество сотрудников, больше указанного
        public static List<Organization> FindOrganizationsByNameWithPersonNumber(List<Organization> orgs, string depName, int numberPerson)
        {
            List<Organization> resultOrg = new List<Organization>();
            foreach (var org in orgs)
            {
                foreach (var dep in org.departments)
                {
                    if (
                        (dep.Name.Contains(depName)) &&
                        (dep.employees.Count() > numberPerson)
                        )
                    {
                        resultOrg.Add(org);
                        break; //если организация подходит, то переходим сразу к следующей организации в листе
                    }
                }
            }
            return resultOrg;
        }

        public static List<Department> FindDepartmentWithOldestPerson(Organization org)
        {
            List<Department> result = new List<Department>();
            //поиск самых старых в каждом отделе
            var old_emps =
                from dep in org.departments
                from emp in dep.employees
                where emp.Age == dep.employees.Max(x => x.Age)
                select emp;

            Employee oldest = old_emps.ToList().Find(x => x.Age == old_emps.ToList().Max(y => y.Age)); //o_O   самый старый из всех отделов

            //найдем по Id персоны отделы, в которых числится самый старый сотрудник
            var deps_with_oldman =
              from dep in org.departments
              from emp in dep.employees
              where emp.GetPersonId() == oldest.GetPersonId()
              select dep;
            return deps_with_oldman.ToList();
        }


        public static Department FindDepartmentWithOldestPerson2(Organization org)//более рациональная версия
        {
            int maximumAge = 0;
            List<Department> departaments = new List<Department>();//искомые отделы
            Department departamentWithOldestEmployee = new Department(-1);

            foreach (var dep_var in org.departments)
            {
                foreach (var emp_var in dep_var.employees)
                {
                    if (emp_var.Age > maximumAge)
                    {
                        maximumAge = emp_var.Age;
                        departamentWithOldestEmployee = dep_var;
                        continue;
                    }
                }
            }
            return departamentWithOldestEmployee;
        }

        public static List<Employee> FindEmployeeWithSubstring(Organization org, string sub)
        {
            var empls_with_name =
                from dep in org.departments
                from emp in dep.employees
                where emp.Name.StartsWith(sub)
                select emp;
            return empls_with_name.ToList();
        }

        //вспомогательный метод. сообщает содержится ли сотрудник в отделе, отличном от передаваемого в аргументе
        //позволяет проверить числится ли сотрудник в более чем одном отделе
        static bool IsContainedInSeveralDepartaments(Organization org, Employee findEmpl, Department dep)
        {
            foreach (var dep_var in org.departments)
            {
                foreach (var emp_var in dep_var.employees)
                {
                    if (
                        (findEmpl.GetPersonId() == emp_var.GetPersonId()) &&
                        (dep_var.Id != dep.Id)
                        )
                        return true;
                }
            }
            return false;
        }

        public static List<Employee> FindEmployeesWorkingInSeveralDepartments(Organization org)
        {
            var empls_with_name =
                from dep in org.departments
                from emp in dep.employees
                where Reports.IsContainedInSeveralDepartaments(org, emp, dep)
                select emp;
            return empls_with_name.ToList();
        }
        


        public static void FindAllEmployeesLivingOnTheSameStreet(List<Employee> emps)//projection method
        {
            var all_emps = emps.Select(e => new { e.address.City, e.Name }).OrderBy(e => e.City);

            foreach (var employees in all_emps)
            {
                Console.WriteLine("  {0}", employees);
            }        
        }

    }


}
