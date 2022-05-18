using Highway_to_heaven.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway_to_heaven.Services
{
    class TravelService
    {
        private readonly HIGHWAY_TO_HEAVENContext context;

        public TravelService(HIGHWAY_TO_HEAVENContext context)
        {
            this.context = context;
        }

        public IEnumerable<PackageTour> GetToursCollection()
        {
            return this.context.PackageTours.AsEnumerable();
        }

        public IEnumerable<PackageTour> GetToursByName(string name)
        {
            return this.context.PackageTours.Where(t => t.PlanetName.Contains(name)).AsEnumerable();
        }

        public void AddNewTour(PackageTour packageTour)
        {
            this.context.PackageTours.Add(packageTour);
            context.SaveChanges();
        }

        public void AddNewPlanet(Planet planet)
        {
            this.context.Planets.Add(planet);
            context.SaveChanges();
        }

        public IEnumerable<Planet> GetPlanets()
        {
            return this.context.Planets.AsEnumerable();
        }
    }
}
