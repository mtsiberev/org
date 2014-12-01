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
        public static int cur_id;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Department> departments;

        public Organization()
        {
            Id = cur_id++;//не самое удачное решение
            departments = new List<Department>();
        }

        public void AddDepartment(Department dep)
        {
            this.departments.Add(new Department(this.departments.Count) { Name = dep.Name });
        }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> employees;
        public void AddEmployee(Employee emp)
        {
            this.employees.Add(new Employee(this.employees.Count) { Name = emp.Name, Age = emp.Age });
        }
        public Department(int id) { Id = id; employees = new List<Employee>(); }
        public Department() { }
    }

    public class Employee : Person
    {
        public int Id { get; set; }
        public Employee(int id)
        {
            Id = id;
            //person_id = cur_id++;
        }//
        //присвоение id персоне,происходит только при создании
        //реальной сущности (которая добавляется в список), чтобы id присваивались по порядку и не тратились на временные 
        //объекты, создаваемые для инициализации реальных
        public Employee() { }
    }

    public class Person
    {
        //public int person_id;
        public int GetPersonId()
        {
            return Name.GetHashCode() + Age.GetHashCode();//пока не используется        
        }
        public static int cur_id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        //static Random rand = new Random((DateTime.Now.Millisecond));//решение проблемы одинаковых случайных чисел

        public Person()
        {
            //person_id = cur_id++;
            //Addr = new Address();          
        }
    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public int Apartment { get; set; }
    }

    class CreditCard
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        protected static int cur_id { get; set; }

        public CreditCard()
        {
            this.Id = ++cur_id;
            Balance = 0;
        }

        public CreditCard(int balance)
        {
            this.Id = ++cur_id;
            this.Balance = balance;
        }
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
                        emp_var.GetPersonId() );
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
        static bool IsContainedInSeveralDepartaments(Organization org, Employee findEmpl, Department dep)
        {
            foreach (var dep_var in org.departments)
            {
                foreach (var emp_var in dep_var.employees)
                {
                    if (
                        (findEmpl.GetPersonId() == emp_var.GetPersonId() ) &&
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Organization> organizations = new List<Organization>();

            Organization firstline = new Organization() { Name = "FirstLine" };
            organizations.Add(firstline);

            Organization secondline = new Organization() { Name = "SecondLine" };
            organizations.Add(secondline);

            Organization thirdline = new Organization() { Name = "ThirdLine" };
            organizations.Add(thirdline);

            Organization fourthline = new Organization() { Name = "FourthLine" };
            organizations.Add(fourthline);

            //добавим пустых отделов
            firstline.AddDepartment(new Department() { Name = "IT department" });
            firstline.AddDepartment(new Department() { Name = "HR department" });
            firstline.AddDepartment(new Department() { Name = "R&D department" });
            firstline.AddDepartment(new Department() { Name = "sales department" });

            secondline.AddDepartment(new Department() { Name = "IT department" });
            secondline.AddDepartment(new Department() { Name = "HR department" });
            secondline.AddDepartment(new Department() { Name = "sales department" });

            thirdline.AddDepartment(new Department() { Name = "HR department" });
            thirdline.AddDepartment(new Department() { Name = "R&D department" });
            thirdline.AddDepartment(new Department() { Name = "sales department" });

            fourthline.AddDepartment(new Department() { Name = "IT department" });
            fourthline.AddDepartment(new Department() { Name = "HR department" });
            fourthline.AddDepartment(new Department() { Name = "R&D department" });

            //добавляем сотрудников.  
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee() { Name = "Petrov", Age = 20 });
            pDep.AddEmployee(new Employee() { Name = "Pirogov", Age = 21 });
            pDep.AddEmployee(new Employee() { Name = "Pavlov", Age = 22 });
            pDep.AddEmployee(new Employee() { Name = "Kotov", Age = 23 });

            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee() { Name = "Dolinin", Age = 25 });
            pDep.AddEmployee(new Employee() { Name = "Laptev", Age = 26 });

            pDep = firstline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee() { Name = "Petrikov", Age = 31 });
            pDep.AddEmployee(new Employee() { Name = "Larin", Age = 32 });
            pDep.AddEmployee(new Employee() { Name = "Mihailov", Age = 33 });

            pDep = firstline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee() { Name = "Tolchin", Age = 34 });
            pDep.AddEmployee(new Employee() { Name = "Parinov", Age = 35 });
            /////////////////////second organization
            pDep = secondline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee() { Name = "Anotin", Age = 45 });
            pDep.AddEmployee(new Employee() { Name = "Sergeev", Age = 46 });
            pDep.AddEmployee(new Employee() { Name = "Demidov", Age = 24 });

            pDep = secondline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee() { Name = "Okarin", Age = 47 });

            pDep = secondline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee() { Name = "Chehov", Age = 48 });
            //////////////////////third organization
            pDep = thirdline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee() { Name = "Teplov", Age = 51 });
            pDep.AddEmployee(new Employee() { Name = "Remezov", Age = 52 });
            pDep.AddEmployee(new Employee() { Name = "Alexeev", Age = 53 });

            pDep = thirdline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee() { Name = "Aleshin", Age = 54 });
            pDep.AddEmployee(new Employee() { Name = "Belkin", Age = 55 });

            pDep = thirdline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee() { Name = "Selin", Age = 32 });
            //////////////////////fourth organization
            pDep = fourthline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee() { Name = "Weller", Age = 55 });

            pDep = fourthline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee() { Name = "Burov", Age = 37 });
            pDep.AddEmployee(new Employee() { Name = "Baganov", Age = 45 });

            pDep = fourthline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee() { Name = "Agarin", Age = 29 });
            pDep.AddEmployee(new Employee() { Name = "Brasov", Age = 50 });


            //Reports.ShowAll(firstline);

            /*
            foreach (var v in Reports.FindDepartmentWithOldestPerson(fourthline))
            {
             Console.WriteLine("Employee with searching age. Dep Id {0} Name: {1}",
             v.Id,
             v.Name);
            }
            */

            //Reports.ShowAll(firstline);
            //Reports.ShowAll(secondline);
            //Reports.FindOrganizationsByNameWithPerson(organizations, "IT", 5);

            //foreach (var org in Reports.FindOrganizationsByNameWithPersonNumber(organizations, "IT", 2))
            // Reports.ShowAll(org);

            //Reports.ShowAll(firstline);
            //Reports.FindEmpsByAge(firstline, 21, 45);

            //foreach (var emp in Reports.FindEmpsByAgeLinQ(firstline, 22, 35))
            /*
            foreach(var emp in Reports.FindEmployeeWithSubstring(firstline, "Pa"))
            {
                Console.WriteLine("Employee with searching age. Emp Id {0} Age: {1} Name {2}",
                emp.Id,
                emp.Age,
                emp.Name);
            }
             * */

        }
    }
}
