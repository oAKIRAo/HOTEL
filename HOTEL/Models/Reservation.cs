using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HOTEL.Models;

namespace HOTEL.Models
{
    public class Reservation
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string ChambreId { get; set; }

        [Required]
        public DateTime DateReservation { get; set; }

        // Navigation Property
        [ForeignKey("ChambreId")]
        public Chambre Chambre { get; set; }
        [ForeignKey("userId")]
        public Users users { get; set; }
    }
}
