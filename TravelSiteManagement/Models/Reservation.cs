using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelSiteWeb.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public string? Description { get; set; }
        public int ClientID { get; set; }
        public int TravelDestinationID { get; set; }
        
        public ICollection<Client> Clients { get; set; }
        public ICollection<TravelDestination> TravelDestinations { get; set; }
        

    }
}
