namespace NurBilgi.Domain.DTOs.Payment
{
    public class RefundRequest
    {
        public string PaymentId { get; set; }
        public decimal? RefundAmount { get; set; } // Optional for partial refunds
        public string Reason { get; set; }
    }
} 