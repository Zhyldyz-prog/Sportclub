using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data; // Пространство имен для ApplicationDbContext
using Web.Entities; // Пространство имен для модели Airpark

namespace Web.Controllers
{
    public class AirparkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AirparkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для отображения списка Airparks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Airparks.ToListAsync());
        }

        // Метод для отображения формы создания
        public IActionResult Create()
        {
            return View();
        }

        // Метод для обработки POST-запроса создания
        [HttpPost]
        public async Task<IActionResult> Create(Airpark airpark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airpark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airpark);
        }

        // Метод для отображения деталей объекта
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airpark = await _context.Airparks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (airpark == null)
            {
                return NotFound();
            }

            return View(airpark);
        }

        // Метод для отображения формы редактирования
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airpark = await _context.Airparks.FindAsync(id);

            if (airpark == null)
            {
                return NotFound();
            }

            return View(airpark);
        }

        // Метод для обработки POST-запроса редактирования
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Airpark airpark)
        {
            if (id != airpark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airpark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirparkExists(airpark.Id))
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

            return View(airpark);
        }

        // Метод для отображения формы удаления
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airpark = await _context.Airparks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (airpark == null)
            {
                return NotFound();
            }

            return View(airpark);
        }

        // Метод для обработки POST-запроса удаления
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airpark = await _context.Airparks.FindAsync(id);

            if (airpark != null)
            {
                _context.Airparks.Remove(airpark);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Метод для проверки существования объекта Airpark
        private bool AirparkExists(int id)
        {
            return _context.Airparks.Any(e => e.Id == id);
        }
    }
}

