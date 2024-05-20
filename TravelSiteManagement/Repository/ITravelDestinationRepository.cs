using TravelSiteWeb.Models;
using System.Collections.Generic;
using System;
namespace RepositoryUsingEFinMVC.Repository
{
    public interface ITravelDestinationRepository
    {
        IQueryable<TravelDestination> GetAll();
        TravelDestination GetById(int TravelDestinationID);
        void Insert(TravelDestination travelDestination);
        void Update(TravelDestination travelDestination);
        void Delete(int TravelDestinationID);
        void Save();
    }
}