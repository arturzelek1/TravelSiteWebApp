using TravelSiteWeb.Services;
using TravelSiteWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Models;
using Microsoft.Data.SqlClient;
using TravelSiteWeb.Repository;

namespace TravelSiteWeb.Controllers
{
    public class AdminDashboard : Controller
    {
        private readonly TravelContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ITravelDestinationRepository _destinationRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IPaginatedListService _paginatedListService;
        private readonly MappingService _mappingService;
        private readonly IValidator<Client> _validator;
        

        public AdminDashboard(IHotelRepository hotelRepository,IFlightRepository flightRepository,ITravelDestinationRepository travelDestinationRepository,IReservationRepository reservationRepository,IClientRepository clientRepository,IPaginatedListService paginatedListService,MappingService mappingService,IValidator<Client> validator,TravelContext context)
        {
            _context = context;
            _clientRepository = clientRepository;
            _paginatedListService = paginatedListService;
            _mappingService = mappingService;
            _validator = validator;
            _reservationRepository = reservationRepository;
            _destinationRepository = travelDestinationRepository;
            _flightRepository = flightRepository;
            _hotelRepository = hotelRepository;
        }
          
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ClientTable(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "lastname_asc" ? "lastname_desc" : "lastname_asc";
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

            var clients = _clientRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.LastName.Contains(searchString)
                                        || c.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "lastname_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                case "lastname_asc":
                    clients = clients.OrderBy(s => s.LastName);
                    break;
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.FirstName);
                    break;
            }

            int pageSize = 3; // Number of items per page
            int pageIndex = pageNumber ?? 1; // Current page number

            var paginatedList = await _paginatedListService.CreateAsync(clients, pageIndex, pageSize);
            return View(paginatedList);
        }
        public async Task<IActionResult> ReservationTable(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
        public async Task<IActionResult> DestinationTable(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
        public async Task<IActionResult> FlightTable()
        {
            return _context.Flights != null ?
                        View(await _context.Flights.ToListAsync()) :
                        Problem("Entity set 'TravelContext.Flights'  is null.");
        }
        public async Task<IActionResult> HotelTable()
        {
            return _context.Hotels != null ?
                        View(await _context.Hotels.ToListAsync()) :
                        Problem("Entity set 'TravelContext.Hotels'  is null.");
        }

        public IActionResult Blank()
        {
            return View();
        }
    }
}
