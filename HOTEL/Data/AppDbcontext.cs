using HOTEL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HOTEL.Data
{
    

        // Single context that includes both Identity and other models
        public class ApplicationDbContext : IdentityDbContext<Users>
        {
            public DbSet<Chambre> Chambres { get; set; }
            public DbSet<Reservation> Reservations { get; set; }

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Ensure you call the base method for Identity tables to be created
                base.OnModelCreating(modelBuilder);

                // Define the relationship between Chambre and Reservation
                modelBuilder.Entity<Reservation>()
                    .HasOne(r => r.Chambre)
                    .WithMany(c => c.Reservations)
                    .HasForeignKey(r => r.ChambreId);

                // Define the relationship between Reservation and User (Identity User)
                modelBuilder.Entity<Reservation>()
                    .HasOne(r => r.users) // A reservation is linked to one user
                    .WithMany(u => u.Reservations) // A user can have multiple reservations
                    .HasForeignKey(r => r.userId); // Foreign key for user
            }
        }
    
}
