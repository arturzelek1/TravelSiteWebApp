using System.ComponentModel.DataAnnotations;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.ViewModel
{
    public class Offer
    {
        // Travel destination
        public int TravelDestinationID { get; set; }
        [DataType(DataType.Date)] // For only date
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string City { get; set; }

        // Hotel
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string HotelCity { get; set; }
        //public string Country { get; set; }
        public decimal CostPerNight { get; set; }
        public string? Website { get; set; }
        public string? Image {  get; set; }

        // Flight
        public int FlightID { get; set; }
        public string FlightNumber {  get; set; }
        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }
        public string AirportTo { get; set; }

        public string AirportFrom { get; set; }

        public decimal FlightCost { get; set; }

        //public string City { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
