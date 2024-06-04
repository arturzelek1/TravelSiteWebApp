using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelSiteManagement.Models;
using TravelSiteWeb.Data;
using TravelSiteWeb.Models;
using TravelSiteWeb.Services;
using TravelSiteWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace TravelSiteWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Propably wont work 
        //private readonly IClientOrderService _clientOrderService;
        //private readonly UserService _userService;
        private readonly MappingService _mappingService;

        public HomeController(ILogger<HomeController> logger,MappingService mappingService)
        {
            _logger = logger;
            _mappingService = mappingService;
        }

        public IActionResult ClientOrder([FromServices] TravelContext context)
        {
            var clientOrderViewModels = _mappingService.GetClientOrderViewModels(context);
            return View(clientOrderViewModels);
        }
        public async Task<IActionResult> Index([FromServices] TravelContext context)
        {

            var offers = _mappingService.GetOfferViewModel(context);
           
            return View(offers);
        }

        public IActionResult Destination()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
