namespace TravelSiteWeb.Models
{
    public class DestinationFlight
    {
        public int TravelDestinationID { get; set; }
        public virtual TravelDestination TravelDestinations { get; set; }
        public int FlightID { get; set; }
        public virtual Flight Flights { get; set; }
    }
}
