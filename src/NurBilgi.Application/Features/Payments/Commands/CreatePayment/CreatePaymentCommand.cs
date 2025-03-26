using MediatR;
using NurBilgi.Domain.DTOs.Payment;

namespace NurBilgi.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<PaymentResult>
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProductId { get; set; }
        public string CustomerEmail { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        
        // Optional card details for direct processing
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string Cvv { get; set; }
    }
} 