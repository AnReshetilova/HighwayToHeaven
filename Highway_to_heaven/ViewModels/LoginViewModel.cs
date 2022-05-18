using Highway_to_heaven.Commands;
using Highway_to_heaven.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Highway_to_heaven.Services;
using Highway_to_heaven.Models;
using Highway_to_heaven.Stores;

namespace Highway_to_heaven.ViewModels
{
    class LoginViewModel : ViewModel
    {
        private string username = "admin";
        private string password = "admin";
        private string info;
        private User userInfo;
        private readonly UserService userService;
        Action<User> setUser;

        public string Info
        {
            get => info;
            set
            {
                info = value;
                InvokePropertyChanged(nameof(Info));
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                InvokePropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                InvokePropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand OpenRegistrationCommand { get; }
        private ICommand openRegistrationCommand;
        private ICommand openAccountViewModel;

        public LoginViewModel(UserService userService, Action<User> setUser, NavigationStore navigationStore, Func<ViewModel> createRegistrationViewModel, Func<ViewModel> createAccountViewModel)
        {
            this.userService = userService;
            this.userInfo = new User();
            this.setUser = setUser;

            LoginCommand = new ExternalCommand(onLoginCommandExecute);
            openRegistrationCommand = new NavigateCommand(navigationStore, createRegistrationViewModel);
            openAccountViewModel = new NavigateCommand(navigationStore, createAccountViewModel);
            OpenRegistrationCommand = new ExternalCommand(onOpenRegistrationCommand);
        }

        private void onLoginCommandExecute(object o)
        {
            userInfo = userService.GetUserByLogin(username);

            if (userInfo == null)
            {
                Info = "Not correct";
            }
            else if (userInfo.Password.Equals(password))
            {
                Info = "Excellent";
            }
            setUser(userInfo);

            openAccountViewModel.Execute(o);
        }

        private void onOpenRegistrationCommand(object o)
        {
            setUser(null);
            userInfo = null;
            openRegistrationCommand.Execute(o);
        }
    }
}
