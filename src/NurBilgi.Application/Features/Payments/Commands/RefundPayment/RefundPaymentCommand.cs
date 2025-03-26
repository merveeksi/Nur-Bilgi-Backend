using MediatR;
using NurBilgi.Domain.DTOs.Payment;

namespace NurBilgi.Application.Features.Payments.Commands.RefundPayment
{
    public class RefundPaymentCommand : IRequest<RefundResult>
    {
        public string PaymentId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Reason { get; set; }
    }
} 