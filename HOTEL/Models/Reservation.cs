using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HOTEL.Models;

namespace HOTEL.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string userId { get; set; }
        [Required]
        public DateTime DateReservation { get; set; }

        // Navigation Property
        [ForeignKey("userId")]
        public Users users { get; set; }
        public Chambre chambre { get; set; }
        public ICollection<Service> services { get; set; }


    }
}
