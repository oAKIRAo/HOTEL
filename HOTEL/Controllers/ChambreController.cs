using Microsoft.AspNetCore.Mvc;
using HOTEL.Models;
using HOTEL.Data;
using System.Linq;

namespace HOTEL.Controllers
{
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
                _context.Chambres.Add(chambre);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(chambre);
        }
    }
}