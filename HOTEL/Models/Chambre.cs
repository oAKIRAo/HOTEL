using HOTEL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

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
        public String ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation reservation { get; set; }
    
    }
}
