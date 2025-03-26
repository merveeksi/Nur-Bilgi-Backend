using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DTOs.Payment;
using NurBilgi.Domain.Entities;
using NurBilgi.Domain.Enum;
using NurBilgi.Domain.Interfaces;

namespace NurBilgi.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentResult>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<CreatePaymentCommandHandler> _logger;
        // Typically you would inject your DB context or repository here
        // private readonly IRepository<Payment> _paymentRepository;

        public CreatePaymentCommandHandler(
            IPaymentService paymentService,
            ILogger<CreatePaymentCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<PaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating payment for amount {Amount} {Currency}", command.Amount, command.Currency);
                
                // Map command to request
                var request = new CreatePaymentRequest
                {
                    Amount = command.Amount,
                    Currency = command.Currency,
                    ProductId = command.ProductId,
                    CustomerEmail = command.CustomerEmail,
                    SuccessUrl = command.SuccessUrl,
                    CancelUrl = command.CancelUrl,
                    CardNumber = command.CardNumber,
                    CardHolderName = command.CardHolderName,
                    ExpirationMonth = command.ExpirationMonth ?? 0,
                    ExpirationYear = command.ExpirationYear ?? 0,
                    Cvv = command.Cvv
                };

                // Process payment through service
                var result = await _paymentService.CreatePaymentAsync(request);

                if (result.Success)
                {
                    // Create a payment record in the database
                    var payment = new Payment
                    {
                        ExternalPaymentId = result.PaymentId,
                        Amount = command.Amount,
                        Currency = command.Currency,
                        Status = PaymentStatus.Pending,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    // Set card details if provided (optional)
                    if (!string.IsNullOrEmpty(command.CardNumber) && !string.IsNullOrEmpty(command.Cvv))
                    {
                        payment.SetCardDetails(command.CardNumber, command.Cvv);
                        payment.CardHolderName = command.CardHolderName;
                        payment.ExpirationMonth = command.ExpirationMonth ?? 0;
                        payment.ExpirationYear = command.ExpirationYear ?? 0;
                    }

                    // Save to database (using repository pattern)
                    // In a real implementation, you'd save this to your database
                    // await _paymentRepository.AddAsync(payment);

                    _logger.LogInformation("Payment record created with ID {PaymentId}", payment.Id);
                }
                else
                {
                    _logger.LogWarning("Payment creation failed: {ErrorMessage}", result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreatePaymentCommandHandler");
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = $"Payment processing error: {ex.Message}"
                };
            }
        }
    }
} 