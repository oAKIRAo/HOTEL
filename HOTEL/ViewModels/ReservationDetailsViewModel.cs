using HOTEL.Models;

namespace HOTEL.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public Guid ReservationId { get; set; }
        public string UserId { get; set; }
        public DateTime DateReservation { get; set; }
        public List<Chambre> Chambres { get; set; }
        public List<Service> Services { get; set; }
    }
}