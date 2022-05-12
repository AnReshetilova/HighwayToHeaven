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
        public  ICommand createTourInfoViewModelCommand { get; }

        public ObservableCollection<PackageTour> Tours
        {
            get => tours;
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

        public ICommand OpenTourCommand { get; }

        public ToursInfoViewModel(NavigationStore navigationStore, TravelService service, PackageTour currentTour, Func<object, ViewModel> createTourInfoViewModel)
        {
            this.service = service;
            this.currentTour = currentTour;
            tours = new ObservableCollection<PackageTour>(service.GetToursCollection());
            OpenTourCommand = new ExternalCommand(OnOpenTourCommandExecute);
            createTourInfoViewModelCommand = new NavigateCommand(navigationStore, createTourInfoViewModel);
        }

        private void OnOpenTourCommandExecute(object o)
        {
            createTourInfoViewModelCommand.Execute(o);
        }
    }
}
