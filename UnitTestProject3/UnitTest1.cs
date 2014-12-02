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
    
    [TestClass]
    public class TestingOfReportsMethods
    {
        static public EmployeeCompare empls_comparator = new EmployeeCompare();

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


        [TestMethod]
        public void TestMethod1()
        {            
            List<Employee> actual = new List<Employee>();
            List<Employee> expected = new List<Employee>();
            

            Organization firstline = new Organization() { Name = "FirstLine" };
            firstline.AddDepartment(new Department() { Name = "IT department" });
            Department pDep = firstline.departments.Find(x => x.Name.Contains("IT department"));
            pDep.AddEmployee(new Employee() { Name = "Petrov", Age = 20 });
            pDep.AddEmployee(new Employee() { Name = "Pirogov", Age = 21 });
            pDep.AddEmployee(new Employee() { Name = "Pavlov", Age = 22 });//
            pDep.AddEmployee(new Employee() { Name = "Pavlinov", Age = 22 });//
            pDep.AddEmployee(new Employee() { Name = "Kozlov", Age = 22 });//
            pDep.AddEmployee(new Employee() { Name = "Pikul", Age = 22 });//
            pDep.AddEmployee(new Employee() { Name = "Kotov", Age = 23 });

            actual = Reports.FindEmpsByAge(firstline, 21, 23);


            expected.Add(new Employee() { Name = "Pavlov", Age = 22 });//
            expected.Add(new Employee() { Name = "Pavlinov", Age = 22 });//
            expected.Add(new Employee() { Name = "Kozlov", Age = 22 });//
            expected.Add(new Employee() { Name = "Pikul", Age = 22 });//
            
                        
            bool result = TestingOfReportsMethods.CompareListOfEmployees(actual, expected);
            Assert.AreEqual(true, result, "Not equal");
        }


    }
}
