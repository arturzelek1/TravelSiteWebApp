using TravelSiteWeb.Models;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Services;
//Only to combine 2 models 
namespace TravelSiteWeb.ViewModel
{
    public class ClientOrderViewModel
    {
        //Client needed fields
        public int ClientID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //Reservation needed fields
        public int ReservationID { get; set; }
        public string Title { get; set; }
        public float Cost { get; set; }
        public List<ReservationViewModel> Reservations { get; set; }
    }
}
