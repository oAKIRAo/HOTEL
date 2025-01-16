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
    }

}

