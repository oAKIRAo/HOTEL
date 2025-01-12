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


                modelBuilder.Entity<Chambre>()
                    .Property(c => c.Id)
                    .ValueGeneratedOnAdd(); // Optional, only for auto GUID generation

                // Ensure you call the base method for Identity tables to be created
                base.OnModelCreating(modelBuilder);

                // Define the relationship between Chambre and Reservation (Chambre has one Reservation, Reservation can have many Chambres)
                modelBuilder.Entity<Chambre>()
                    .HasOne(c => c.reservation) // A Chambre has one Reservation
                    .WithMany(r => r.chambres)  // A Reservation can have multiple Chambres
                    .HasForeignKey(c => c.ReservationId) // ForeignKey on ReservationId in Chambre
                    .IsRequired(false);

            // Define the relationship between Reservation and User (Identity User)
            modelBuilder.Entity<Reservation>()
                    .HasOne(r => r.users) // A reservation is linked to one user
                    .WithMany(u => u.Reservations) // A user can have multiple reservations
                    .HasForeignKey(r => r.userId); // Foreign key on userId in Reservation

                // Ensure `ReservationId` in Chambre is optional (nullable)
                modelBuilder.Entity<Chambre>()
                    .Property(c => c.ReservationId)
                    .IsRequired(false); // Optional column in the database
            }
        }
    }
