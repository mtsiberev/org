using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
//using System.Security.Cryptography;

namespace OrganizationsNS
{
    public class Organization
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<Department> departments;
        public Organization(int id)
        {
            Id = id;
            departments = new List<Department>();
        }

        public void AddDepartment(string name)
        {
            this.departments.Add(new Department(this.departments.Count) { Name = name });
        }
    }

    public class Department
    {
        public Department(int id) { Id = id; employees = new List<Employee>(); }
        public int Id { get; private set; }

        public string Name { get; set; }
        public List<Employee> employees;

        public void AddEmployee(string name, int age, string city, string street)
        {
            this.employees.Add(new Employee(this.employees.Count) { Name = name, Age = age, City = city, Street = street });
        }
    }

    public class Employee : Person
    {
        public int Id { get; private set; }

        public Employee(int id)//Id выдается отделом
        {
            Id = id;
        }
    }

    public class Person
    {
        // public Person(){}               
        public int GetPersonId()
        {
            return Name.GetHashCode() + Age.GetHashCode();
        }
        public string Name { get; set; }
        public int Age { get; set; }
        //static Random rand = new Random((DateTime.Now.Millisecond));//решение проблемы одинаковых случайных чисел
        public string City { get; set; }
        public string Street { get; set; }
    }

    public class Reports
    {
        public static void ShowAll(Organization org)
        {
            Console.WriteLine("Organization name: {0}  Id: {1}", org.Name, org.Id);
            foreach (var dep_var in org.departments)
            {
                Console.WriteLine("Departament: Id: {0}  Name: {1}", dep_var.Id, dep_var.Name);
                foreach (var emp_var in dep_var.employees)
                {
                    Console.WriteLine("\tEmployee: Emp Id {0} Name: {1} Age {2} PersonId {3}",
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


        public static List<Department> FindDepartmentWithOldestPerson2(Organization org)//более рациональная версия
        {
            Employee standard = new Employee(-1) { Age = 0, Name = "", Street = "", City = "" };
            List<Department> departaments = new List<Department>();//искомые отделы
            //departaments.Add(new Department(-1));

            foreach (var dep_var in org.departments)
            {
                foreach (var emp_var in dep_var.employees)
                {
                    if (emp_var.Age > standard.Age)
                    {
                        standard = emp_var;
                        if (departaments.Count != 0) departaments.Clear();
                        departaments.Add(dep_var);
                        continue;
                    }
                    if (emp_var.Age == standard.Age)
                    {
                        departaments.Add(dep_var);
                    }
                }
            }
            //departaments.Distinct();
            return departaments;
        }

        public static List<Employee> FindEmployeeWithSubstring(Organization org, string sub)
        {
            var empls_with_name =
                from dep in org.departments
                from emp in dep.employees
                //where emp.Name.Contains(sub) 
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

        //FindAllEmployeesLivingOnTheSameStreet/city etc.
        public static void FindAllEmployeesLivingOnTheSameStreet()
        {

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Organization> organizations = new List<Organization>();

            for (int i = 0; i < 4; i++)
            {
                organizations.Add(new Organization(i) { Name = (i.ToString() + "Line") });
            }

            //добавим пустых отделов
            organizations[0].AddDepartment("IT department");
            organizations[0].AddDepartment("HR department");
            organizations[0].AddDepartment("R&D department");
            organizations[0].AddDepartment("sales department");

            organizations[1].AddDepartment("IT department");
            organizations[1].AddDepartment("HR department");
            organizations[1].AddDepartment("sales department");

            organizations[2].AddDepartment("HR department");
            organizations[2].AddDepartment("R&D department");
            organizations[2].AddDepartment("sales department");

            organizations[3].AddDepartment("IT department");
            organizations[3].AddDepartment("HR department");
            organizations[3].AddDepartment("R&D department");

            //добавляем сотрудников.  
            Department pDep = organizations[0].departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee("Petrov", 20, "NN", "larina");
            pDep.AddEmployee("Pirogov", 21, "M", "repina");
            pDep.AddEmployee("Kotov", 23, "SPB", "pushkina");

            pDep = organizations[0].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee("Dolinin", 57, "SPB", "lenina"); //oldman
            pDep.AddEmployee("Laptev", 26, "M", "chekhova");

            pDep = organizations[0].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee("Petrikov", 31, "NN", "larina");
            pDep.AddEmployee("Mihailov", 33, "NN", "larina");

            pDep = organizations[0].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee("Tolchin", 34, "NN", "larina");
            pDep.AddEmployee("Dolinin", 57, "SPB", "lenina"); //oldman
            pDep.AddEmployee("Parinov", 35, "SPB", "pushkina");
            /////////////////////second organization
            pDep = organizations[1].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee("Anotin", 45, "SPB", "pushkina");
            pDep.AddEmployee("Demidov", 24, "M", "chekhova");

            pDep = organizations[1].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee("Okarin", 47, "SPB", "pushkina");

            pDep = organizations[1].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee("Chehov", 48, "M", "pechkina");
            //////////////////////third organization
            pDep = organizations[2].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee("Teplov", 51, "NN", "larina");
            pDep.AddEmployee("Remezov", 52, "SPB", "pushkina");
            pDep.AddEmployee("Alexeev", 53, "SPB", "pechkina");

            pDep = organizations[2].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee("Aleshin", 54, "NN", "larina");
            pDep.AddEmployee("Belkin", 55, "M", "stalina");

            pDep = organizations[2].departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee("Selin", 32, "M", "bunina");
            //////////////////////fourth organization
            pDep = organizations[3].departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee("Weller", 55, "NN", "larina");

            pDep = organizations[3].departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee("Burov", 37, "NN", "engelsa");
            pDep.AddEmployee("Baganov", 45, "NN", "lermontova");

            pDep = organizations[3].departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee("Agarin", 29, "NN", "tolstoga");
            pDep.AddEmployee("Brasov", 50, "SPB", "gorkoga");


            Reports.FindDepartmentWithOldestPerson2(organizations[0]);




        }
    }
}


