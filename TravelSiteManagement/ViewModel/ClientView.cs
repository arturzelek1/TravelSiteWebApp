using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class ClientViewModel
    {
        public int ClientID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}