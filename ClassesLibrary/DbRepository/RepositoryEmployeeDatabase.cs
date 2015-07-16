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

        public void Update(Employee entity)
        {
            var queryString = String.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}",
                c_employeesDatabaseName,
                entity.Name,
                entity.Id);
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
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    var repositoryDepartmentDb = RegisterByContainer.Container.GetInstance<IRepository<Department>>();

                    while (reader.Read())
                    {
                        var employeeDb = MapperDb.GetEmployeeDb(reader);
                        var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
                        resultList.Add(MapperBm.GetEmployee(employeeDb, department));
                    }
                }
            }
            return resultList;
        }


        public int GetCount(int departmentId)
        {
            int result = 0;
            var queryString = String.Format("SELECT COUNT(*) FROM {0} WHERE DepartmentId = {1};", c_employeesDatabaseName, departmentId);
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = (int)reader.GetValue(0);
                    }
                }
            }
            return result;
        }


        public Employee GetById(int id)
        {
            var queryString = String.Format("SELECT TOP 1 * FROM {0} WHERE Id = {1};", c_employeesDatabaseName, id);
            var repositoryDepartmentDb = RegisterByContainer.Container.GetInstance<IRepository<Department>>();
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.Read())
                {
                    EmployeeDb employeeDb = MapperDb.GetEmployeeDb(reader);
                    var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
                    return MapperBm.GetEmployee(employeeDb, department);
                }
            }
            return null;
        }

        public Employee GetRandom()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEntitiesForOnePage(int pageNum, int pageSize, int parentId)
        {
            var repositoryDepartmentDb = RegisterByContainer.Container.GetInstance<IRepository<Department>>();
            var resultList = new List<Employee>();
         
            var queryString = String.Format(
                "SELECT * FROM {0} " +
                "WHERE DepartmentId = {1} " +
                "ORDER BY Name OFFSET ({2} - 1) * {3} ROWS " +
                "FETCH NEXT {3} ROWS ONLY;",
                c_employeesDatabaseName, parentId, pageNum, pageSize);
         
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EmployeeDb employeeDb = MapperDb.GetEmployeeDb(reader);
                        var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
                        resultList.Add(MapperBm.GetEmployee(employeeDb, department));
                    }
                }
            }
            return resultList;
        }

    }
}
