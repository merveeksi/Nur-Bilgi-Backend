namespace NurBilgi.Domain.DTOs.Payment
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string PaymentId { get; set; }
        public string CheckoutUrl { get; set; }
        public string ErrorMessage { get; set; }
        public string TransactionReference { get; set; }
    }
} 