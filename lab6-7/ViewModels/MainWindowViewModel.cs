using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using lab6_7.Commands;
using lab6_7.Stores;
using lab6_7.ViewModels.Base;

namespace lab6_7.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        string title = "Энциклопедия космоса";
        string header;
        readonly NavigationStore navigationStore;

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

        public MainWindowViewModel(NavigationStore navigationStore, Func<ViewModel> createLoginViewModel, Func<ViewModel> createToursInfoViewModel)
        {
            this.navigationStore = navigationStore;

            OpenToursInfoCommand = new NavigateCommand(this.navigationStore, createToursInfoViewModel);
            OpenLoginCommand = new NavigateCommand(this.navigationStore, createLoginViewModel);

            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            InvokePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
