using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelSiteWeb.Models;
using TravelSiteWeb.Data;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Services;
using TravelSiteWeb.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization; //Functionality only allowed to logged accounts

namespace RepositoryUsingEFinMVC.Controllers
{
    
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        // private readonly IPaginatedList<Client> _paginatedListService;
        private readonly IPaginatedListService _paginatedListService;
        private readonly MappingService _mappingService;
        private readonly IValidator<Client> _validator;

        public ClientController(IClientRepository clientRepository,

IPaginatedListService paginatedListService, MappingService mappingService, IValidator<Client> validator
            // IPaginatedList<Client> paginatedListService
            )
        {
            _clientRepository = clientRepository;
            _paginatedListService = paginatedListService;
            _mappingService = mappingService;
            _validator = validator;

        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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

        //Using ClientViewModel
        [HttpGet]
        public ActionResult ClientView([FromServices] TravelContext context)
        {
            var clientsViewModels = _mappingService.GetClientView(context);
            return View(clientsViewModels);
        }

        [HttpGet]
        [Authorize] //Authorizations for logged users
        //[ValidateAntiForgeryToken] //Only logged in users
        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
       //Zapytać czy jak już get został zablokowany to post jest możliwy
        public async Task<ActionResult> AddClient(Client model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (result.IsValid)
            {
                _clientRepository.Insert(model);
                _clientRepository.Save();
                return RedirectToAction("Index", "Client");
            }
            else
            {
                // Jeśli walidacja nie powiedzie się, zwróć widok z błędami walidacji
                return View(model);
            }
        }

        [HttpGet]
        [Authorize] //Authorizations for logged users
        //[ValidateAntiForgeryToken] //Only logged in users
        public ActionResult EditClient(int ClientID)
        {
            Client model = _clientRepository.GetById(ClientID);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditClient(Client model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (result.IsValid)
            {
                _clientRepository.Update(model);
                _clientRepository.Save();
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize] //Authorizations for logged users
        //[ValidateAntiForgeryToken] //Only logged in users
        public ActionResult DeleteClient(int ClientID)
        {
            Client model = _clientRepository.GetById(ClientID);
            return View(model);
        }

        [HttpPost]
        [Authorize] //Authorizations for logged users
        //[ValidateAntiForgeryToken] //Only logged in users
        public ActionResult Delete(int ClientID)
        {
            _clientRepository.Delete(ClientID);
            _clientRepository.Save();
            return RedirectToAction("Index", "Client");
        }
    }
}