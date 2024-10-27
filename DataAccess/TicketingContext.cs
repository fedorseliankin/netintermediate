using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess
{
    public class TicketingContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<PriceOption> PriceOptions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public TicketingContext(DbContextOptions<TicketingContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Sections)
                .WithOne(s => s.Venue)
                .HasForeignKey(s => s.VenueId);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CartId);
            modelBuilder.Entity<Seat>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (SeatStatus)Enum.Parse(typeof(SeatStatus), v));
        }
    }
}
