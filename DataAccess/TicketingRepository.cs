using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess
{
    public class TicketRepository
    {
        public void Add(Ticket ticket)
        {
            using (var context = new TicketingContext())
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
        }

        public Ticket Get(int id)
        {
            using (var context = new TicketingContext())
            {
                return context.Tickets.Find(id);
            }
        }

        public void Update(Ticket ticket)
        {
            using (var context = new TicketingContext())
            {
                context.Entry(ticket).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int ticketId)
        {
            using (var context = new TicketingContext())
            {
                var ticket = context.Tickets.Find(ticketId);
                if (ticket != null)
                {
                    context.Tickets.Remove(ticket);
                    context.SaveChanges();
                }
            }
        }
    }
}
