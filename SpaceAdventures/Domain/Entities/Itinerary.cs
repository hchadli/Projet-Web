
#nullable disable
using System;
using System.Collections.Generic;

namespace SpaceAdventures.Domain.Entities
{ 

    public partial class Itinerary
    {
        public Itinerary()
        {
            Flights = new HashSet<Flight>();
        }

        public int IdItinerary { get; set; }
        public int IdAirport1 { get; set; }
        public int IdAirport2 { get; set; }

        public virtual Airport IdAirport1Navigation { get; set; }
        public virtual Airport IdAirport2Navigation { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}