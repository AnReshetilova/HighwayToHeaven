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

        public IEnumerable<Planet> GetPlanetsByName(string name)
        {
            return this.context.Planets.Where(t => t.Name.Contains(name)).AsEnumerable();
        }

        public void AddNewTour(PackageTour packageTour)
        {
            this.context.PackageTours.Add(packageTour);
            context.SaveChanges();
        }

        public bool AddNewPlanet(Planet planet)
        {
            if (GetPlanetByName(planet.Name) != null)
            {
                return false;
            }

            this.context.Planets.Add(planet);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Planet> GetPlanets()
        {
            return this.context.Planets.AsEnumerable();
        }

        public IEnumerable<Question> GetQuestionsByTourId(int id)
        {
            return context.Questions.Where(t => t.IdTour == id).AsEnumerable();
        }

        public IEnumerable<Answer> GetAnswersBuQuestionId(int id)
        {
            return context.Answers.Where(t => t.IdQuestion == id).AsEnumerable();
        }

        public void AddNewTravel(Travel travel)
        {
            context.Travels.Add(travel);
            context.SaveChanges();
        }

        public Travel GetTravelByUserTourId(string userId, int tourId)
        {
            return context.Travels.FirstOrDefault(t => t.IdTraveler.Equals(userId) && t.IdTour == tourId);
        }

        public IEnumerable<Travel> GetTravelByTourId(int tourId)
        {
            return context.Travels.Where(t => t.IdTour == tourId).AsEnumerable();
        }

        public Planet GetPlanetByName(string name)
        {
            return context.Planets.FirstOrDefault(t => t.Name == name);
        }
    }
}
