using Organizations;
using Organizations.EntitiesLists;
using OrganizationsWebApplication.Models.EntitiesModels;
using System.Linq;
using OrganizationsWebApplication.Models.PagesModels;

namespace OrganizationsWebApplication.Mappers
{
    public static class EntitiesListToView
    {
        private static Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        
        public static OrganizationsListViewModel GetOrganizationsListViewModel(OrganizationsList organizationsList)
        {
            var organizatonsList =
         from organization in organizationsList.Content
         select new OrganizationViewModel() { Id = organization.Id, Name = organization.Name };

            return new OrganizationsListViewModel(organizationsList.CurrentPage, organizationsList.MaxPageNumber, organizatonsList.ToList(), organizationsList.SortType);
        }

        public static OrganizationInfoViewModel GetOrganizationInfoViewModel(OrganizationWithDepartments organization)
        {
            var departmentList =
         from department in organization.Content
         select new DepartmentViewModel() { Id = department.Id, ParentId = organization.Id, Name = department.Name };

            return new OrganizationInfoViewModel(organization.Id, organization.CurrentPage, organization.MaxPageNumber, departmentList.ToList(), organization.Name, organization.SortType);
        }

        public static DepartmentInfoViewModel GetDepartmentInfoViewModel(DepartmentWithEmployees department)
        {
            var employeeList =
         from employee in department.Content
         select new EmployeeViewModel() { Id = employee.Id, ParentId = department.Id, Name = employee.Name };
            
            var departmentViewModel = new DepartmentInfoViewModel(department.ParentId, department.Id, department.CurrentPage, department.MaxPageNumber, employeeList.ToList(), department.SortType);
            departmentViewModel.Name = m_facade.GetDepartmentById(departmentViewModel.viewConditionProperty.Id).Name;
            return departmentViewModel;
        }
    }
}