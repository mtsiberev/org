using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OrganizationsNS;
using System.Collections.Generic;
using System.Linq;


namespace UnitTestProject3
{
    public class EmployeeCompare : IEqualityComparer<Employee>
    {
        public bool Equals(Employee employee1, Employee employee2)
        {
            return (employee1.GetPersonId() == employee2.GetPersonId());
        }

        public int GetHashCode(Employee employee)
        {
            return employee.GetPersonId();
        }
    }

    public class OrganizationCompare : IEqualityComparer<Organization>
    {
        public bool Equals(Organization organization1, Organization organization2)
        {
            return (organization1.Id == organization2.Id);
        }

        public int GetHashCode(Organization organization)
        {
            return organization.GetHashCode();
        }
    }

    public class DepartmentsCompare : IEqualityComparer<Department>
    {
        public bool Equals(Department department1, Department department2)
        {
            return (department1.Id == department2.Id);
        }

        public int GetHashCode(Department department)
        {
            return department.GetHashCode();
        }
    }
    
    [TestClass]
    public class TestingOfReportsMethods
    {
        //шаблон/////        
        static public bool CompareListOfObjects<T>(List<T> expectedInstances, List<T> actualInstances, IEqualityComparer<T> cmp)
        {
            foreach (var employee in actualInstances)
            {
                if (!expectedInstances.Contains(employee, cmp))
                    return false;
            }

            foreach (var employee in expectedInstances)
            {
                if (!actualInstances.Contains(employee, cmp))
                    return false;
            }
            return true;
        }
        ////////////            
        static public EmployeeCompare employeesComparator = new EmployeeCompare();
        static public OrganizationCompare organizationsComparator = new OrganizationCompare();
        static public DepartmentsCompare departmentsComparator = new DepartmentsCompare();

        [TestMethod]
        public void TestingOfFindEmpsByAge()
        {
            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();

            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });


            actualEmployees = Reports.FindEmployeesByAge(firstline, 21, 23);

            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pavlov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pavlinov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Kozlov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pikul")); ;//

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualEmployees, expectedEmployees, employeesComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindEmpsByAgeLinQ()
        {
            List<Employee> actual_organizations = new List<Employee>();
            List<Employee> expected_organizations = new List<Employee>();

            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Pirogov", BirthDate = new DateTime(1994, 1, 1) });
      

            actual_organizations = Reports.FindEmployeesByAgeLinQ(firstline, 21, 23);

            expected_organizations.Add(pDep.employees.Find(x => x.LastName == "Pavlov"));
            expected_organizations.Add(pDep.employees.Find(x => x.LastName == "Pavlinov"));
      

            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_organizations, expected_organizations, employeesComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindingOrganizationsByNameWithPersonNumber()
        {
            List<Organization> organizations = new List<Organization>();
            List<Organization> actualOrganizations = new List<Organization>();
            List<Organization> expectedOrganizations = new List<Organization>();

            //исходный набор организаций
            Organization firstline = new Organization(0) { Name = "FirstLine" };
            Organization secondline = new Organization(1) { Name = "SecondLine" };
            Organization thirdline = new Organization(2) { Name = "ThirdLine" };

            organizations.Add(firstline);
            organizations.Add(secondline);
            organizations.Add(thirdline);

            //организации, которые попадают под условие
            expectedOrganizations.Add(firstline);
            expectedOrganizations.Add(thirdline);

            //добавим пустых отделов
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "HR department" });

            secondline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            secondline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "sales department" });

            thirdline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            thirdline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "R&D department" });

            //добавляем сотрудников.  
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Pirogov", BirthDate = new DateTime(1995, 1, 1) });//+
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Pavlov", BirthDate = new DateTime(1994, 1, 1) });//+
  
            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Dolinin", BirthDate = new DateTime(1995, 1, 1) });
        

            /////////////////////second organization
            pDep = secondline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Anotin", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Sergeev", BirthDate = new DateTime(1995, 1, 1) });

            pDep = secondline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Chehov", BirthDate = new DateTime(1995, 1, 1) });
            //////////////////////third organization
            pDep = thirdline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Teplov", BirthDate = new DateTime(1995, 1, 1) });//+
    

            pDep = thirdline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Aleshin", BirthDate = new DateTime(1995, 1, 1) });
     

            actualOrganizations = Reports.FindOrganizationsByNameOfDepartmentWithPersonNumber(organizations, "IT", 2);

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualOrganizations, expectedOrganizations, organizationsComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindDepartmentWithOldestPerson()
        {
            Organization firstline = new Organization(0) { Name = "FirstLine" };

            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "HR department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "R&D department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "sales department" });

            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee(new Employee(firstline.GetNewDepartmentId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(firstline.GetNewDepartmentId()) { LastName = "Kotov", BirthDate = new DateTime(1995, 1, 1) });
         

            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartmentId()) { LastName = "Dolinin", BirthDate = new DateTime(1995, 1, 1) });
           
            pDep = firstline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartmentId()) { LastName = "Petrikov", BirthDate = new DateTime(1995, 1, 1) });
          
            pDep = firstline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(firstline.GetNewDepartmentId()) { LastName = "Tolchin", BirthDate = new DateTime(1995, 1, 1) });
          
            List<Department> actualDepartments = new List<Department>();
            //actualDepartments = Reports.FindDepartmentWithOldestPerson(firstline);
            actualDepartments.Add(Reports.FindDepartmentWithOldestPerson(firstline));

            List<Department> expectedDepartments = new List<Department>();
            expectedDepartments.Add(firstline.departments.Find(x => x.Name.Contains("IT department")));

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualDepartments, expectedDepartments, departmentsComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindEmployeeWithSubstring()
        {
            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });//
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Kotov", BirthDate = new DateTime(1995, 1, 1) });
       

            List<Employee> actualEmployees = Reports.FindEmployeesWithSubstring(firstline, "Pet");

            List<Employee> expected_employees = new List<Employee>();
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petrov"));//
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petarin"));
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petotov"));//

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualEmployees, expected_employees, employeesComparator);
            Assert.AreEqual(true, result, "Not equal");
        }

        [TestMethod]
        public void TestingOfFindEmployeesWorkingInSeveralDepartments()
        {
            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "HR department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "R&D department" });
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "sales department" });

            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));

            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Kotov", BirthDate = new DateTime(1995, 1, 1) });

            pDep = firstline.departments.Find(x => x.Name.Contains("HR department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Dolinin", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Laptev", BirthDate = new DateTime(1995, 1, 1) });
         
            pDep = firstline.departments.Find(x => x.Name.Contains("R&D department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrikov", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Larin", BirthDate = new DateTime(1995, 1, 1) });
        
            pDep = firstline.departments.Find(x => x.Name.Contains("sales department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Tolchin", BirthDate = new DateTime(1995, 1, 1) });
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Parinov", BirthDate = new DateTime(1995, 1, 1) });//+

            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();
            actualEmployees = Reports.FindEmployeesWorkingInSeveralDepartments(firstline);

            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Parinov"));
            bool result = TestingOfReportsMethods.CompareListOfObjects(actualEmployees, expectedEmployees, employeesComparator);
            Assert.AreEqual(true, result, "Not equal");
        }
    }

}
