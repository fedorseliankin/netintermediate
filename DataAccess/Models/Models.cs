namespace net_inermediate.DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }

        // Navigation property
        public Event Event { get; set; }
    }
}
