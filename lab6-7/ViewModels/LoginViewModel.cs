using lab6_7.Commands;
using lab6_7.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab6_7.ViewModels
{
    class LoginViewModel : ViewModel
    {
        string username;
        string password;

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

        public LoginViewModel()
        {
            LoginCommand = new LoginCommand();
        }
    }
}
