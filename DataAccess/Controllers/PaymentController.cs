using Microsoft.AspNetCore.Mvc;
using net_inermediate.DataAccess.Repositories;

namespace net_inermediate.DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentsController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPayment(Guid paymentId, CancellationToken ct)
        {
            var payment = await _paymentRepository.GetPaymentAsync(paymentId, ct);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost("{paymentId}/complete")]
        public async Task<IActionResult> CompletePayment(Guid paymentId, CancellationToken ct)
        {
            await _paymentRepository.UpdatePaymentAndSeatsStatusAsync(paymentId, "Complete", "Sold", ct);
            return Ok();
        }

        [HttpPost("{paymentId}/failed")]
        public async Task<IActionResult> FailPayment(Guid paymentId, CancellationToken ct)
        {
            await _paymentRepository.UpdatePaymentAndSeatsStatusAsync(paymentId, "Failed", "Available", ct);
            return Ok();
        }
    }
}
