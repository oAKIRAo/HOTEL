using HOTEL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace HOTEL.Models
{
    public class Chambre
    {
        public Guid Id { get; set; }
        [Required]
        public int Capacite { get; set; }

        [Required]
        public float Prix { get; set; }

        [Required]
        public bool EstReservee { get; set; } = false;
        [Required]
        public String Name { get; set; }
        public int? ReservationId { get; set; } = null;

        [ForeignKey("ReservationId")]
        public Reservation? reservation { get; set; }= null;


    }

}
