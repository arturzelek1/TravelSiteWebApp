using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TravelSiteWeb.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime TravelDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public ICollection<Reservation> Reservations { get; set; }

        public Client()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
