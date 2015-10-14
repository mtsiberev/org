using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.PagesModels;

namespace OrganizationsWebApplication.Helpers
{
    public class MainCommandsViewHelper
    {
        public string ControllerName { get; private set; }

        public string ChangeSortingAction { get; private set; }
        public string AddNewEntityAction { get; private set; }
        public string GoNextPageAction { get; private set; }
        public string GoPrevPageAction { get; private set; }

        public string GoHomeController { get; private set; }
        public string GoHomeAction { get; private set; }
      
        public MainCommandsViewHelper(BaseViewModel viewModel)
        {
            var fullname = viewModel.GetType().Name;
            ControllerName = fullname.Replace("ViewModel", "");
            
            if (ControllerName == "Administration")
            {
                AddNewEntityAction = null;
                GoHomeController = "OrganizationsList";
                GoHomeAction = "OrganizationsList";
            }

            if (ControllerName == "OrganizationsList")
            {
                AddNewEntityAction = "AddOrganizationMenu";
                GoHomeController = null;
                GoHomeAction = null;
            }

            if (ControllerName == "OrganizationInfo")
            {
                AddNewEntityAction = "AddDepartmentMenu";
                GoHomeController = "OrganizationsList";
                GoHomeAction = "OrganizationsList";
            }

            if (ControllerName == "DepartmentInfo")
            {
                AddNewEntityAction = null;
                GoHomeController = "OrganizationInfo";
                GoHomeAction = "OrganizationInfo";
            }

            ChangeSortingAction = "ChangeSortType";
            GoNextPageAction = "GoNextPage";
            GoPrevPageAction = "GoPrevPage";
        }
    }
}