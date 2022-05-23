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
        private readonly PackageTour currentTour;
        private User userInfo;

        public App()
        {
            HIGHWAY_TO_HEAVENContext context = new HIGHWAY_TO_HEAVENContext();
            navigationStore = new NavigationStore();
            userService = new UserService(context);
            travelService = new TravelService(context);
            userInfo = new User();
            currentTour = new PackageTour();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore, CreateLoginViewModel, CreateToursInfoViewModel, CreateAccountViewModel, GetUser, 
                    CreateAddTourViewModel, CreateSettingsViewModel, CreateAddPlanetViewModel, SetUser, CreatePlanetsViewModel, CreateStatisticsViewModel)
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

        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel();
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(userService, SetUser, navigationStore, CreateRegistrationViewModel, CreateAccountViewModel);
        }

        private ToursInfoViewModel CreateToursInfoViewModel()
        {
            return new ToursInfoViewModel(navigationStore, travelService, currentTour, CreateTourInfoViewModel, userInfo);
        }

        private AccountViewModel CreateAccountViewModel()
        {
            return new AccountViewModel(GetUser, userService);
        }

        private TourInfoViewModel CreateTourInfoViewModel(object tour)
        {
            return new TourInfoViewModel(tour as PackageTour, navigationStore, CreateCommentsViewModel, CreateQuestionViewModel, travelService, userInfo);
        }

        private AddTourViewModel CreateAddTourViewModel()
        {
            return new AddTourViewModel(travelService);
        }

        private RegistrationViewModel CreateRegistrationViewModel()
        {
            return new RegistrationViewModel(userService, navigationStore, CreateLoginViewModel);
        }

        private AddPlanetViewModel CreateAddPlanetViewModel()
        {
            return new AddPlanetViewModel(travelService);
        }

        private CommentsViewModel CreateCommentsViewModel(object tour)
        {
            return new CommentsViewModel(userService, tour as PackageTour, userInfo);
        }

        private PlanetsViewModel CreatePlanetsViewModel()
        {
            return new PlanetsViewModel(travelService);
        }

        private QuestionViewModel CreateQuestionViewModel(object tour)
        {
            return new QuestionViewModel(travelService, userInfo, tour as PackageTour);
        }

        private StatisticsViewModel CreateStatisticsViewModel()
        {
            return new StatisticsViewModel(userService);
        }
    }
}
