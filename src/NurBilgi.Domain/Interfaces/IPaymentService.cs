using System.Threading.Tasks;
using NurBilgi.Domain.DTOs.Payment;

namespace NurBilgi.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> CreatePaymentAsync(CreatePaymentRequest request);
        Task<RefundResult> RefundPaymentAsync(RefundRequest request);
        Task<PaymentResult> VerifyPaymentAsync(string paymentId);
        // Additional methods could include subscription management, payment status checks, etc.
    }
} 