using lab6_7.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_7.Services
{
    class TravelService
    {
        private readonly HIGHWAY_TO_HEAVENContext context;

        public TravelService(HIGHWAY_TO_HEAVENContext context)
        {
            this.context = context;
        }

        public IEnumerable<Planet> GetPlanetsCollection()
        {
            return this.context.Planets.AsEnumerable();
        }
    }
}
