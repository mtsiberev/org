using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.DbEntity;

namespace Organizations
{
    public static class MapperBm
    {
        public static Employee GetEmployee(
            EmployeeDb employeeDb, 
            Department department, 
            Organization organization)
        {
            return new Employee(
                employeeDb.Id, 
                new Department(
                    department.Id, 
                    new Organization(
                        organization.Id)))
                        {Name = employeeDb.Name};
        }

        public static Department GetDepartment(DepartmentDb departmentDb,
            Organization organization)
        {
            return new Department(
                departmentDb.Id, 
                new Organization(
                    organization.Id))
                    {Name = departmentDb.Name};
        }

        public static Organization GetOrganization(OrganizationDb organizationDb)
        {
                return new Organization(organizationDb.Id)
                {Name = organizationDb.Name};
        }

    }
}
