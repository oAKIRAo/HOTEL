using HOTEL.Models;

namespace HOTEL.ViewModels
{
    public class ChambreServiceViewModel
    {
        public IEnumerable<Chambre> Chambres { get; set; }
        public IEnumerable<Service> Services { get; set; }

    }
}
