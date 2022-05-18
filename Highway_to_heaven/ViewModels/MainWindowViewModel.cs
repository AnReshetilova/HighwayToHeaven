using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string title = "Энциклопедия космоса";
        private string header;
        private readonly NavigationStore navigationStore;
        private string viewMenu = "Collapsed";
        private string adminFunction;
        private string userFunction;
        private User userInfo;
        Action<User> setUser;
        Func<User> getUser;

        public string AdminFunction
        {
            get => adminFunction;
            set => Set(ref adminFunction, value);
        }
        public string UserFunction
        {
            get => userFunction;
            set => Set(ref userFunction, value);
        }
        public string ViewMenu
        {
            get => viewMenu;
            set => Set(ref viewMenu, value);
        }

        public ViewModel CurrentViewModel => navigationStore.CurrentViewModel;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string Header
        {
            get => header;
            set => Set(ref header, value);
        }

        public ICommand OpenToursInfoCommand { get; }
        public ICommand OpenLoginCommand { get; }
        private ICommand openLoginCommand;
        public ICommand OpenAccountInfoCommand { get; }
        public ICommand OpenAddTourCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand OpenAddPlanetCommand { get; }
        public ICommand OpenPlanetsInfoCommand { get; }
        public ICommand OpenStatisticCommand { get; }

        public MainWindowViewModel(NavigationStore navigationStore, Func<ViewModel> createLoginViewModel, Func<ViewModel> createToursInfoViewModel, Func<ViewModel> createAccountViewModel,
            Func<User> getUser, Func<ViewModel> createAddTourViewModel, Func<ViewModel> createSettingsViewModel, Func<ViewModel> createAddPlanetViewModel, Action<User> setUser, Func<ViewModel> createPlanetsViewModel,
            Func<ViewModel> createStatisticsViewModel)
        {
            this.navigationStore = navigationStore;

            OpenToursInfoCommand = new NavigateCommand(this.navigationStore, createToursInfoViewModel);
            OpenLoginCommand = new ExternalCommand(OnOpenLoginCommand);
            openLoginCommand = new NavigateCommand(this.navigationStore, createLoginViewModel);
            OpenAccountInfoCommand = new NavigateCommand(this.navigationStore, createAccountViewModel);
            OpenAddTourCommand = new NavigateCommand(this.navigationStore, createAddTourViewModel);
            OpenSettingsCommand = new NavigateCommand(this.navigationStore, createSettingsViewModel);
            OpenAddPlanetCommand = new NavigateCommand(this.navigationStore, createAddPlanetViewModel);
            OpenPlanetsInfoCommand = new NavigateCommand(this.navigationStore, createPlanetsViewModel);
            OpenStatisticCommand = new NavigateCommand(this.navigationStore, createStatisticsViewModel);

            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            this.getUser = getUser;
            this.userInfo = getUser();
            this.setUser = setUser;
        }

        private void OnOpenLoginCommand(object o)
        {
            setUser(null);
            userInfo = null;
            openLoginCommand.Execute(o);
        }

        private void OnCurrentViewModelChanged()
        {
            userInfo = getUser();
            if (userInfo != null && userInfo.Login != null)
            {
                ViewMenu = "Visible";
                if (userInfo.IsAdmin == true)
                {
                    AdminFunction = "Visible";
                    UserFunction = "Collapsed";
                }
                else
                {
                    UserFunction = "Visible";
                    AdminFunction = "Collapsed";
                }
            }
            else
            {
                ViewMenu = "Collapsed";
            }
            InvokePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
