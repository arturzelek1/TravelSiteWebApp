namespace TravelSiteWeb.Models
{
    public class TravelFlight
    {
        public int FlightID { get; set; }
        public virtual Flight Flights { get; set; }
        public int TravelDestinationID { get; set; }
        public virtual TravelDestination TravelDestinations { get; set; }
    }
}
