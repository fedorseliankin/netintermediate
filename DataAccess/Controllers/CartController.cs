using Microsoft.AspNetCore.Mvc;
using net_inermediate.DataAccess.Models;
using net_inermediate.DataAccess.Repositories;

namespace net_inermediate.DataAccess.Controllers
{
    [Route("api/orders/carts")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CartRepository _cartRepository;

        public OrdersController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(Guid cartId, CancellationToken ct)
        {
            var cart = await _cartRepository.GetCartAsync(cartId, ct);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("{cartId}")]
        public async Task<IActionResult> AddToCart(Guid cartId, [FromBody] CartItem item, CancellationToken ct)
        {
            await _cartRepository.AddToCartAsync(cartId, item, ct);
            return Ok(await _cartRepository.GetCartAsync(cartId, ct));
        }

        [HttpDelete("{cartId}/events/{eventId}/seats/{seatId}")]
        public async Task<IActionResult> RemoveFromCart(Guid cartId, int eventId, int seatId, CancellationToken ct)
        {
            await _cartRepository.RemoveFromCartAsync(cartId, eventId, seatId, ct);
            return Ok(await _cartRepository.GetCartAsync(cartId, ct));
        }

        [HttpPut("{cartId}/book")]
        public async Task<IActionResult> BookCart(Guid cartId, CancellationToken ct)
        {
            await _cartRepository.ClearCartAsync(cartId, ct);

            Guid paymentId = Guid.NewGuid();
            return Ok(new { PaymentId = paymentId });
        }
    }
}
