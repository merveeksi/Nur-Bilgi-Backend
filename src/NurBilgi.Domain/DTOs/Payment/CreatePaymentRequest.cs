namespace NurBilgi.Domain.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProductId { get; set; }
        public string CustomerEmail { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        // Card details if processing payment directly
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string Cvv { get; set; }
    }
} 