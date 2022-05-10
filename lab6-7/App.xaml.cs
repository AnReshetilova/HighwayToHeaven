using lab6_7.Stores;
using lab6_7.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using lab6_7.Services;
using lab6_7.Models;

namespace lab6_7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;
        private readonly UserService userService;
        private User userInfo;

        public App()
        {
            HIGHWAY_TO_HEAVENContext context = new HIGHWAY_TO_HEAVENContext();
            navigationStore = new NavigationStore();
            userService = new UserService(context);
            userInfo = new User();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore, CreateLoginViewModel, CreateToursInfoViewModel, CreateAccountViewModel, SetUser)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private User SetUser()
        {
            return userInfo;
        }

        private void SetUser(User userInfo)
        {
            this.userInfo = userInfo;
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(userService, SetUser);
        }

        private ToursInfoViewModel CreateToursInfoViewModel()
        {
            return new ToursInfoViewModel();
        }

        private AccountViewModel CreateAccountViewModel()
        {
            return new AccountViewModel(SetUser);
        }
    }
}
