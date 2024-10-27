using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess.Repositories
{
    public class TicketRepository
    {
        private readonly TicketingContext _context;

        public TicketRepository(TicketingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket ticket, CancellationToken ct)
        {
            await _context.Tickets.AddAsync(ticket, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Ticket> Get(int id, CancellationToken ct)
        {
            return await _context.Tickets.FindAsync(id, ct);
        }

        public async Task Update(Ticket ticket, CancellationToken ct)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(int ticketId, CancellationToken ct)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId, ct);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
