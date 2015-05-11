using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class RepositoryOrganizationDatabase : IRepository<Organization>
    {
        private const string c_organizationsDatabaseName = "Organizations";

        private string GetDeletingQueryString(int id)
        {
            return String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_organizationsDatabaseName, id);
        }

        public void Delete(int id)
        {
            var repositoryDepartmentDb = new RepositoryDepartmentDatabase();
            //var repositoryDepartmentDb = RegisterByContainer.Container.GetInstance<IRepository<Department>>();

            var departmentList = repositoryDepartmentDb.GetAll();
            foreach (var department in departmentList)
            {
                if (department.ParentOrganization.Id == id)
                {
                    repositoryDepartmentDb.AddDeletingQuery(department.Id);//////////////
                }
            }

            var queryString = GetDeletingQueryString(id);
            AdoHelper.Instance.AddQuery(queryString);
            AdoHelper.Instance.ExecCommand();
        }

        public void Insert(Organization entity)
        {
            var queryString = String.Format("INSERT INTO {0} (Name) VALUES ('{1}');",
                c_organizationsDatabaseName, entity.Name);
            if (queryString.Length != 0)
            {
                AdoHelper.Instance.AddQuery(queryString);
                AdoHelper.Instance.ExecCommand();
            }
        }

        public List<Organization> GetAll()
        {
            var resultList = new List<Organization>();
            var queryString = String.Format("SELECT * FROM {0};", c_organizationsDatabaseName);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var organizationDb = MapperDb.GetOrganizationDb(reader);
                    resultList.Add(MapperBm.GetOrganization(organizationDb));
                }
            }
            return resultList;
        }

        public Organization GetById(int id)
        {
            var queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_organizationsDatabaseName, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            OrganizationDb organizationDb = null;
            if (reader.Read())
                organizationDb = MapperDb.GetOrganizationDb(reader);
            return MapperBm.GetOrganization(organizationDb);
        }

        public Organization GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
