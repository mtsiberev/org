using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class RepoOrganizationDb : IRepository<Organization>
    {
        private const string c_organizationsDb = "Organizations";
        public void Delete(Organization entity)
        {
            var queryString = "";
            queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_organizationsDb, entity.Id);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Organization entity)
        {
            var queryString = "";
            queryString = String.Format("INSERT INTO {0} (Name) VALUES ('{1}');",
                c_organizationsDb, entity.Name);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }


        public List<Organization> GetAll()
        {
            var queryString = "";
            var resultList = new List<Organization>();
            queryString = String.Format("SELECT * FROM {0};", c_organizationsDb);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //resultList.Add(MapperDB.GetObject(typeof(T), reader));
                }
            }
            return resultList;
        }
        
        public Organization GetById(int id)
        {
            var queryString = "";
            queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_organizationsDb, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            OrganizationDb organizationDb = null;
            if (reader.Read())
                organizationDb = MapperDB.GetOrganizationDb(reader);

            return MapperBM.GetOrganization(organizationDb);
        }
        
        public Organization GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
