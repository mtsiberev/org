using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Threading;


namespace Organizations
{
    class Organization
    {
        // public int emp_num { get; set; }//количество сотрудников в организации
        // public int deps_num { get; set; }//количество отделов в организации
        public string Name { get; set; }
        //public string Phone { get; set; }
        //public Address Addr { get; set; }
        //public string CEO;
        // public Dictionary<int, Department> deps;//идентификаторы отделов в организации
        //public Dictionary<int, Employee> employees;//сотрудники организации
        //List<Employee> employees;
        public List<Department> deps;
        //ид выдает организация при создании на основе кол-ва элементов в листе

        public Organization()
        {
            //Addr = new Address();
            //employees = new List <Employee>();
            deps = new List<Department>();
        }

        public void AddDep(string name)
        {
            this.deps.Add(new Department(this.deps.Count) { Name = name });
        }

    }

    class Department
    {
        //public static int cur_id;
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<int> employees = new List<int>();//список идентификаторов сотрудников, относящихся к конкретному отделу
        //public Department() { Id = cur_id++; }

        public List<Employee> employees;

        public void AddEmp(string name)
        {
            this.employees.Add(new Employee(this.employees.Count) { Name = name });
        }

        public Department(int id) { Id = id; employees = new List<Employee>(); }



    }

    class Employee : Person
    {
        //public static int cur_id;
        public int Id { get; set; }//идентификатор сотрудника  (совпадает с ключом в словаре) 
        //public string Position { get; set; }
        //public int Salary { get; set; }
        public Employee(int id) { Id = id; }
    }

    class Person
    {
        public string Name { get; set; }
        //public string Lastname { get; set; }
        public int Age { get; set; }
        public Address Addr { get; set; }
        public Person()
        {
            Addr = new Address();
            //Random rand = new Random( (DateTime.Now.Ticks) );
            Random rand = new Random((DateTime.Now.Millisecond));
            Age = rand.Next(18, 60);
            Thread.Sleep(10);
        }
    }

    class Address
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



    class Reports
    {
        public void FindEmpsByAge(Organization org, int min, int max)
        {
            foreach (var dep_var in org.deps)
            {

                //Console.WriteLine("Deps Key: {0} Name: {1}", dep_var.Id, dep_var.Name);
                //Employee e = dep_var.employees.Find(x => ((x.Age > min) && (x.Age < max)));
                foreach (var emp_var in dep_var.employees)
                { 
                    if( (emp_var.Age > min) && (emp_var.Age < max) )
                    {
                        Console.WriteLine("Employee with searching age. Dep Id {0} Emp Id {1} Age: {2} Name {3}",
                        dep_var.Id,
                        emp_var.Id,
                        emp_var.Age,
                        emp_var.Name);
                    };
                //Employee e = dep_var.employees.Find(x => x.Age < max);                    
                //Employee e = dep_var.employees.Find(x => (x.Age > min) && (x.Age < max) );
                                           

                  //Console.WriteLine("\tEmps Key: {0} Name: {1}", emp_var.Id, emp_var.Name);
                Console.WriteLine("\r\n");
            }
            }
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            /*
            Person per = new Person();
            Console.WriteLine(per.Addr.Apartment);
            per.Addr.Apartment = 15;
            Console.WriteLine(per.Addr.Apartment);
            */
            /*
            List<Employee> persons = new List<Employee>();
            persons.Add(new Employee { Name = "Mike", Lastname = "Tyson", Salary = 1000 });
            persons.Add(new Employee { Name = "Muhhamed", Lastname = "Ali", Salary = 1500 });
            persons.Add(new Employee { Name = "Joe", Lastname = "Fraser", Salary = 500 });

            foreach (var person in persons)
            {
                Console.WriteLine(person.Name);
                Console.WriteLine(person.Lastname);
                Console.WriteLine("\r\n");
            }

            //persons. 
            Console.WriteLine(
                "\nFind: Part where name contains \"Tyson\": {0}",
                persons.Find(x => x.Lastname.Contains("Tyson"))
                );
            */
            //создаем организацию и департаменты
            Organization firstline = new Organization() { Name = "FirstLine" };
            //добавим пустых отделов
            firstline.AddDep("dep1");
            firstline.AddDep("dep2");
            firstline.AddDep("dep3");

            Department pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            pp.AddEmp("Petrov");
            pp.AddEmp("Petrov");
            pp.AddEmp("Petrov");

            pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            pp.AddEmp("Pirogov");

            pp = firstline.deps.Find(x => x.Name.Contains("dep2"));
            pp.AddEmp("Sidorov");
            pp.AddEmp("Petrov");

            pp = firstline.deps.Find(x => x.Name.Contains("dep3"));
            pp.AddEmp("Ivanov");

            foreach (var dep_var in firstline.deps)
            {
                Console.WriteLine("Deps Key: {0} Name: {1}", dep_var.Id, dep_var.Name);
                foreach (var emp_var in dep_var.employees)
                {
                    Console.WriteLine("\tEmps Key: {0} Name: {1}  Age {2}",
                        emp_var.Id,
                        emp_var.Name,
                        emp_var.Age);
                }
                Console.WriteLine("\r\n");
            }

            Console.WriteLine("\r\n");

            Reports rep = new Reports();
            rep.FindEmpsByAge(firstline, 20, 40);

            /*
            foreach (var emp in firstline.employees)
            {
                Console.WriteLine("Key: {0} Name: {1}",
                    empl.Id,
                    empl.Name                    
                    );
            }
            */
            /*
 firstline.AddEmp(new Employee() { Position = "driver", Salary = 100, Name = "John" } );
 firstline.AddEmp(new Employee() { Position = "worker", Salary = 200, Name = "Mike"} );
 firstline.AddEmp(new Employee() { Position = "manager", Salary = 300, Name = "Sam" } );
 */

            /*

            Department pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            pp.employees.Add();
                        
            //firstline.deps.Add(new Department { Name = "dep2" });
            //firstline.deps.Add(new Department { Name = "dep3" });

            //добавляем сотрудника в организацию
            firstline.employees.Add(firstline.emp_num,
                new Employee { Id = firstline.emp_num, Position = "manager", Salary = 100, Name = "Mike" }
                );
            //определяем его в отдел
            Department pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            pp.employees.Add(firstline.emp_num);
            firstline.emp_num++;//увеличиваем количество сотрудников
            //по-моему криво реализовано, что надо вручную инкременить, как по другому сделать - не знаю
            
            firstline.employees.Add(firstline.emp_num,
                new Employee { Id = firstline.emp_num, Position = "driver", Salary = 110, Name = "Jack" }
                );
            pp.employees.Add(firstline.emp_num);
            firstline.emp_num++;
            
            firstline.employees.Add(firstline.emp_num,
                new Employee { Id = firstline.emp_num, Position = "worker", Salary = 120, Name = "Bob" }
                );
            //добавляем сотрудника в другой отдел
            pp = firstline.deps.Find(x => x.Name.Contains("dep2"));
            pp.employees.Add(firstline.emp_num);
            firstline.emp_num++;
                        
            foreach (var empl in firstline.employees)
            {
                Console.WriteLine("Key: {0} Name: {1} Position: {2} Department: {3} Id: {4}",
                    empl.Key,
                    empl.Value.Name,
                    empl.Value.Position,

                    empl.Value.Id
                    );
            }
            
            //Department pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            
            //Department pp = firstline.deps.Find(x => x.Name.Contains("dep1"));
            //pp.employees.Add(new Employee { Position = "manager", Salary = 100 } );
            //Console.WriteLine(pp.Name);
            */


        }
    }
}
