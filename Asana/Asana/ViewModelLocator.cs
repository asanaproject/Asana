using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private NavigationService navigationService;
        private IUserService userService;
        public AppViewModel appViewModel;
        public LogInViewModel logInViewModel;
        public ForgetPassViewModel forGetPassViewModel;
        public RegisterEmailViewModel registerEmailViewModel;
        public HomeViewModel homeViewModel;
        public SendCodeEmailViewModel sendCodeEmailViewModel;
        public ConfirmCodeViewModel confirmationCodeViewModel;
        public SignUpViewModel signUpViewModel;
        public CreateProjectViewModel createProjectViewModel;
        public ProjectPageViewModel projectPageViewModel;
        public ChatViewModel chatViewModel;
        public ListChannelsViewModel listChannelsViewModel;
        public ProfileViewModel profileViewModel;
        
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
            createProjectViewModel = new CreateProjectViewModel(navigationService);
            projectPageViewModel = new ProjectPageViewModel(navigationService);
            listChannelsViewModel = new ListChannelsViewModel(navigationService);
            chatViewModel = new ChatViewModel(navigationService);
            profileViewModel = new ProfileViewModel(navigationService);

            navigationService.AddPage(signUpViewModel, ViewType.SignUp);
            navigationService.AddPage(confirmationCodeViewModel, ViewType.ConfirmCode);
            navigationService.AddPage(registerEmailViewModel, ViewType.RegisterEmail);
            navigationService.AddPage(forGetPassViewModel, ViewType.ForgetPass);
            navigationService.AddPage(sendCodeEmailViewModel, ViewType.ForgotEmailCode);
            navigationService.AddPage(homeViewModel, ViewType.Home);
            navigationService.AddPage(logInViewModel, ViewType.LogIn);
            navigationService.AddPage(createProjectViewModel, ViewType.CreateProject);
            navigationService.AddPage(chatViewModel, ViewType.ChatView);
            navigationService.AddPage(projectPageViewModel, ViewType.ProjectPage);
            navigationService.AddPage(listChannelsViewModel, ViewType.ListChannels);
            navigationService.AddPage(profileViewModel, ViewType.Profile);

            userService = new UserService();
            string user = CheckLoginLog.Load();

            if (user != "" && userService.Select(user) != null)
            {
                CurrentUser.Instance.User = userService.Select(user);
                navigationService.NavigateTo(ViewType.Profile);
            }
            else
                navigationService.NavigateTo(ViewType.LogIn);

        }
    }
}
