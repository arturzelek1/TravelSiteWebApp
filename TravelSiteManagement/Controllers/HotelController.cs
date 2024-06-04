using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Data;
using TravelSiteWeb.Models;
using TravelSiteWeb.Services;
using TravelSiteWeb.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace TravelSiteWeb.Controllers
{
    public class HotelController : Controller
    {
        private readonly TravelContext _context;
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<Hotel> _validator;

        public HotelController(TravelContext context, IHotelRepository hotelRepository, IValidator<Hotel> validator)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _validator = validator;
        }

        // GET: Hotel
        public async Task<IActionResult> Index()
        {
              return _context.Hotels != null ? 
                          View(await _context.Hotels.ToListAsync()) :
                          Problem("Entity set 'TravelContext.Hotels'  is null.");
        }

        // GET: Hotel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpGet]
        // GET: Hotel/Create
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Hotel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (result.IsValid)
            {
                _hotelRepository.Insert(model);
                _hotelRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(int HotelID)
        {
            Hotel model = _hotelRepository.GetByID(HotelID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel model, int id)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (id != model.HotelID)
            {
                return NotFound();
            }

            if (result.IsValid)
            {
                try
                {
                    _hotelRepository.Update(model);
                    _hotelRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(model.HotelID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(int HotelID)
        {
            Hotel model = _hotelRepository.GetByID(HotelID);
            return View(model);
        }

        // POST: Hotel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int HotelID)
        {
            _hotelRepository.Delete(HotelID);
            _hotelRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
          return (_context.Hotels?.Any(e => e.HotelID == id)).GetValueOrDefault();
        }
    }
}
