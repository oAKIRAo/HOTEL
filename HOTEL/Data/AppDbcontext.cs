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
            public DbSet<Service> Services { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Reservation and Chambre
            modelBuilder.Entity<Chambre>()
                  .HasOne(c => c.reservation) // One Chambre has one Reservation
                  .WithOne(r => r.chambre)    // One Reservation has one Chambre
                  .HasForeignKey<Chambre>(c => c.ReservationId)
                   .IsRequired(false); // Optional (nullable) foreign key

            // Configure the relationship between Reservation and User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.users) // A Reservation is linked to one User
                .WithMany(u => u.Reservations) // A User can have many Reservations
                .HasForeignKey(r => r.userId); // Foreign key on userId in Reservation

            // Ensure `Id` properties are auto-generated
            modelBuilder.Entity<Chambre>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Service>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }
    }
    }
