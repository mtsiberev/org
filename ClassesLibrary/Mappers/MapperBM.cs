using Organizations.DbEntity;

namespace Organizations.Mappers
{
    public static class MapperBm
    {
        public static Employee GetEmployee(EmployeeDb employeeDb, Department department)
        {
            return new Employee(employeeDb.Id, department)
            {
                Name = employeeDb.Name
            };
        }

        public static Department GetDepartment(DepartmentDb departmentDb, Organization organization)
        {
            return new Department(departmentDb.Id, organization)
            {
                Name = departmentDb.Name
            };
        }

        public static Organization GetOrganization(OrganizationDb organizationDb)
        {
                return new Organization(organizationDb.Id)
                {
                    Name = organizationDb.Name
                };
        }

    }
}
