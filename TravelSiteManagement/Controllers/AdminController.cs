using TravelSiteWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TravelSiteWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly TravelContext _context;

        public AdminController(TravelContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UserManagement()
        {
            return View();
        }

        public async Task<IActionResult> ContentManagement()
        {
            return View();
        }

        public async Task<IActionResult> Statistics()
        {
            return View();
        }

        public async Task<IActionResult> SystemSettings()
        {
            return View();
        }

        public async Task<IActionResult> Support()
        {
            return View();
        }

        public async Task<IActionResult> Payment()
        {
            return View();
        }
    }
}
