using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelSiteManagement.Models;
using TravelSiteWeb.Data;
using TravelSiteWeb.Models;
using TravelSiteWeb.Services;
using TravelSiteWeb.ViewModel;

namespace TravelSiteWeb.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        //Propably wont work 
        //private readonly IClientOrderService _clientOrderService;

        private readonly MappingService _mappingService;

        public HomeController(ILogger<HomeController> logger, MappingService mappingService)
        {
            _logger = logger;
            _mappingService = mappingService;
        }

        private IEnumerable<TravelDetail> travel = new
        List<TravelDetail>
        {
            new TravelDetail
            {
                Id = 1,
                Name = "Cinque Terre",
                Country = "Italy",
                Description = "City break in cinque terre",
                Cost = 800

            },
            new TravelDetail
            {
                Id= 2,
                Name = "Barcelona",
                Country = "Spain",
                Description = "Flamenco dance",
                Cost = 1400
            },
            new TravelDetail
            {
                Id = 3,
                Name = "Cappadocia",
                Country = "Turkey",
                Description = "Baloons in Turkey",
                Cost = 900
            },
            new TravelDetail
            {
                Id= 4,
                Name = "Santorini",
                Country = "Greece",
                Description = "Beautiful Greece sights",
                Cost = 1200

            }

        };


        public IActionResult ClientOrder([FromServices] TravelContext context)
        {
            var clientOrderViewModels = _mappingService.GetClientOrderViewModels(context);
            return View(clientOrderViewModels);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Destination()
        {
            return View(travel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
