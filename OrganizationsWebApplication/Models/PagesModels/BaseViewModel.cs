
namespace OrganizationsWebApplication.Models.PagesModels
{
    public class BaseViewModel
    {
        public ViewState ViewStateProperty;

        public BaseViewModel()
        {
            ViewStateProperty = new ViewState();
        }
    }
}