using HOTEL.Data;
using HOTEL.Models;
using HOTEL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOTEL.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReservationController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            bool chambresReserved = _context.Chambres.All(c => c.EstReservee);

            // Handle condition before creating the ViewModel
            var chambres = !chambresReserved ? _context.Chambres.ToList() : new List<Chambre>();

            var viewModel = new ChambreServiceViewModel
            {
                Chambres = chambres,
                Services = _context.Services.ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ReserveRoom(string chambreId, string userId)
        {
            try
            {
                var chambre = _context.Chambres.FirstOrDefault(c => c.Id.Equals(chambreId));

                if (chambre == null || chambre.EstReservee)
                {
                    return NotFound("Room not available or does not exist.");
                }

                var reservation = _context.Reservations
                                          .Include(r => r.chambres)
                                          .FirstOrDefault(r => r.userId.Equals(userId));

                if (reservation == null)
                {
                    reservation = new Reservation
                    {
                        Id = Guid.NewGuid(),
                        userId = userId,
                        DateReservation = DateTime.Now
                    };

                    _context.Reservations.Add(reservation);
                }

                reservation.chambres.Add(chambre);
                chambre.EstReservee = true;
                _context.SaveChanges();

                return RedirectToAction("details", new { reservationId = reservation.Id });
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or simply output to console for debugging)
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddService(string serviceId, string userId)
        {
            try
            {
                var service = _context.Services.FirstOrDefault(s => s.Id.Equals(serviceId));

                if (service == null)
                {
                    return NotFound("Service does not exist.");
                }

                var reservation = _context.Reservations
                                          .Include(r => r.services)
                                          .FirstOrDefault(r => r.userId.Equals(userId));

                if (reservation == null)
                {
                    reservation = new Reservation
                    {
                        Id = Guid.NewGuid(),
                        userId = userId,
                        DateReservation = DateTime.Now
                    };

                    _context.Reservations.Add(reservation);
                }

                reservation.services.Add(service);
                _context.SaveChanges();

                return RedirectToAction("details", new { reservationId = reservation.Id });
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or simply output to console for debugging)
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        public IActionResult details(string reservationId)
        {
            var reservation = _context.Reservations
                                      .Include(r => r.chambres)
                                      .Include(r => r.services)
                                      .FirstOrDefault(r => r.Id.Equals(reservationId));

            if (reservation == null)
            {
                return NotFound("Reservation does not exist.");
            }

            var viewModel = new ReservationDetailsViewModel
            {
                ReservationId = reservation.Id,
                UserId = reservation.userId,
                DateReservation = reservation.DateReservation,
                Chambres = reservation.chambres.ToList(),
                Services = reservation.services.ToList()
            };

            return View(viewModel);
        }

    }
}

