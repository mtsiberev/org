using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Organizations.DbEntity
{
    public class RepositoryDb<T> : IRepository<T> where T : class, IEntityDb
    {
        private const string c_employeesDb = "Employees";
        private const string c_departmentsDb = "Departments";
        private const string c_organizationsDb = "Organizations";
        
        public void Delete(T entity)
        {
            var queryString = "";
            if (typeof(T) == typeof(EmployeeDb))
            {
                var tempEntity = entity as EmployeeDb;
                queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                    c_employeesDb, tempEntity.Id);
            }

            if (typeof(T) == typeof(DepartmentDb))
            {
                var tempEntity = entity as DepartmentDb;
                queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                    c_departmentsDb, tempEntity.Id);
            }
            
            if (typeof(T) == typeof(OrganizationDb))
            {
                var tempEntity = entity as OrganizationDb;
                queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                    c_organizationsDb, tempEntity.Id);
            }
            
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public void Insert(T entity)
        {
            var queryString = "";
            if (typeof(T) == typeof(EmployeeDb))
            {
                var tempEntity = entity as EmployeeDb;
                queryString = String.Format("INSERT INTO {0} (DepartmentId, Name) VALUES ({1}, '{2}');",
                    c_employeesDb, tempEntity.ParentDepartment, tempEntity.Name);
            }

            if (typeof(T) == typeof(DepartmentDb))
            {
                var tempEntity = entity as DepartmentDb;
                queryString = String.Format("INSERT INTO {0} (OrganizationId, Name) VALUES ({1}, '{2}');",
                    c_departmentsDb, tempEntity.ParentOrganization, tempEntity.Name);
            }

            if (typeof(T) == typeof(OrganizationDb))
            {
                var tempEntity = entity as OrganizationDb;
                queryString = String.Format("INSERT INTO {0} (Name) VALUES ('{1}');",
                    c_organizationsDb, tempEntity.Name);
            }

            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public List<T> GetAll()
        {
            var queryString = "";
            var resultList = new List<T>();

            if (typeof(T) == typeof(EmployeeDb))
                queryString = String.Format("SELECT * FROM {0};", c_employeesDb);
            if (typeof(T) == typeof(DepartmentDb))
                queryString = String.Format("SELECT * FROM {0};", c_departmentsDb);
            if (typeof(T) == typeof(OrganizationDb))
                queryString = String.Format("SELECT * FROM {0};", c_organizationsDb);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    resultList.Add(MapperDB.GetObject(typeof(T), reader));
                }
            }
            return resultList;
        }

        public T GetById(int id)
        {
            var queryString = "";

            if (typeof(T) == typeof(EmployeeDb))
                queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_employeesDb, id);
            if (typeof(T) == typeof(DepartmentDb))
                queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_departmentsDb, id);
            if (typeof(T) == typeof(OrganizationDb))
                queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_organizationsDb, id);

            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.Read())
                return MapperDB.GetObject(typeof(T), reader);
            return null;
        }

        public T GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
