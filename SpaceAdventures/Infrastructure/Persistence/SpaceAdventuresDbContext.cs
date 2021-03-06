
#nullable disable
using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Infrastructure.Persistence
{
    public partial class SpaceAdventuresDbContext : DbContext, ISpaceAdventureDbContext
    {
        public SpaceAdventuresDbContext()
        {
        }

        public SpaceAdventuresDbContext(DbContextOptions<SpaceAdventuresDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_AltDiction_CP850_CI_AI");

            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.HasKey(e => e.IdAircraft);

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.IdAirport);

                entity.ToTable("Airport");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPlanetNavigation)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.IdPlanet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_Planet");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.IdBooking);

                entity.ToTable("Booking");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Client");

                entity.HasOne(d => d.IdFlightNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.IdFlight)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Flight");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("Client");

                entity.HasIndex(e => e.Email, "IX_Client_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Client_User");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.IdFlight);

                entity.ToTable("Flight");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.HasOne(d => d.IdAircraftNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.IdAircraft)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Aircraft");

                entity.HasOne(d => d.IdItineraryNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.IdItinerary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Itinerary");
            });

            modelBuilder.Entity<Itinerary>(entity =>
            {
                entity.HasKey(e => e.IdItinerary);

                entity.ToTable("Itinerary");

                entity.HasIndex(e => new { e.IdAirport1, e.IdAirport2 }, "IX_Itinerary_Aiport1_Aiport2")
                    .IsUnique();

                entity.HasOne(d => d.IdAirport1Navigation)
                    .WithMany(p => p.ItineraryIdAirport1Navigations)
                    .HasForeignKey(d => d.IdAirport1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Itinerary_Airport1");

                entity.HasOne(d => d.IdAirport2Navigation)
                    .WithMany(p => p.ItineraryIdAirport2Navigations)
                    .HasForeignKey(d => d.IdAirport2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Itinerary_Airport2");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.IdPlanet);

                entity.ToTable("Planet");

                entity.HasIndex(e => e.Name, "IX_Planet_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdUserAuth0).IsRequired();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}