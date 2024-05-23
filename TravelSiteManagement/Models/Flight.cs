using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelSiteWeb.Models
{
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }
        public int TravelDestinationID { get; set; }
        public string FlightNumber { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public decimal FlightCost { get; set; }

        public string City { get; set; }

        public ICollection<TravelDestination> TravelDestinations { get; set; }

    }
}
