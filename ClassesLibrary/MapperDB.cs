using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.DbEntity;

namespace Organizations
{
    public static class MapperDB
    {
        public static EmployeeDb GetEmployeeDb(DataTableReader reader)
        {
            var id = (int)reader.GetValue(0);
            var parentId = (int)reader.GetValue(1);
            var name = reader.GetValue(2).ToString();
            return new EmployeeDb(id, parentId) { Name = name };
        }

        public static DepartmentDb GetDepartmentDb(DataTableReader reader)
        {
            var id = (int)reader.GetValue(0);
            var parentId = (int)reader.GetValue(1);
            var name = reader.GetValue(2).ToString();
            return new DepartmentDb(id, parentId) { Name = name };
        }

        public static OrganizationDb GetOrganizationDb(DataTableReader reader)
        {
            var id = (int)reader.GetValue(0);
            var name = reader.GetValue(1).ToString();
            return new OrganizationDb(id) { Name = name };
        }

        /*
        public static dynamic GetObject(Type T, DataTableReader reader)
        {
            if (T == typeof(EmployeeDb))
                return GetEmployeeDb(reader);
            if (T == typeof(DepartmentDb))
                return GetDepartmentDb(reader);
            if (T == typeof(OrganizationDb))
                return GetOrganizationDb(reader);
            return null;
        }
        */
    }
}
