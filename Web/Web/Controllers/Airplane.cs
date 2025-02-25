using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Entities;
using Infrastructure.Data;


namespace Web.Controllers
{
    public class AirplaneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AirplaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Airplane/Index
        public async Task<IActionResult> Index()
        {
            var airplanes = await _context.Airplanes
                .Include(a => a.Airpark) // Включаем данные о аэропарке
                .ToListAsync();

            return View(airplanes);
        }


        // GET: Airplane/Create
        public IActionResult Create()
        {
            ViewBag.Airparks = new SelectList(_context.Airparks, "Id", "Name");
            return View();
        }

        // POST: Airplane/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Airplane airplane)
        {
            if (ModelState.IsValid)
            {
                _context.Airplanes.Add(airplane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Airparks = new SelectList(_context.Airparks, "Id", "Name");
            return View(airplane);
        }
    }
}