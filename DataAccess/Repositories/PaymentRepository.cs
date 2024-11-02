using Microsoft.EntityFrameworkCore;
using net_inermediate.DataAccess.Models;

namespace net_inermediate.DataAccess.Repositories
{
    public class PaymentRepository
    {
        private readonly TicketingContext _context;

        public PaymentRepository(TicketingContext context)
        {
            _context = context;
        }
        public SeatStatus ConvertStringToSeatStatus(string status)
        {
            if (Enum.TryParse<SeatStatus>(status, out var seatStatus))
            {
                return seatStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid seat status value: {status}");
            }
        }
        public async Task<Payment> GetPaymentAsync(Guid paymentId, CancellationToken ct)
        {
            return await _context.Payments
                .Include(p => p.Seats)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId, ct);
        }

        public async Task UpdatePaymentAndSeatsStatusAsync(Guid paymentId, string paymentStatus, string seatStatus, CancellationToken ct)
        {
            var payment = await GetPaymentAsync(paymentId, ct);
            if (payment == null)
                throw new KeyNotFoundException("Payment not found.");

            payment.Status = paymentStatus;

            var seatStatusEnum = ConvertStringToSeatStatus(seatStatus);

            if (payment.Seats != null && payment.Seats.Any())
            {
                foreach (var seat in payment.Seats)
                {
                    seat.Status = seatStatusEnum;
                }
                _context.UpdateRange(payment.Seats);
            }

            _context.Update(payment);
            await _context.SaveChangesAsync(ct);
        }
    }
}
