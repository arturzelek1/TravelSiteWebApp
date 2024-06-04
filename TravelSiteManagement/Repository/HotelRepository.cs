using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelSiteWeb.Data;

namespace TravelSiteWeb.Repository
{
    public class HotelRepository: IHotelRepository
    {
        private readonly TravelContext _context;
        public HotelRepository()
        {
            _context = new TravelContext();
        }
        public HotelRepository(TravelContext context)
        {
            _context = context;
        }
        public IQueryable<Hotel> GetAll()
        {
            return _context.Hotels.AsQueryable();
        }
        public Hotel GetByID(int HotelID)
        {
            return _context.Hotels.Find(HotelID);
        }
        public void Insert(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
        }
        public void Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
        }
        public void Delete(int HotelID)
        {
            Hotel hotel = _context.Hotels.Find(HotelID);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
