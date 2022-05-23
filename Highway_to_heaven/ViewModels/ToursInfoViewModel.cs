using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Highway_to_heaven.ViewModels
{
    class ToursInfoViewModel : ViewModel
    {
        private ObservableCollection<PackageTour> tours;
        private readonly TravelService service;
        private string searchText;
        private PackageTour currentTour;
        private string mode = "ВСЕ";
        User user;

        public ObservableCollection<PackageTour> Tours
        {
            get => tours;
            set => Set(ref tours, value);
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (value == "")
                {

                        tours = new ObservableCollection<PackageTour>(service.GetToursCollection());
                   
                }
                else
                {
                   
                        tours = new ObservableCollection<PackageTour>(service.GetToursByName(value));
                   
                }
                Set(ref searchText, value);
            }
        }

        public string Mode
        {
            get => mode;
            set => Set(ref mode, value);
        }

        public ICommand OpenTourCommand { get; }
        public ICommand createTourInfoViewModelCommand { get; }
        public ICommand ChangeModeCommand { get; }

        public ToursInfoViewModel(NavigationStore navigationStore, TravelService service, PackageTour currentTour, Func<object, ViewModel> createTourInfoViewModel, User user)
        {
            this.service = service;
            this.currentTour = currentTour;
            tours = new ObservableCollection<PackageTour>(service.GetToursCollection());
            OpenTourCommand = new ExternalCommand(OnOpenTourCommandExecute);
            createTourInfoViewModelCommand = new NavigateCommand(navigationStore, createTourInfoViewModel);
            ChangeModeCommand = new ExternalCommand(OnChangeModeCommand);
            this.user = user;
        }

        private void OnOpenTourCommandExecute(object o)
        {
            createTourInfoViewModelCommand.Execute(o);
        }

        private void OnChangeModeCommand(object o)
        {
            if (mode.Equals("ВСЕ"))
            {
                Mode = "ПРОЙДЕНО";
                Tours = new ObservableCollection<PackageTour>(service.GetViewedTours(user.Login));
            }
            else
            {
                Mode = "ВСЕ";
                Tours = new ObservableCollection<PackageTour>(service.GetToursCollection());
            }
        }
    }
}
