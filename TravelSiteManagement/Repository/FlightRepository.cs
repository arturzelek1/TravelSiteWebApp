using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using TravelSiteWeb.Data;

namespace TravelSiteWeb.Repository
{
    public class FlightRepository: IFlightRepository
    {
        private readonly TravelContext _context;
        public FlightRepository()
        {
            _context = new TravelContext();
        }
        public FlightRepository(TravelContext context)
        {
            _context = context;
        }
        public IQueryable<Flight> GetAll()
        {
            return _context.Flights.AsQueryable();
        }
        public Flight GetById(int FlightID)
        {
            return _context.Flights.Find(FlightID);
        }
        public void Insert(Flight flight)
        {
            _context.Flights.Add(flight);
        }
        public void Update(Flight flight)
        {
            _context.Entry(flight).State = EntityState.Modified;
        }
        public void Delete(int FlightID)
        {
            Flight flight = _context.Flights.Find(FlightID);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
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
