//using net_inermediate.DataAccess;

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();


using net_inermediate.DataAccess;
using net_inermediate.DataAccess.Models;
using System;
using System.Linq;
 
namespace HelloApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (TicketingContext db = new TicketingContext())
            {

                var ticketRepository = new TicketRepository();
                var eventRepository = new EventRepository();

                eventRepository.Add(new Event
                {
                    Id = 1,
                    Name = "Test",
                    DateTime = DateTime.Now,
                    Description = "Test",
                });
                ticketRepository.Add(new Ticket {
                    TicketId = 1,
                    EventId = 1,
                    SeatNumber = "row1seat1",
                    Price = 289,
                });
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

            }
            Console.Read();
        }
    }
}