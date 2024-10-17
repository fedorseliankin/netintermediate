using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess
{
    public class EventRepository
    {
        public void Add(Event e)
        {
            using (var context = new TicketingContext())
            {
                context.Events.Add(e);
                context.SaveChanges();
            }
        }

        public Event Get(int id)
        {
            using (var context = new TicketingContext())
            {
                return context.Events.Find(id);
            }
        }

        public void UpdateTicket(Event e)
        {
            using (var context = new TicketingContext())
            {
                context.Entry(e).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new TicketingContext())
            {
                var e = context.Events.Find(id);
                if (e != null)
                {
                    context.Events.Remove(e);
                    context.SaveChanges();
                }
            }
        }
    }
}
