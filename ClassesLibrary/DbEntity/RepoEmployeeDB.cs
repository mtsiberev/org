using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbEntity
{
    public class RepoEmployeeDb : IRepository<Employee>
    {
        private const string c_employeesDb = "Employees";
     
        public void Delete(int id)
        {
            var queryString = "";
            queryString = String.Format("DELETE FROM {0} WHERE Id = {1};",
                c_employeesDb, id);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public void Insert(Employee entity)
        {
            var queryString = "";
            queryString = String.Format("INSERT INTO {0} (DepartmentId, Name) VALUES ({1}, '{2}');",
                c_employeesDb, entity.ParentDepartment.Id, entity.Name);
            if (queryString.Length != 0)
                AdoHelper.ExecCommand(queryString);
        }

        public List<Employee> GetAll()
        {
            var queryString = "";
            var resultList = new List<Employee>();
            queryString = String.Format("SELECT * FROM {0};", c_employeesDb);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            if (reader.HasRows)
            {
                var repositoryDepartmentDb = new RepoDepartmentDb();
                var repositoryOrganizationDb = new RepoOrganizationDb();
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
            var queryString = "";
            queryString = String.Format("SELECT * FROM {0} WHERE Id = {1};", c_employeesDb, id);
            var table = AdoHelper.GetDataTable(queryString);
            var reader = AdoHelper.GetDataTableReader(table);
            EmployeeDb employeeDb = null;
            var repositoryDepartmentDb = new RepoDepartmentDb();
            var repositoryOrganizationDb = new RepoOrganizationDb();

            if (reader.Read())
            {
                employeeDb = MapperDb.GetEmployeeDb(reader);
            }

            var department = repositoryDepartmentDb.GetById(employeeDb.ParentDepartmentId);
            var organization = repositoryOrganizationDb.GetById(department.ParentOrganization.Id);
            return MapperBm.GetEmployee(employeeDb, department, organization);
        }

        public Employee GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
