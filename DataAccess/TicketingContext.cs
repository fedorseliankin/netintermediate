using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess
{
    public class TicketingContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public TicketingContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=netintermediate;user=root;password=Fyodor123!");
        }
    }
}
