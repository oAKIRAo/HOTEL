using Microsoft.AspNetCore.Mvc;
using HOTEL.Models;
using HOTEL.Data;
using System.Linq;

namespace HOTEL.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public IActionResult Index()
        {
            var reservations = _context.Reservations
                .Select(r => new
                {
                    r.Id,
                    r.userId,
                    r.DateReservation,
                    Chambre = _context.Chambres.FirstOrDefault(c => c.Id == r.ChambreId)
                })
                .ToList();

            return View(reservations);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            // Fetch available Chambres (not reserved)
            ViewBag.Chambres = _context.Chambres.Where(c => !c.EstReservee).ToList();
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,userId,ChambreId,DateReservation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                // Check if the selected Chambre exists and is not reserved
                var chambre = _context.Chambres.FirstOrDefault(c => c.Id == reservation.ChambreId && !c.EstReservee);
                if (chambre == null)
                {
                    ModelState.AddModelError("", "The selected room is not available.");
                }
                else
                {
                    // Check if the user already has a reservation for the same room
                    if (_context.Reservations.Any(r => r.userId == reservation.userId && r.ChambreId == reservation.ChambreId))
                    {
                        ModelState.AddModelError("", "You have already reserved this room.");
                    }
                    else
                    {
                        // Mark the room as reserved
                        chambre.EstReservee = true;
                        _context.Chambres.Update(chambre);

                        // Add the reservation to the database
                        _context.Reservations.Add(reservation);
                        _context.SaveChanges(); // Save changes to the database

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            // Repopulate the available Chambres list if validation fails
            ViewBag.Chambres = _context.Chambres.Where(c => !c.EstReservee).ToList();
            return View(reservation);
        }


    }
}
