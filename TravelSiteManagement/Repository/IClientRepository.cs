using TravelSiteWeb.Models;
using System.Collections.Generic;
using System;
namespace RepositoryUsingEFinMVC.Repository
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        Client GetById(int ClientID);
        void Insert(Client client);
        void Update(Client client);
        void Delete(int ClientID);
        void Save();
    }
}