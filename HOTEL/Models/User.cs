using HOTEL.Models;
using Microsoft.AspNetCore.Identity;

namespace HOTEL.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}