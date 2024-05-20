using TravelSiteWeb.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
namespace RepositoryUsingEFinMVC.Repository
{
    public interface IReservationRepository
    {
        IQueryable<Reservation> GetAll();
        Reservation GetById(int ReservationID);
        void Insert(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int ReservationID);
        void Save();

    }
}