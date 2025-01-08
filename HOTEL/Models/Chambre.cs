using HOTEL.Models;
using System.ComponentModel.DataAnnotations;

namespace HOTEL.Models
{
    public class Chambre
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Capacite { get; set; }

        [Required]
        public float Prix { get; set; }

        public bool EstReservee { get; set; } = false;

        // Navigation Property
        public ICollection<Reservation> Reservations { get; set; }
    }
}
