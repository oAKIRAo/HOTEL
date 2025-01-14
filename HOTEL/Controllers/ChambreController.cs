using Microsoft.AspNetCore.Mvc;
using HOTEL.Models;
using HOTEL.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;


namespace HOTEL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChambresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChambresController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var chambres = _context.Chambres.ToList();
            return View(chambres);
        }
        public IActionResult Create()
        {
            return View();
        }
        // POST: Chambres/Create
        [HttpPost]
        public IActionResult Create(Chambre chambre)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!chambre.ReservationId.HasValue)
                    {
                        chambre.ReservationId = null; // if there is no reservation linked tho this room i make sure it's null 
                    }
                    _context.Chambres.Add(chambre);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the room. Please try again.");
                }
            }

            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");

                }
            }
            return View(chambre);
        }
        public IActionResult Edit(Guid id)
        {
            var chambre = _context.Chambres.FirstOrDefault(c => c.Id == id);
            if (chambre == null)
            {
                return NotFound();
            }
            return View(chambre);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, Chambre updatedChambre)
        {
            if (ModelState.IsValid)
            {
                var chambre = _context.Chambres.FirstOrDefault(c => c.Id == id);
                if (chambre != null)
                {
                    chambre.Capacite = updatedChambre.Capacite;
                    chambre.Prix = updatedChambre.Prix;
                    chambre.EstReservee = updatedChambre.EstReservee;
                    chambre.Name = updatedChambre.Name;

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(updatedChambre);
        }
        public IActionResult Delete(Guid id)
        {
            var chambre = _context.Chambres.FirstOrDefault(c => c.Id == id);
            if (chambre == null)
            {
                return NotFound();
            }
            return View(chambre);
        }

        [HttpPost]
        public IActionResult Deleted(Guid id)
        {
            var chambre = _context.Chambres.FirstOrDefault(c => c.Id == id);
            if (chambre != null)
            {
                _context.Chambres.Remove(chambre);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


    }
}