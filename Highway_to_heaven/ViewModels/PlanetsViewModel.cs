using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class PlanetsViewModel : ViewModel
    {
        private ObservableCollection<Planet> planets;
        private TravelService travelService;
        private string searchText;
        public ObservableCollection<Planet> Planets
        {
            get => planets;
        }
        public PlanetsViewModel(TravelService travelService)
        {
            this.travelService = travelService;
            planets = new ObservableCollection<Planet>(travelService.GetPlanets());
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (value == "")
                {
                    planets = new ObservableCollection<Planet>(travelService.GetPlanets());
                }
                else
                {
                    planets = new ObservableCollection<Planet>(travelService.GetPlanetsByName(value));
                }
                Set(ref searchText, value);
            }
        }
    }
}
