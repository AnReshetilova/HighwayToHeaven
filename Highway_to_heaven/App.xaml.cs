using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Highway_to_heaven.Services;
using Highway_to_heaven.Models;

namespace Highway_to_heaven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;
        private readonly UserService userService;
        private readonly TravelService travelService;
        private User userInfo;

        public App()
        {
            HIGHWAY_TO_HEAVENContext context = new HIGHWAY_TO_HEAVENContext();
            navigationStore = new NavigationStore();
            userService = new UserService(context);
            travelService = new TravelService(context);
            userInfo = new User();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore, CreateLoginViewModel, CreateToursInfoViewModel, CreateAccountViewModel, GetUser)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private User GetUser()
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
            return new ToursInfoViewModel(travelService);
        }

        private AccountViewModel CreateAccountViewModel()
        {
            return new AccountViewModel(GetUser);
        }
    }
}
