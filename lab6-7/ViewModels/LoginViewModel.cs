using lab6_7.Commands;
using lab6_7.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using lab6_7.Services;
using lab6_7.Models;
using lab6_7.Stores;

namespace lab6_7.ViewModels
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

        public LoginViewModel(UserService userService, Action<User> setUser)
        {
            this.userService = userService;
            this.userInfo = new User();
            this.setUser = setUser;

            LoginCommand = new LoginCommand(onLoginCommandExecute);
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
