using lab6_7.Stores;
using lab6_7.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace lab6_7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly NavigationStore navigationStore;

        public App()
        {
            navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateLoginViewModel();
            //navigationStore.CurrentViewModel = new LoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore, CreateLoginViewModel, CreateToursInfoViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel();
        }

        private ToursInfoViewModel CreateToursInfoViewModel()
        {
            return new ToursInfoViewModel();
        }
    }
}
