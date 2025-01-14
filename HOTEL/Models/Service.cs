using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEL.Models
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name field is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Price field is required")]
        public float Price { get; set; }
        public Guid? ReservationId { get; set; } = null;
        [ForeignKey("ReservationId")]
        public Reservation? reservation { get; set; } = null;

    }
}
