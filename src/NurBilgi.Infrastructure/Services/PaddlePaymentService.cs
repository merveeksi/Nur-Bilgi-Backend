using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NurBilgi.Domain.DTOs.Payment;
using NurBilgi.Domain.Interfaces;

namespace NurBilgi.Infrastructure.Services
{
    public class PaddlePaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaddlePaymentService> _logger;
        private readonly PaddleOptions _options;

        public PaddlePaymentService(
            HttpClient httpClient,
            IOptions<PaddleOptions> options,
            ILogger<PaddlePaymentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options.Value;

            // Configure HttpClient with base address and default headers
            _httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Vendor-Id", _options.VendorId);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.ApiKey}");
        }

        public async Task<PaymentResult> CreatePaymentAsync(CreatePaymentRequest request)
        {
            try
            {
                // Build Paddle-specific payload
                var payload = new
                {
                    amount = request.Amount,
                    currency = request.Currency,
                    customer_email = request.CustomerEmail,
                    product_id = request.ProductId,
                    passthrough = JsonSerializer.Serialize(new { customData = "Your custom data here" }),
                    return_url = request.SuccessUrl,
                    cancel_url = request.CancelUrl
                };

                // Send request to Paddle API
                var response = await _httpClient.PostAsJsonAsync("/api/2.0/product/generate_pay_link", payload);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Paddle API error: {ErrorContent}", errorContent);
                    
                    return new PaymentResult
                    {
                        Success = false,
                        ErrorMessage = $"Paddle API failed: {errorContent}"
                    };
                }

                // Parse response based on Paddle API structure
                var paddleResponse = await response.Content.ReadFromJsonAsync<PaddleCreatePaymentResponse>();
                
                if (paddleResponse?.Success != true)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        ErrorMessage = paddleResponse?.Error?.Message ?? "Unknown error from Paddle"
                    };
                }

                // Return successful result
                return new PaymentResult
                {
                    Success = true,
                    PaymentId = paddleResponse.Response.PaymentId,
                    CheckoutUrl = paddleResponse.Response.Url,
                    TransactionReference = paddleResponse.Response.PaymentId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment with Paddle");
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = $"Payment processing error: {ex.Message}"
                };
            }
        }

        public async Task<RefundResult> RefundPaymentAsync(RefundRequest request)
        {
            try
            {
                var payload = new
                {
                    order_id = request.PaymentId,
                    amount = request.RefundAmount,
                    reason = request.Reason
                };

                var response = await _httpClient.PostAsJsonAsync("/api/2.0/payment/refund", payload);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Paddle refund API error: {ErrorContent}", errorContent);
                    
                    return new RefundResult
                    {
                        Success = false,
                        ErrorMessage = $"Paddle refund API failed: {errorContent}"
                    };
                }

                var paddleResponse = await response.Content.ReadFromJsonAsync<PaddleRefundResponse>();
                
                if (paddleResponse?.Success != true)
                {
                    return new RefundResult
                    {
                        Success = false,
                        ErrorMessage = paddleResponse?.Error?.Message ?? "Unknown error from Paddle"
                    };
                }

                return new RefundResult
                {
                    Success = true,
                    RefundId = paddleResponse.Response.RefundId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing refund with Paddle");
                return new RefundResult
                {
                    Success = false,
                    ErrorMessage = $"Refund processing error: {ex.Message}"
                };
            }
        }

        public async Task<PaymentResult> VerifyPaymentAsync(string paymentId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/2.0/payment/details?payment_id={paymentId}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Paddle verification API error: {ErrorContent}", errorContent);
                    
                    return new PaymentResult
                    {
                        Success = false,
                        ErrorMessage = $"Payment verification failed: {errorContent}"
                    };
                }

                var paddleResponse = await response.Content.ReadFromJsonAsync<PaddleVerifyPaymentResponse>();
                
                if (paddleResponse?.Success != true)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        ErrorMessage = paddleResponse?.Error?.Message ?? "Unknown error from Paddle"
                    };
                }

                // Check payment status logic here
                bool isPaid = paddleResponse.Response.Status == "completed";

                return new PaymentResult
                {
                    Success = isPaid,
                    PaymentId = paymentId,
                    ErrorMessage = !isPaid ? "Payment not completed" : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying payment with Paddle");
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = $"Payment verification error: {ex.Message}"
                };
            }
        }
    }

    // Paddle API response types
    public class PaddleCreatePaymentResponse
    {
        public bool Success { get; set; }
        public PaddleErrorDetails Error { get; set; }
        public PaddleCreatePaymentResponseData Response { get; set; }
    }

    public class PaddleCreatePaymentResponseData
    {
        public string Url { get; set; }
        public string PaymentId { get; set; }
    }

    public class PaddleRefundResponse
    {
        public bool Success { get; set; }
        public PaddleErrorDetails Error { get; set; }
        public PaddleRefundResponseData Response { get; set; }
    }

    public class PaddleRefundResponseData
    {
        public string RefundId { get; set; }
    }

    public class PaddleVerifyPaymentResponse
    {
        public bool Success { get; set; }
        public PaddleErrorDetails Error { get; set; }
        public PaddleVerifyPaymentResponseData Response { get; set; }
    }

    public class PaddleVerifyPaymentResponseData
    {
        public string Status { get; set; }
    }

    public class PaddleErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
} 