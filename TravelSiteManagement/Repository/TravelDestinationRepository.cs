using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Data;
using static RepositoryUsingEFinMVC.Repository.TravelDestinationRepository;

namespace RepositoryUsingEFinMVC.Repository
{
    public class TravelDestinationRepository : ITravelDestinationRepository
    {
        //The following variable is going to hold the EmployeeDBContext instance
        private readonly TravelContext _context;
        //Initialzing the EmployeeDBContext instance
        public TravelDestinationRepository()
        {
            _context = new TravelContext();
        }
        //Initializing the EmployeeDBContext instance which it received as an argument
        public TravelDestinationRepository(TravelContext context)
        {
            _context = context;
        }
        //This method will return all the Employees from the Employee table
        public IQueryable<TravelDestination> GetAll()
        {
            return _context.TravelDestinations.AsQueryable();
        }
        //This method will return one Employee's information from the Employee table
        //based on the EmployeeID which it received as an argument
        public TravelDestination GetById(int TravelDestinationID)
        {
            return _context.TravelDestinations.Find(TravelDestinationID);
        }
        //This method will Insert one Employee object into the Employee table
        //It will receive the Employee object as an argument which needs to be inserted into the database
        public void Insert(TravelDestination travelDestination)
        {
            //The State of the Entity is going to be Added State
            _context.TravelDestinations.Add(travelDestination);
        }
        //This method is going to update the Employee data in the database
        //It will receive the Employee object as an argument
        public void Update(TravelDestination travelDestination)
        {
            //It will mark the Entity State as Modified
            _context.Entry(travelDestination).State = EntityState.Modified;
        }
        //This method is going to remove the Employee Information from the Database
        //It will receive the EmployeeID as an argument whose information needs to be removed from the database
        public void Delete(int TravelDestinationID)
        {
            //First, fetch the Employee details based on the EmployeeID id
            TravelDestination travelDestination = _context.TravelDestinations.Find(TravelDestinationID);
            //If the employee object is not null, then remove the employee
            if (travelDestination != null)
            {
                //This will mark the Entity State as Deleted
                _context.TravelDestinations.Remove(travelDestination);
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