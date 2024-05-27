using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelSiteWeb.Models
{ 

    public class TravelDestination
    {
        [Key]
        public int TravelDestinationID { get; set; }
        [DataType(DataType.Date)] // For only date
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)] 
        public DateTime DateEnd { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string City { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Flight> Flights { get; set; }
        public ICollection<Hotel> Hotels { get; set; }

       
    }
}
