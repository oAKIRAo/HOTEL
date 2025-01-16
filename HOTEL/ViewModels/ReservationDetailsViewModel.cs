using HOTEL.Models;
using Microsoft.AspNetCore.Components.Web;

namespace HOTEL.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public List<Chambre> Chambres { get; set; }
        public List<Service> Services { get; set; }
        public float price { get; set; }
    }
}