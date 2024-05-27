using Microsoft.AspNetCore.Mvc;
using TravelSiteWeb.Models;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Data;
using TravelSiteWeb.Services;
using FluentValidation;
using FluentValidation.Results;

namespace RepositoryUsingEFinMVC.Controllers
{
    public class TravelDestinationController : Controller
    {
        //Create a variable to hold the instance of EmployeeRepository
        private ITravelDestinationRepository _destinationRepository;
        private readonly IPaginatedListService _paginatedListService;
        private readonly TravelContext _context;
        private readonly IValidator<TravelDestination> _validator;
        
        public TravelDestinationController(IValidator<TravelDestination> validator,IPaginatedListService paginatedListService, TravelContext context, ITravelDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
            _paginatedListService = paginatedListService;
            _context = context;
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
            ViewData["CountrySortParm"] = String.IsNullOrEmpty(sortOrder) ? "Country_desc" : "";
            ViewData["CitySortParm"] = sortOrder == "CityName_asc" ? "CityName_desc" : "CityName_asc";
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

            var destinations = _destinationRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                destinations = destinations.Where(d => d.City.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "CityName_desc":
                    destinations = destinations.OrderByDescending(d => d.DateStart);
                    break;
                case "CityName_asc":
                    destinations = destinations.OrderBy(d => d.DateStart);
                    break;
                case "Country_desc":
                    destinations = destinations.OrderByDescending(d => d.ToLocation);
                    break;
                default:
                    destinations = destinations.OrderBy(d => d.ToLocation);
                    break;
            }

            int pageSize = 3; // Number of items per page
            int pageIndex = pageNumber ?? 1; // Current page number

            var paginatedList = await _paginatedListService.CreateAsync(destinations, pageNumber ?? 1, pageSize);
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
        public async Task<ActionResult> Add(TravelDestination model)
        {
            //First Check whether the Model State is Valid or not
            ValidationResult result = await _validator.ValidateAsync(model);
            if (result.IsValid)
            {
                //If Valid, then call the Insert Method of EmployeeRepository to make the Entity State Added
                _destinationRepository.Insert(model);
                //To make the changes permanent in the database, call the Save method of EmployeeRepository
                _destinationRepository.Save();
                //Once the new data is saved into the database, redirect to the Index View
                return RedirectToAction("Index", "TravelDestination");
            }
            else
            {
                //If the Model State is invalid, then stay on the same view
                return View(model);
            }
        }
        //The following Action Method will open the Edit Employee view based on the EmployeeId
        [HttpGet]
        public ActionResult Edit(int TravelDestinationID)
        {
            //First, Fetch the Employee information by calling the GetById method of EmployeeRepository
            TravelDestination model = _destinationRepository.GetById(TravelDestinationID);
            //Then Pass the Employee data to the Edit view
            return View(model);
        }
        //The following Action Method will be called when you click on the Submit button on the Edit Employee view
        [HttpPost]
        public ActionResult Edit(TravelDestination model)
        {
            //First Check whether the Model State is Valid or not
            if (ModelState.IsValid)
            {
                //If Valid, then call the Update Method of EmployeeRepository  to make the Entity State Modified
                _destinationRepository.Update(model);
                //To make the changes permanent in the database, call the Save method of EmployeeRepository
                _destinationRepository.Save();
                //Once the updated data is saved into the database, redirect to the Index View
                return RedirectToAction("Index", "TravelDestination");
            }
            else
            {
                //If the Model State is invalid, then stay on the same view
                return View(model);
            }
        }
        //The following Action Method will open the Delete Employee view based on the EmployeeId
        [HttpGet]
        public ActionResult Delete(int TravelDestinationID)
        {
            //First, Fetch the Employee information by calling the GetById method of EmployeeRepository
            TravelDestination model = _destinationRepository.GetById(TravelDestinationID);
            //Then Pass the Employee data to the Delete view
            return View(model);
        }
        //The following Action Method will be called when you click on the Submit button on the Delete Employee view
        [HttpPost]
        public ActionResult DeleteDestination(int TravelDestinationID)
        {
            //Call the Delete Method of the EmployeeRepository to Make the Entity State Deleted 
            _destinationRepository.Delete(TravelDestinationID);
            //Then Call the Save Method to delete the entity from the database permanently
            _destinationRepository.Save();
            //And finally, redirect the user to the Index View
            return RedirectToAction("Index", "TravelDestination");
        }
    }
}
