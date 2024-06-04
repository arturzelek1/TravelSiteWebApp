using TravelSiteWeb.Models;

namespace TravelSiteWeb.Repository
{
    public interface IHotelRepository
    {
        IQueryable<Hotel> GetAll();
        Hotel GetByID(int HotelID);
        void Insert(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(int HotelID);
        void Save();
    }
}
