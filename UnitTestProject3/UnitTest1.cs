using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OrganizationsNS;
using System.Collections.Generic;
using System.Linq;


namespace OrganizationsNS
{

    public class EntityCompare : IEqualityComparer<IEntity>
    {
        public bool Equals(IEntity entity1, IEntity entity2)
        {
            return (entity1.GetId() == entity2.GetId());
        }

        public int GetHashCode(IEntity entity)
        {
            return entity.GetHashCode();
        }
    }
    

    [TestClass]
    public class TestingOfReportsMethods
    {

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

        //    static public InstanceCompare<Employee> employeesComparator = new EmployeeCompare();
        //   static public InstanceCompare<Organization> organizationsComparator = new OrganizationCompare();
        //   static public InstanceCompare<Department> departmentsComparator = new DepartmentsCompare();

        //static public EmployeeCompare<Employee> employeesComparator = new EmployeeCompare();
        //static public DepartmentsCompare departmentsComparator = new DepartmentsCompare();

        static public EntityCompare entityComparator = new EntityCompare();



        [TestMethod]
        public void TestingOfFindEmpsByAge()
        {
            List<Employee> actualEmployees = new List<Employee>();
            List<Employee> expectedEmployees = new List<Employee>();

            Organization firstline = new Organization(0) { Name = "FirstLine" };
            firstline.AddDepartment(new Department(firstline.GetNewDepartmentId()) { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee(pDep.GetNewEmployeeId()) { LastName = "Petrov", BirthDate = new DateTime(1995, 1, 1) });


            actualEmployees = Facade.FindEmployeesByAge(firstline, 21, 23);

            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pavlov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pavlinov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Kozlov"));//
            expectedEmployees.Add(pDep.employees.Find(x => x.LastName == "Pikul")); ;//

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualEmployees, expectedEmployees, entityComparator);
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


            actual_organizations = Facade.FindEmployeesByAgeLinQ(firstline, 21, 23);

            expected_organizations.Add(pDep.employees.Find(x => x.LastName == "Pavlov"));
            expected_organizations.Add(pDep.employees.Find(x => x.LastName == "Pavlinov"));


            bool result = TestingOfReportsMethods.CompareListOfObjects(actual_organizations, expected_organizations, entityComparator);
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


            actualOrganizations = Facade.FindOrganizationsByNameOfDepartmentWithPersonNumber(organizations, "IT", 2);

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualOrganizations, expectedOrganizations, entityComparator);
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
            actualDepartments.Add(Facade.FindDepartmentWithOldestPerson(firstline));

            List<Department> expectedDepartments = new List<Department>();
            expectedDepartments.Add(firstline.departments.Find(x => x.Name.Contains("IT department")));

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualDepartments, expectedDepartments, entityComparator);
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


            List<Employee> actualEmployees = Facade.FindEmployeesWithSubstring(firstline, "Pet");

            List<Employee> expected_employees = new List<Employee>();
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petrov"));//
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petarin"));
            expected_employees.Add(pDep.employees.Find(x => x.LastName == "Petotov"));//

            bool result = TestingOfReportsMethods.CompareListOfObjects(actualEmployees, expected_employees, entityComparator);
            Assert.AreEqual(true, result, "Not equal");

        }


    }


}
