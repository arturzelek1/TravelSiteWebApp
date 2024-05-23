using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelSiteWeb.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }

        public string HotelName { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public decimal CostPerNight { get; set; }

        public string? Website { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }


        public virtual ICollection<TravelDestination> TravelDestinations { get; set; }

    }


}

