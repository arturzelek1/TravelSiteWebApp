namespace TravelSiteWeb.Models
{
    public class ClientReservation
    {
        public int ClientID { get; set; }
        public virtual Client Clients { get; set; }
        public int ReservationID { get; set; }
        public virtual Reservation Reservations { get; set; }
    }
}
