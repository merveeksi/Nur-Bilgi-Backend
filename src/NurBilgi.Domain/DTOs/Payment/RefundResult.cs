namespace NurBilgi.Domain.DTOs.Payment
{
    public class RefundResult
    {
        public bool Success { get; set; }
        public string RefundId { get; set; }
        public string ErrorMessage { get; set; }
    }
} 