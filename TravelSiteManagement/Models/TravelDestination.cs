using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TravelSiteWeb.Models
{
    public enum Country
    {
        Italy, Spain, Turkey, Greece
    }

    public class TravelDestination
    {
        [Key]
        public int TravelDestinationID { get; set; }
        public Country? Country { get; set; }
        public float Cost { get; set; }
        public string City { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public TravelDestination()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
