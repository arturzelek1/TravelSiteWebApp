using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Data;
using static RepositoryUsingEFinMVC.Repository.ReservationRepository;
using TravelSiteWeb;
using TravelSiteWeb.Services;
using FluentValidation;
using FluentValidation.Results;
namespace RepositoryUsingEFinMVC.Controllers
{
    public class ReservationController : Controller
    {
        //Create a variable to hold the instance of EmployeeRepository
        private IReservationRepository _reservationRepository;
        private readonly IPaginatedListService _paginatedListService;
        private readonly IValidator<Reservation> _validator;
        //private readonly IPaginatedList<Reservation> _paginatedListService;

        //Initializing the _employeeRepository through parameterless constructor
        public ReservationController(IReservationRepository reservationRepository, IPaginatedListService paginatedListService, IValidator<Reservation> validator)
        {
            _reservationRepository = new ReservationRepository(new TravelContext());
            _paginatedListService = paginatedListService;
            _validator = validator;
        }
        //If you want to Initialize _employeeRepository using Dependency Injection Container,
        //Then include the following Parameterized Constructor
        //public EmployeeController(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}
        //The following Action Method is used to return all the Employees
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "TitleName_asc" ? "TitleName_desc" : "TitleName_asc";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var reservations = _reservationRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(r => r.Title.Contains(searchString)
                                       || r.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TitleName_desc":
                    reservations = reservations.OrderByDescending(s => s.Title);
                    break;
                case "TitleName_asc":
                    reservations = reservations.OrderBy(s => s.Title);
                    break;
                case "ID_desc":
                    reservations = reservations.OrderByDescending(s => s.ReservationID);
                    break;
                default:
                    reservations = reservations.OrderBy(s => s.ReservationID);
                    break;
            }

            int pageSize = 3; // Number of items per page
            int pageIndex = pageNumber ?? 1; // Current page number

            var paginatedList = await _paginatedListService.CreateAsync(reservations, pageNumber ?? 1, pageSize);
            return View(paginatedList);
        }
        //The following Action Method will open the Add Employee view
        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }
        //The following Action Method will be called when you click on the Submit button on the Add Employee view
        [HttpPost]
        public async Task<ActionResult> Add(Reservation model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (result.IsValid)
            {
                _reservationRepository.Insert(model);
                _reservationRepository.Save();

                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                // Jeśli walidacja nie powiodła się, przekaż błędy do widoku
                ModelState.AddModelError("", result.Errors.ToString());
                return View(model);
            }
        }
        //The following Action Method will open the Edit Employee view based on the EmployeeId
        [HttpGet]
        public ActionResult Edit(int ReservationID)
        {
            //First, Fetch the Employee information by calling the GetById method of EmployeeRepository
            Reservation model = _reservationRepository.GetById(ReservationID);
            //Then Pass the Employee data to the Edit view
            return View(model);
        }
        //The following Action Method will be called when you click on the Submit button on the Edit Employee view
        [HttpPost]
        public async Task<ActionResult> Edit(Reservation model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            //First Check whether the Model State is Valid or not
            if (result.IsValid)
            {
                //If Valid, then call the Update Method of EmployeeRepository  to make the Entity State Modified
                _reservationRepository.Update(model);

                //To make the changes permanent in the database, call the Save method of EmployeeRepository
                _reservationRepository.Save();
                //Once the updated data is saved into the database, redirect to the Index View
                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                //If the Model State is invalid, then stay on the same view
                return View(model);
            }
        }
        //The following Action Method will open the Delete Employee view based on the EmployeeId
        [HttpGet]
        public ActionResult Delete(int ReservationID)
        {
            //First, Fetch the Employee information by calling the GetById method of EmployeeRepository
            Reservation model = _reservationRepository.GetById(ReservationID);
            //Then Pass the Employee data to the Delete view
            return View(model);
        }
        //The following Action Method will be called when you click on the Submit button on the Delete Employee view
        [HttpPost]
        public ActionResult DeleteReservation(int ReservationID)
        {
            //Call the Delete Method of the EmployeeRepository to Make the Entity State Deleted 
            _reservationRepository.Delete(ReservationID);
            //Then Call the Save Method to delete the entity from the database permanently
            _reservationRepository.Save();
            //And finally, redirect the user to the Index View
            return RedirectToAction("Index", "Reservation");
        }
    }
}