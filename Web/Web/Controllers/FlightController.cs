using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Entities;

namespace Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Flight/
        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights
                .Include(f => f.Airplane)
                .ThenInclude(a => a.Airpark)
                .ToListAsync();

            return View(flights);
        }

        // GET: /Flight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var flight = await _context.Flights
                .Include(f => f.Airplane)
                .ThenInclude(a => a.Airpark)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null) return NotFound();

            return View(flight);
        }

        // GET: /Flight/Create
        public async Task<IActionResult> Create()
        {
            // Получаем список всех самолетов
            var airplanes = await _context.Airplanes.ToListAsync();
            ViewData["AirplaneId"] = new SelectList(airplanes, "Id", "Name");

            return View();
        }

        // POST: /Flight/Create
        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Если модель не валидна, передаем список самолетов для выбора
            var airplanes = await _context.Airplanes.ToListAsync();
            ViewData["AirplaneId"] = new SelectList(airplanes, "Id", "Name", flight.AirplaneId);

            return View(flight);
        }

        // GET: /Flight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();

            // Получаем список всех самолетов для редактирования
            var airplanes = await _context.Airplanes.ToListAsync();
            ViewData["AirplaneId"] = new SelectList(airplanes, "Id", "Name", flight.AirplaneId);

            return View(flight);
        }

        // POST: /Flight/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Flight flight)
        {
            if (id != flight.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Если модель не валидна, передаем список самолетов для выбора
            var airplanes = await _context.Airplanes.ToListAsync();
            ViewData["AirplaneId"] = new SelectList(airplanes, "Id", "Name", flight.AirplaneId);

            return View(flight);
        }

        // GET: /Flight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var flight = await _context.Flights
                .Include(f => f.Airplane)
                .ThenInclude(a => a.Airpark)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null) return NotFound();

            return View(flight);
        }

        // POST: /Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(f => f.Id == id);
        }
    }
}