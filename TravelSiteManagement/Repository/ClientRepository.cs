using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Data;
using static RepositoryUsingEFinMVC.Repository.ClientRepository;

namespace RepositoryUsingEFinMVC.Repository
{
    public class ClientRepository : IClientRepository
    {
        //The following variable is going to hold the EmployeeDBContext instance
        private readonly TravelContext _context;
        //Initialzing the EmployeeDBContext instance
        public ClientRepository()
        {
            _context = new TravelContext();
        }
        //Initializing the EmployeeDBContext instance which it received as an argument
        public ClientRepository(TravelContext context)
        {
            _context = context;
        }
        //This method will return all the Employees from the Employee table
        public IQueryable<Client> GetAll()
        {
            return _context.Clients.AsQueryable();
        }
        //This method will return one Employee's information from the Employee table
        //based on the EmployeeID which it received as an argument
        public Client GetById(int ClientID)
        {
            return _context.Clients.Find(ClientID);
        }
        //This method will Insert one Employee object into the Employee table
        //It will receive the Employee object as an argument which needs to be inserted into the database
        public void Insert(Client client)
        {
            //The State of the Entity is going to be Added State
            _context.Clients.Add(client);
        }
        //This method is going to update the Employee data in the database
        //It will receive the Employee object as an argument
        public void Update(Client client)
        {
            //It will mark the Entity State as Modified
            _context.Entry(client).State = EntityState.Modified;
        }
        //This method is going to remove the Employee Information from the Database
        //It will receive the EmployeeID as an argument whose information needs to be removed from the database
        public void Delete(int ClientID)
        {
            //First, fetch the Employee details based on the EmployeeID id
            Client client = _context.Clients.Find(ClientID);
            //If the employee object is not null, then remove the employee
            if (client != null)
            {
                //This will mark the Entity State as Deleted
                _context.Clients.Remove(client);
            }

        }
        //This method will make the changes permanent in the database
        //That means once we call Insert, Update, and Delete Methods, then we need to call
        //the Save method to make the changes permanent in the database
        public void Save()
        {
            //Based on the Entity State, it will generate the corresponding SQL Statement and
            //Execute the SQL Statement in the database
            //For Added Entity State: It will generate INSERT SQL Statement
            //For Modified Entity State: It will generate UPDATE SQL Statement
            //For Deleted Entity State: It will generate DELETE SQL Statement
            _context.SaveChanges();
        }
        private bool disposed = false;
        //As a context object is a heavy object or you can say time-consuming object
        //So, once the operations are done we need to dispose of the same using Dispose method
        //The EmployeeDBContext class inherited from DbContext class and the DbContext class
        //is Inherited from the IDisposable interface
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