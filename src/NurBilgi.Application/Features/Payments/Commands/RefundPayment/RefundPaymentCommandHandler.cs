using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DTOs.Payment;
using NurBilgi.Domain.Enum;
using NurBilgi.Domain.Interfaces;

namespace NurBilgi.Application.Features.Payments.Commands.RefundPayment
{
    public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, RefundResult>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<RefundPaymentCommandHandler> _logger;
        // private readonly IRepository<Payment> _paymentRepository;

        public RefundPaymentCommandHandler(
            IPaymentService paymentService,
            ILogger<RefundPaymentCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<RefundResult> Handle(RefundPaymentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing refund for payment {PaymentId}", command.PaymentId);
                
                // Create refund request
                var request = new RefundRequest
                {
                    PaymentId = command.PaymentId,
                    RefundAmount = command.RefundAmount,
                    Reason = command.Reason
                };

                // Process refund
                var result = await _paymentService.RefundPaymentAsync(request);

                if (result.Success)
                {
                    // Update payment status in database
                    /* In a real implementation:
                    var payment = await _paymentRepository.GetByExternalIdAsync(command.PaymentId);
                    if (payment != null)
                    {
                        payment.Status = PaymentStatus.Cancelled;
                        payment.UpdatedAt = DateTime.UtcNow;
                        await _paymentRepository.UpdateAsync(payment);
                    }
                    */

                    _logger.LogInformation("Refund processed successfully for payment {PaymentId}", command.PaymentId);
                }
                else
                {
                    _logger.LogWarning("Refund failed: {ErrorMessage}", result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RefundPaymentCommandHandler");
                return new RefundResult
                {
                    Success = false,
                    ErrorMessage = $"Refund processing error: {ex.Message}"
                };
            }
        }
    }
} 