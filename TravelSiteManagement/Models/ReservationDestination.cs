namespace TravelSiteWeb.Models
{
    public class ReservationDestination
    {
        public int ReservationID { get; set; }
        public int TravelDestinationID { get; set; }
        public virtual TravelDestination TravelDestinations { get; set; }
    }

}