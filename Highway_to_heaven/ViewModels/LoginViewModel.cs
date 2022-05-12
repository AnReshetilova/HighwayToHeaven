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
        private string username;
        private string password;
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

        public LoginViewModel(UserService userService, Action<User> setUser, NavigationStore navigationStore, Func<ViewModel> createRegistrationViewModel)
        {
            this.userService = userService;
            this.userInfo = new User();
            this.setUser = setUser;

            LoginCommand = new ExternalCommand(onLoginCommandExecute);
            OpenRegistrationCommand = new NavigateCommand(navigationStore, createRegistrationViewModel);
        }

        private void onLoginCommandExecute(object o)
        {
            userInfo = userService.GetUserByName(username);

            if (userInfo == null)
            {
                Info = "Not correct";
            }
            else if (userInfo.Password.Equals(password))
            {
                Info = "Excellent";
            }
            setUser(userInfo);
        }
    }
}
