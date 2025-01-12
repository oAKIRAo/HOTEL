using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HOTEL.Models;

namespace HOTEL.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public DateTime DateReservation { get; set; }

        // Navigation Property
        [ForeignKey("userId")]
        public Users users { get; set; }
        public ICollection<Chambre> chambres { get; set; }

    }
}
