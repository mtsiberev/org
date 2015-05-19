using System;
using System.Collections.Generic;
using Organizations.DbEntity;
using Organizations.Helpers;
using Organizations.Mappers;

namespace Organizations.DbRepository
{
    public class RepositoryEmployeeDatabase : IRepository<Employee>
    {
        private const string c_employeesDatabaseName = "Employees";

        public void Delete(int id)
        {
            var queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_employeesDatabaseName, id);
            AdoHelper.ExecCommand(queryString);
        }

        public void Update(int id, Employee entity)
        {
            var queryString = String.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}",
                c_employeesDatabaseName,
                entity.Name,
                id);
            AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Employee entity)
        {
            var queryString = String.Format("INSERT INTO {0} (DepartmentId, Name) VALUES ({1}, '{2}');",
                c_employeesDatabaseName, entity.ParentDepartment.Id, entity.Name);
            AdoHelper.ExecCommand(queryString);
        }

        public List<Employee> GetAll()
        {
            var resultList = new List<Employee>();
            var queryString = String.Format("SELECT * FROM {0};", c_employeesDatabaseName);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                var repositoryDepartmentDb = new RepositoryDepartmentDatabase();
                var repositoryOrganizationDb = new RepositoryOrganizationDatabase();
                while (reader.Read())
                {
                    var employeeDb = MapperDb.GetEmployeeDb(reader);
                    var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
                    var organization = repositoryOrganizationDb.GetById(department.ParentOrganization.Id);
                    resultList.Add(MapperBm.GetEmployee(employeeDb, department, organization));
                }
            }
            return resultList;
        }

        public Employee GetById(int id)
        {
            var queryString = String.Format("SELECT TOP 1 * FROM {0} WHERE Id = {1};", c_employeesDatabaseName, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            EmployeeDb employeeDb = null;

            var repositoryDepartmentDb = RegisterByContainer.Container.GetInstance<IRepository<Department>>();
            var repositoryOrganizationDb = RegisterByContainer.Container.GetInstance<IRepository<Organization>>();

            if (reader.Read())
            {
                employeeDb = MapperDb.GetEmployeeDb(reader);
                var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
                var organization = repositoryOrganizationDb.GetById(department.ParentOrganization.Id);
                return MapperBm.GetEmployee(employeeDb, department, organization);
            }
            return null;
        }

        public Employee GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
