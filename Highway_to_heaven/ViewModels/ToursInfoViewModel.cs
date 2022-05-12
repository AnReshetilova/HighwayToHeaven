using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway_to_heaven.ViewModels
{
    class ToursInfoViewModel : ViewModel
    {
        private ObservableCollection<PackageTour> tours;
        private readonly TravelService service;
        private string searchText;
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

        public ToursInfoViewModel(TravelService service)
        {
            this.service = service;
            tours = new ObservableCollection<PackageTour>(service.GetToursCollection());
        }
    }
}
