using HOTEL.Data;
using HOTEL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HOTEL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Service service)
        {
            if (_context.Services.Any(s => s.Type == service.Type))
            {
                ModelState.AddModelError("", "The service name is already taken. Please choose a different name.");
                return View(service);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (!service.ReservationId.HasValue)
                    {
                        service.ReservationId = null; // if there is no reservation linked tho this room i make sure it's null 
                    }
                    _context.Add(service);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the service. Please try again.");
                }

            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");

                }
            }
            return View(service);
        }
        public IActionResult Edit(Guid id)
        {
            var services = _context.Services.FirstOrDefault(s => s.Id == id);
            if (services == null)
            {
                return NotFound();
            }
            return View();

        }
        [HttpPost]
        public IActionResult Edit(Guid id, Service Newservice)
        {
            if (ModelState.IsValid)
            {
                var service = _context.Services.FirstOrDefault(c => c.Id == id);
                if (service != null)
                {
                    service.Type = Newservice.Type;
                    service.Price = Newservice.Price;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(Newservice);
        }
        public IActionResult Delete(Guid id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);

        }
        public IActionResult Deleted(Guid id)
        {

            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _context.Remove(service);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }
    }
}