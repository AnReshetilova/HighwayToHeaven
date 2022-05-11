using lab6_7.Models;
using lab6_7.Services;
using lab6_7.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_7.ViewModels
{
    class ToursInfoViewModel : ViewModel
    {
        private ObservableCollection<Planet> planets;

        public ObservableCollection<Planet> Planets
        {
            get => planets;
        }

        public ToursInfoViewModel(TravelService service)
        {
            planets = new ObservableCollection<Planet>(service.GetPlanetsCollection());
        }
    }
}
