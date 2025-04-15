using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Entities;
using Infrastructure.Data;
using Web.Models;

[Authorize]
public class BookingController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Booking/Create?airplaneId=1
    public async Task<IActionResult> Create(int airplaneId)
    {
        var flight = await _context.Flights
            .Include(f => f.Airplane)
            .ThenInclude(a => a.Airpark)
            .FirstOrDefaultAsync(f => f.AirplaneId == airplaneId);

        if (flight == null)
        {
            return NotFound();
        }

        var userName = User.Identity?.Name;

        var model = new BookingViewModel
        {
            UserName = userName,
            Flight = flight
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(int flightId)
    {
        var userName = User.Identity?.Name;

        // Здесь можно добавить сохранение в таблицу Reservation, если такая есть

        TempData["Message"] = "Бронирование успешно подтверждено!";
        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}