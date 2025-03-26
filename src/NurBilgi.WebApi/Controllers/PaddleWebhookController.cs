using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NurBilgi.Domain.Enum;
using NurBilgi.Infrastructure.Services;

namespace NurBilgi.WebApi.Controllers
{
    [ApiController]
    [Route("api/webhook/paddle")]
    public class PaddleWebhookController : ControllerBase
    {
        private readonly ILogger<PaddleWebhookController> _logger;
        private readonly PaddleOptions _paddleOptions;
        // Inject your repositories or services to update payment status
        // private readonly IRepository<Payment> _paymentRepository;

        public PaddleWebhookController(
            ILogger<PaddleWebhookController> logger,
            IOptions<PaddleOptions> paddleOptions)
        {
            _logger = logger;
            _paddleOptions = paddleOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                // Read the body so we can verify the signature
                string requestBody;
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                // Verify Paddle signature
                if (!VerifyPaddleSignature(requestBody))
                {
                    _logger.LogWarning("Invalid webhook signature");
                    return BadRequest("Invalid signature");
                }

                // Parse the form data from request body
                var formData = ParseFormData(requestBody);
                
                // Extract alert_name and checkout/order ID
                string alertName = formData.ContainsKey("alert_name") ? formData["alert_name"] : string.Empty;
                string orderId = formData.ContainsKey("order_id") ? formData["order_id"] : string.Empty;

                _logger.LogInformation("Received Paddle webhook: {AlertName} for order {OrderId}", alertName, orderId);

                // Handle different webhook types
                switch (alertName)
                {
                    case "payment_succeeded":
                        await HandlePaymentSucceeded(formData);
                        break;
                    case "payment_refunded":
                        await HandlePaymentRefunded(formData);
                        break;
                    case "subscription_created":
                        await HandleSubscriptionCreated(formData);
                        break;
                    // Handle other webhook types as needed
                    default:
                        _logger.LogInformation("Unhandled webhook type: {AlertName}", alertName);
                        break;
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Paddle webhook");
                return StatusCode(500, "Error processing webhook");
            }
        }

        private async Task HandlePaymentSucceeded(Dictionary<string, string> formData)
        {
            /* 
               In a real implementation:
               
               var orderId = formData["order_id"];
               var payment = await _paymentRepository.GetByExternalIdAsync(orderId);
               
               if (payment != null)
               {
                   payment.Status = PaymentStatus.Completed;
                   payment.IsPaid = true;
                   payment.UpdatedAt = DateTime.UtcNow;
                   await _paymentRepository.UpdateAsync(payment);
                   
                   // Trigger additional business logic like:
                   // - Send confirmation email
                   // - Update subscription status
                   // - Create order records
                   // etc.
               }
            */
            
            _logger.LogInformation("Payment succeeded for order {OrderId}", 
                formData.ContainsKey("order_id") ? formData["order_id"] : "unknown");
            
            // Implementation placeholder
            await Task.CompletedTask;
        }

        private async Task HandlePaymentRefunded(Dictionary<string, string> formData)
        {
            _logger.LogInformation("Payment refunded for order {OrderId}", 
                formData.ContainsKey("order_id") ? formData["order_id"] : "unknown");
            
            // Implementation placeholder
            await Task.CompletedTask;
        }

        private async Task HandleSubscriptionCreated(Dictionary<string, string> formData)
        {
            _logger.LogInformation("Subscription created for order {OrderId}", 
                formData.ContainsKey("order_id") ? formData["order_id"] : "unknown");
            
            // Implementation placeholder  
            await Task.CompletedTask;
        }

        private bool VerifyPaddleSignature(string requestBody)
        {
            try
            {
                var formData = ParseFormData(requestBody);
                
                if (!formData.ContainsKey("p_signature"))
                {
                    _logger.LogWarning("No signature found in webhook");
                    return false;
                }

                string signature = formData["p_signature"];
                formData.Remove("p_signature");

                // Sort parameters alphabetically
                var sortedParams = formData.OrderBy(x => x.Key).ToList();
                
                // Concatenate values
                var serializedData = string.Join("", sortedParams.Select(x => x.Value));
                
                // Add the webhook secret (from your Paddle account)
                serializedData += _paddleOptions.WebhookSecret;

                // Generate PHP style md5 hash
                string hash;
                using (var md5 = MD5.Create())
                {
                    byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(serializedData));
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }

                // Compare the computed hash with the provided signature
                return hash == signature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying Paddle signature");
                return false;
            }
        }

        private Dictionary<string, string> ParseFormData(string requestBody)
        {
            var result = new Dictionary<string, string>();
            
            foreach (var part in requestBody.Split('&'))
            {
                if (string.IsNullOrEmpty(part))
                    continue;
                    
                var keyValue = part.Split('=');
                if (keyValue.Length == 2)
                {
                    var key = Uri.UnescapeDataString(keyValue[0]);
                    var value = Uri.UnescapeDataString(keyValue[1]);
                    result[key] = value;
                }
            }
            
            return result;
        }
    }
} 