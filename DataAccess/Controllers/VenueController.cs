using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using net_inermediate.DataAccess.Models;
using net_inermediate.DataAccess.Repositories;

namespace net_inermediate.DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueRepository _venueRepository;

        public VenuesController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        // GET /venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues(CancellationToken ct)
        {
            return Ok(await _venueRepository.GetAllVenuesAsync(ct));
        }

        // GET /venues/{venue_id}/sections
        [HttpGet("{venueId}/sections")]
        public async Task<ActionResult<IEnumerable<Section>>> GetSectionsByVenueId(int venueId, CancellationToken ct)
        {
            var sections = await _venueRepository.GetSectionsByVenueIdAsync(venueId, ct);
            if (sections == null || !sections.Any())
            {
                return NotFound();
            }
            return Ok(sections);
        }
    }
}
