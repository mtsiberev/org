using System.Collections.Generic;

namespace OrganizationsWebApplication.Models
{
    public class DepartmentWithEmployeesViewModel
    {
        public int Id { get; set; }
        public List<EmployeeViewModel> Employees { get;  set; }
        public string Name { get; set; }
    }
}