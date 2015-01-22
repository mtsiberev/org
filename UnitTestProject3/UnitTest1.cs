using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OrganizationsNS;
using System.Collections.Generic;
using System.Linq;//для перегрузки контэйнса


namespace UnitTestProject3
{   
    public class EmployeeCompare : IEqualityComparer<Employee>//компаратор для объектов-сотрудников
    {         
        public bool Equals(Employee empl1, Employee empl2)
        {
            return ( empl1.GetPersonId() == empl2.GetPersonId() );
        }

        public int GetHashCode(Employee empl1)
        {
            return empl1.GetPersonId();//заглушка
        }
    }

    public class OrganizationCompare : IEqualityComparer<Organization>//компаратор для сравнения организаций
    {
        public bool Equals(Organization org1, Organization org2)
        {
            return (org1.Id == org2.Id );
        }

        public int GetHashCode(Organization org1)
        {
            return org1.GetHashCode();//заглушка
        }
    }
       
    public class DepartmentsCompare : IEqualityComparer<Department> //компаратор отделов   
    {
        public bool Equals(Department dep1, Department dep2)
        {
            return (dep1.Id == dep2.Id);
        }

        public int GetHashCode(Department dep)
        {
            return dep.GetHashCode();//заглушка
        }
    }
     

    [TestClass]
    public class TestingOfReportsMethods
    {        
//шаблон/////        
        static public bool CompareListOfObjects<T>(List<T> expect, List<T> actual, IEqualityComparer<T> cmp)
        {            
            //даже если есть повторы в одном из списков, результат будет положительным
            //в дальнейшем повторы будут удалены Distinct в функции, отдающей лист с людьми, подходящими под условия выборки
            //все ли элементы одного списка представлены в другом списке
            foreach (var element in actual)
            {
                if (!expect.Contains(element, cmp))
                    return false;
            }
            //и наоборот            
            foreach (var element in expect)
            {
                if (!actual.Contains(element, cmp))
                    return false;
            }
            return true;
        }
 ////////////        
        static public EmployeeCompare empls_comparator = new EmployeeCompare();
        static public OrganizationCompare orgs_comparator = new OrganizationCompare();
        static public DepartmentsCompare deps_comparator = new DepartmentsCompare();

        /*
        static public bool CompareListOfEmployees(List<Employee> expect, List<Employee> actual) 
        {
            //даже если есть повторы в одном из списков, результат будет положительным
            //в дальнейшем повторы будут удалены Distinct в функции, отдающей лист с людьми, подходящими под условия выборки
            //все ли элементы одного списка представлены в другом списке
            foreach (var employee in actual)
            {                
                if (!expect.Contains(employee, empls_comparator))
                    return false; 
            }
            //и наоборот            
            foreach (var employee in expect)
            {
                if (!actual.Contains(employee, empls_comparator))
                    return false;
            }
            return true;
        }
        
        static public bool CompareListOfOrganizations(List<Organization> expect, List<Organization> actual)
        { 
            foreach (var organization in actual)
            {
                if (!expect.Contains(organization, orgs_comparator))
                    return false;
            }
            //и наоборот            
            foreach (var organization in expect)
            {
                if (!actual.Contains(organization, orgs_comparator))
                    return false;
            }
            return true;
        }
        */
                
        [TestMethod]
        public void TestingOfFindEmpsByAge()
        {            
            List<Employee> actual_employees = new List<Employee>();
            List<Employee> expected_employees = new List<Employee>();            

            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment( new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrov", Age = 20 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pirogov", Age = 21 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pavlov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pavlinov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kozlov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pikul", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kotov", Age = 23 });

            actual_employees = Reports.FindEmpsByAge(firstline, 21, 23);

            //pDep.employees.Find(x => x.Name == "Pavlov")


            expected_employees.Add(  pDep.employees.Find(x => x.Name == "Pavlov")   );//
            expected_employees.Add(  pDep.employees.Find(x => x.Name ==  "Pavlinov") );//
            expected_employees.Add(  pDep.employees.Find(x => x.Name == "Kozlov") );//
            expected_employees.Add(pDep.employees.Find(x => x.Name == "Pikul") ); ;//
                                    
//            bool result = TestingOfReportsMethods.CompareListOfEmployees(actual, expected);
            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_employees, expected_employees, empls_comparator);
            Assert.AreEqual(true, result, "Not equal");
        }
        
        [TestMethod]
        public void TestingOfFindEmpsByAgeLinQ()
        {
            List<Employee> actual_organizations = new List<Employee>();
            List<Employee> expected_organizations = new List<Employee>();

            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrov", Age = 20 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pirogov", Age = 21 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pavlov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pavlinov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kozlov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pikul", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kotov", Age = 23 });

            actual_organizations = Reports.FindEmpsByAgeLinQ(firstline, 21, 23);

            expected_organizations.Add( pDep.employees.Find(x => x.Name == "Pavlov") );
            expected_organizations.Add(pDep.employees.Find(x => x.Name == "Pavlinov"));
            expected_organizations.Add(pDep.employees.Find(x => x.Name == "Kozlov"));
            expected_organizations.Add(pDep.employees.Find(x => x.Name == "Pikul"));

            //bool result = TestingOfReportsMethods.CompareListOfEmployees(actual, expected);
            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_organizations, expected_organizations, empls_comparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindingOrganizationsByNameWithPersonNumber()
        {            
            List<Organization> organizations = new List<Organization>();
            List<Organization> actual_organizations = new List<Organization>();
            List<Organization> expected_organizations = new List<Organization>();
                        
            //исходный набор организаций
            Organization firstline = new Organization(0) { Name = "FirstLine" };
            Organization secondline = new Organization(1) { Name = "SecondLine" };
            Organization thirdline = new Organization(2) { Name = "ThirdLine" };
            
            organizations.Add(firstline);
            organizations.Add(secondline);
            organizations.Add(thirdline);

            //организации, которые попадают под условие
            expected_organizations.Add(firstline);
            expected_organizations.Add(thirdline);
            
            //добавим пустых отделов
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "HR department" });

            secondline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            secondline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "sales department" });

            thirdline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            thirdline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "R&D department" });

            //добавляем сотрудников.  
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pirogov", Age = 21 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Pavlov", Age = 22 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kotov", Age = 23 });//+

            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Dolinin", Age = 25 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Laptev", Age = 26 });

            /////////////////////second organization
            pDep = secondline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Anotin", Age = 45 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Sergeev", Age = 46 });      

            pDep = secondline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Chehov", Age = 48 });
            //////////////////////third organization
            pDep = thirdline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Teplov", Age = 51 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Remezov", Age = 52 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Alexeev", Age = 53 });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Burov", Age = 37 });//+

            pDep = thirdline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Aleshin", Age = 54 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Belkin", Age = 55 });

            actual_organizations = Reports.FindOrganizationsByNameWithPersonNumber(organizations, "IT", 2);
            
            //bool result = TestingOfReportsMethods.CompareListOfOrganizations(actual_organizations, expected_organizations);
            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_organizations, expected_organizations, orgs_comparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            Organization firstline = new Organization(0) { Name = "FirstLine" };

            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "HR department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "R&D department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "sales department" });

            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Petrov", Age = 20 });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Kotov", Age = 33 });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Larin", Age = 50 });//+

            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Dolinin", Age = 25 });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Laptev", Age = 29 });

            pDep = firstline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Petrikov", Age = 31 });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Larin", Age = 50 });//+
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Mihailov", Age = 33 });

            pDep = firstline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Tolchin", Age = 34 });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartamentId() ) { Name = "Parinov", Age = 35 });
            
            List<Department> actual_departments = new List<Department>();
            actual_departments = Reports.FindDepartmentWithOldestPerson(firstline);
                        
            List<Department> expected_departments = new List<Department>();
            expected_departments.Add(firstline.departments.Find(x => x.Name.Contains("IT department")));
            expected_departments.Add(firstline.departments.Find(x => x.Name.Contains("R&D department")));

            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_departments, expected_departments, deps_comparator);
            Assert.AreEqual(true, result, "Not equal"); 
        }

        [TestMethod]
        public void TestingOfFindEmployeeWithSubstring()
        {
            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrov", Age = 20 });//
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kotov", Age = 33 });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petarin", Age = 50 });//
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petotov", Age = 33 });//
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Larin", Age = 50 });

            List<Employee> actual_employees = Reports.FindEmployeeWithSubstring(firstline, "Pet");

            List<Employee> expected_employees = new List<Employee>();
            expected_employees.Add( pDep.employees.Find(x => x.Name == "Petrov") );//
            expected_employees.Add( pDep.employees.Find(x => x.Name == "Petarin") );
            expected_employees.Add( pDep.employees.Find(x => x.Name == "Petotov") );//

            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_employees, expected_employees, empls_comparator);
            Assert.AreEqual(true, result, "Not equal");
        }
        
      [TestMethod]
        public void TestingOfFindEmployeesWorkingInSeveralDepartments()
      {
          Organization firstline = new Organization(0) { Name = "FirstLine" };
          firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "IT department" });
          firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "HR department" });
          firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "R&D department" });
          firstline.AddDepartment(new Department(firstline.GetNewDepartamentId() ) { Name = "sales department" });
          
          Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));

          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrov", Age = 20 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Kotov", Age = 33 });
      
          pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Dolinin", Age = 25 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Laptev", Age = 29 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Parinov", Age = 35 });//+

          pDep = firstline.departments.Find(x => x.Name.Contains("R&D department"));
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Petrikov", Age = 31 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Larin", Age = 50 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId() ) { Name = "Mihailov", Age = 33 });

          pDep = firstline.departments.Find(x => x.Name.Contains("sales department"));
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { Name = "Tolchin", Age = 34 });
          pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { Name = "Parinov", Age = 35 });//+

          List<Employee> actual_employees = new List<Employee>();
          List<Employee> expected_employees = new List<Employee>();
          actual_employees = Reports.FindEmployeesWorkingInSeveralDepartments(firstline);

          expected_employees.Add( pDep.employees.Find(x => x.Name == "Parinov") );
          bool result =  TestingOfReportsMethods.CompareListOfObjects(actual_employees, expected_employees, empls_comparator);
          Assert.AreEqual(true, result, "Not equal");
      }
    }

}
