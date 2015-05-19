﻿using System;
using System.Collections.Generic;
using Organizations.DbEntity;
using Organizations.Helpers;
using Organizations.Mappers;

namespace Organizations.DbRepository
{
    public class RepositoryDepartmentDatabase : IRepository<Department>
    {
        private const string c_departmentsDatabaseName = "Departments";

        public void Delete(int id)
        {
            var queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_departmentsDatabaseName, id);
            AdoHelper.ExecCommand(queryString);
        }

        public void Update(int id, Department entity)
        {
            var queryString = String.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}",
                c_departmentsDatabaseName,
                entity.Name,
                id);
            AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Department entity)
        {
            var queryString = String.Format("INSERT INTO {0} (OrganizationId, Name) VALUES ({1}, '{2}');",
                c_departmentsDatabaseName, entity.ParentOrganization.Id, entity.Name);
            AdoHelper.ExecCommand(queryString);
        }

        public List<Department> GetAll()
        {
            var resultList = new List<Department>();
            var queryString = String.Format("SELECT * FROM {0};", c_departmentsDatabaseName);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                var repositoryOrganizationDb = RegisterByContainer.Container.GetInstance<IRepository<Organization>>();

                while (reader.Read())
                {
                    var departmentDb = MapperDb.GetDepartmentDb(reader);
                    var organization = repositoryOrganizationDb.GetById(departmentDb.ParentOrganizationId);
                    resultList.Add(MapperBm.GetDepartment(departmentDb, organization));
                }
            }
            return resultList;
        }

        public Department GetById(int id)
        {
            var queryString = String.Format("SELECT TOP 1 * FROM {0} WHERE Id = {1};", c_departmentsDatabaseName, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            DepartmentDb departmentDb = null;

            var repositoryOrganizationDb = RegisterByContainer.Container.GetInstance<IRepository<Organization>>();

            if (reader.Read())
            {
                departmentDb = MapperDb.GetDepartmentDb(reader);
                var organization = repositoryOrganizationDb.GetById(departmentDb.ParentOrganizationId);
                return MapperBm.GetDepartment(departmentDb, organization);
            }
            return null;
        }

        public Department GetRandom()
        {
            throw new NotImplementedException();
        }

    }
}