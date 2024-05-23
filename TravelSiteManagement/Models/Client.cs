using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelSiteWeb.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

    }
}
