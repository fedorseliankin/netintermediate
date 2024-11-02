using Microsoft.AspNetCore.Mvc;
using net_inermediate.DataAccess.Models;
using net_inermediate.DataAccess.Repositories;

namespace net_inermediate.DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventRepository _eventRepository;

        public EventsController(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: /events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(CancellationToken ct)
        {
            return Ok(await _eventRepository.ListAsync(ct));
        }

        // GET: /events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id, CancellationToken ct)
        {
            var eventItem = await _eventRepository.GetAsync(id, ct);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }
    }
}
