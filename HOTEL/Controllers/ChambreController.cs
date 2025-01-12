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
                if (!chambre.ReservationId.HasValue)
                {
                    chambre.ReservationId = null; // if there is no reservation linked tho this room i make sure it's null 
                }
                _context.Chambres.Add(chambre);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
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
    }
}