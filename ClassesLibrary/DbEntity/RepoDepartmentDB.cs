using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class RepoDepartmentDb : IRepository<Department>
    {
        private const string c_departmentsDb = "Departments";

        public void Delete(int id)
        {
            var repositoryEmployeeDb = new RepoEmployeeDb();

            var employeeList = repositoryEmployeeDb.GetAll();
            foreach (var employee in employeeList)
            {
                if (employee.ParentDepartment.Id == id)
                {
                    repositoryEmployeeDb.Delete(employee.Id);
                }
            }

            var queryString = "";
            queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_departmentsDb, id);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Department entity)
        {
            var queryString = "";
            queryString = String.Format("INSERT INTO {0} (OrganizationId, Name) VALUES ({1}, '{2}');",
                c_departmentsDb, entity.ParentOrganization.Id, entity.Name);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public List<Department> GetAll()
        {
            var queryString = "";
            var resultList = new List<Department>();
            queryString = String.Format("SELECT * FROM {0};", c_departmentsDb);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                var repositoryOrganizationDb = new RepoOrganizationDb();
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
            var queryString = "";
            queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_departmentsDb, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            DepartmentDb departmentDb = null;
            var repositoryOrganizationDb = new RepoOrganizationDb();
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
