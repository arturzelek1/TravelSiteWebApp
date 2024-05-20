using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace TravelSiteWeb.Models
{
    public class Reservation
    {

        public int ReservationID { get; set; }
        public string Title { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }

        public int ClientID { get; set; }
        public int TravelDestinationID { get; set; }

        public virtual Client? Clients { get; set; }

        public virtual TravelDestination? TravelDestinations { get; set; }
    }
}
