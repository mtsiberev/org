using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class RepositoryDepartmentDatabase : IRepository<Department>
    {
        private const string c_departmentsDatabaseName = "Departments";

        private string GetDeletingQueryString(int id)
        {
            return String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_departmentsDatabaseName, id);
        }

        public void AddDeletingQuery(int id)
        {
            var repositoryEmployeeDb = new RepositoryEmployeeDatabase();
            var employeeList = repositoryEmployeeDb.GetAll();
            foreach (var employee in employeeList)
            {
                if (employee.ParentDepartment.Id == id)
                {
                    repositoryEmployeeDb.AddDeletingQuery(employee.Id);
                }
            }

            var queryString = GetDeletingQueryString(id);
            AdoHelper.Instance.AddQuery(queryString);
        }

        public void Delete(int id)
        {
            AddDeletingQuery(id);
            AdoHelper.Instance.ExecCommand();
        }

        public void Insert(Department entity)
        {
            var queryString = String.Format("INSERT INTO {0} (OrganizationId, Name) VALUES ({1}, '{2}');",
                c_departmentsDatabaseName, entity.ParentOrganization.Id, entity.Name);
            if (queryString.Length != 0)
            {
                AdoHelper.Instance.AddQuery(queryString);
                AdoHelper.Instance.ExecCommand();
            }
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
            var queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_departmentsDatabaseName, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            DepartmentDb departmentDb = null;
         
            var repositoryOrganizationDb = RegisterByContainer.Container.GetInstance<IRepository<Organization>>();

            if (reader.Read())
            {
                departmentDb = MapperDb.GetDepartmentDb(reader);
            }
            var organization = repositoryOrganizationDb.GetById(departmentDb.ParentOrganizationId);
            return MapperBm.GetDepartment(departmentDb, organization);
        }

        public Department GetRandom()
        {
            throw new NotImplementedException();
        }

    }
}
