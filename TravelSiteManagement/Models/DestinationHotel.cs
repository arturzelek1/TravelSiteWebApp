namespace TravelSiteWeb.Models
{
    public class DestinationHotel
    {
        public int TravelDestinationID { get; set; }
        public virtual TravelDestination TravelDestinations { get; set; }
        public int HotelID { get; set; }
        public virtual Hotel Hotels { get; set; }
    }
}
