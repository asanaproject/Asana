using Asana.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;


namespace Asana.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public AppViewModel appViewModel;
        public LogInViewModel logInViewModel;
        public ForgetPassViewModel forGetPassViewModel;
        public SignUpViewModel signUpViewModel;

        private NavigationService navigationService;

        public ViewModelLocator()
        {
            navigationService = new NavigationService();

            appViewModel = new AppViewModel();
            logInViewModel = new LogInViewModel(navigationService);
            forGetPassViewModel = new ForgetPassViewModel(navigationService);
            signUpViewModel = new SignUpViewModel(navigationService);

            navigationService.AddPage(forGetPassViewModel, ViewType.ForgetPass);
            navigationService.AddPage(signUpViewModel, ViewType.SignUp);
            navigationService.AddPage(logInViewModel,ViewType.LogIn);
            navigationService.NavigateTo(ViewType.LogIn);
        }
    }
}