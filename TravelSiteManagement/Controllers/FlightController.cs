using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Data;
using TravelSiteWeb.Models;
using TravelSiteWeb.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace TravelSiteWeb.Controllers
{
    public class FlightController : Controller
    {
        private readonly TravelContext _context;
        private readonly IFlightRepository _flightRepository;
        private readonly IValidator<Flight> _validator;

        public FlightController(TravelContext context, IFlightRepository flightRepository, IValidator<Flight> validator)
        {
            _context = context;
            _flightRepository = flightRepository;
            _validator = validator;
        }

        // GET: Flight
        public async Task<IActionResult> Index()
        {
              return _context.Flights != null ? 
                          View(await _context.Flights.ToListAsync()) :
                          Problem("Entity set 'TravelContext.Flights'  is null.");
        }

        // GET: Flight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpGet]
        // GET: Flight/Create
        public IActionResult Add()
        {
            return View();
        }

        // POST: Flight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Flight model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (result.IsValid)
            {
                _flightRepository.Insert(model);
                _flightRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]// GET: Flight/Edit/5
        public async Task<IActionResult> Edit(int FlightID)
        {
           Flight model = _flightRepository.GetById(FlightID);
           return View(model);
        }

        // POST: Flight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Flight model, int id)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (id != model.FlightID)
            {
                return NotFound();
            }

            if (result.IsValid)
            {
                try
                {
                    _flightRepository.Update(model);
                   _flightRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(model.FlightID))
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
        [HttpGet]
        // GET: Flight/Delete/5
        public async Task<IActionResult> Delete(int FlightID)
        {
            Flight model = _flightRepository.GetById(FlightID);
            return View(model);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int FlightID)
        {
           _flightRepository.Delete(FlightID);
           _flightRepository.Save();
           return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
          return (_context.Flights?.Any(e => e.FlightID == id)).GetValueOrDefault();
        }
    }
}
