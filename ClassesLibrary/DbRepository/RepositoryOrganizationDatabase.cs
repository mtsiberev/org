using System;
using System.Collections.Generic;
using System.Linq;
using Organizations.Helpers;
using Organizations.Mappers;

namespace Organizations.DbRepository
{
    public class RepositoryOrganizationDatabase : IRepository<Organization>
    {
        private const string c_organizationsDatabaseName = "Organizations";

        public void Delete(int id)
        {
            var queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_organizationsDatabaseName, id);
            AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Organization entity)
        {
            var queryString = String.Format("INSERT INTO {0} (Name) VALUES ('{1}');",
                c_organizationsDatabaseName, entity.Name);
            AdoHelper.ExecCommand(queryString);
        }

        public void Update(Organization entity)
        {
            var queryString = String.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}",
                c_organizationsDatabaseName,
                entity.Name,
                entity.Id
                );
            AdoHelper.ExecCommand(queryString);
        }

        public List<Organization> GetAll()
        {
            var resultList = new List<Organization>();
            var queryString = String.Format("SELECT * FROM {0};", c_organizationsDatabaseName);
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var organizationDb = MapperDb.GetOrganizationDb(reader);
                        resultList.Add(MapperBm.GetOrganization(organizationDb));
                    }
                }
            }
            return resultList;
        }

        public int GetCount()
        {
            int result = 0;
            var queryString = String.Format("SELECT COUNT(*) FROM {0};", c_organizationsDatabaseName);
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
        
        public List<Organization> GetEntitiesForOnePage(int pageNum, int pageSize, string sortType)
        {
            var resultList = new List<Organization>();

            var queryString = String.Format(
                "SELECT * FROM {0} WHERE id IN " +
                "(SELECT id FROM {0} ORDER BY Name " +
                "OFFSET ({1} - 1) * {2} ROWS " +
                "FETCH NEXT {2} ROWS ONLY ) ORDER BY Name {3};",
                c_organizationsDatabaseName, pageNum, pageSize, sortType);

            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var organizationDb = MapperDb.GetOrganizationDb(reader);
                        resultList.Add(MapperBm.GetOrganization(organizationDb));
                    }
                }
            }
            return resultList;
        }

        public Organization GetById(int id)
        {
            var queryString = String.Format("SELECT TOP 1 * FROM {0} WHERE Id = {1};", c_organizationsDatabaseName, id);
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.Read())
                {
                    var organizationDb = MapperDb.GetOrganizationDb(reader);
                    return MapperBm.GetOrganization(organizationDb);
                }
            }
            return null;
        }

        public Organization GetRandom()
        {
            throw new NotImplementedException();
        }


    }
}
