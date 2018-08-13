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
        private NavigationService navigationService;
        public AppViewModel appViewModel;
        public LogInViewModel logInViewModel;
        public ForgetPassViewModel forGetPassViewModel;
        public RegisterEmailViewModel registerEmailViewModel;
        public HomeViewModel homeViewModel;
        public SendCodeEmailViewModel sendCodeEmailViewModel;
        public ConfirmCodeViewModel confirmationCodeViewModel;
        public SignUpViewModel signUpViewModel;
        public ViewModelLocator()
        {
            navigationService = new NavigationService();

            appViewModel = new AppViewModel();
            logInViewModel = new LogInViewModel(navigationService);
            registerEmailViewModel = new RegisterEmailViewModel(navigationService);
            forGetPassViewModel = new ForgetPassViewModel(navigationService);
            sendCodeEmailViewModel = new SendCodeEmailViewModel(navigationService);
            homeViewModel = new HomeViewModel(navigationService);
            confirmationCodeViewModel = new ConfirmCodeViewModel(navigationService);
            signUpViewModel = new SignUpViewModel(navigationService);

            navigationService.AddPage(signUpViewModel,ViewType.SignUp);
            navigationService.AddPage(confirmationCodeViewModel,ViewType.ConfirmCode);
            navigationService.AddPage(registerEmailViewModel, ViewType.RegisterEmail);
            navigationService.AddPage(forGetPassViewModel, ViewType.ForgetPass);
            navigationService.AddPage(sendCodeEmailViewModel, ViewType.ForgotEmailCode);
            navigationService.AddPage(logInViewModel,ViewType.LogIn);
            navigationService.AddPage(homeViewModel, ViewType.Home);
            navigationService.NavigateTo(ViewType.LogIn);
        }
    }
}