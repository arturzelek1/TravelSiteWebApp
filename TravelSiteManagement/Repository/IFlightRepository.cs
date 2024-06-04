using TravelSiteWeb.Models;

namespace TravelSiteWeb.Repository
{
    public interface IFlightRepository
    {
        IQueryable<Flight> GetAll();
        Flight GetById(int FlightID);
        void Insert(Flight flight);
        void Update(Flight flight);
        void Delete(int FlightID);
        void Save();
    }
}
