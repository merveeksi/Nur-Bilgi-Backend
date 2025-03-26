using NurBilgi.Application.Common.Interfaces;
using Google.Cloud.AIPlatform.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;
using Value = Google.Protobuf.WellKnownTypes.Value;

namespace NurBilgi.Infrastructure.Services
{
    public class GeminiChatbotService : IChatbotService
    {
        private readonly PredictionServiceClient _predictionServiceClient;
        private readonly string _endpointName;
        private readonly ILogger<GeminiChatbotService> _logger;
        private readonly string _projectId;

        public GeminiChatbotService(
            IConfiguration configuration,
            PredictionServiceClient predictionServiceClient,
            ILogger<GeminiChatbotService> logger)
        {
            _logger = logger;
            _predictionServiceClient = predictionServiceClient;

            _projectId = configuration["GoogleCloud:ProjectId"]
                ?? throw new ArgumentNullException(nameof(configuration), "GoogleCloud:ProjectId yapılandırması eksik.");
            string? locationId = configuration["GoogleCloud:LocationId"];
            string publisher = configuration["GoogleCloud:Publisher"] ?? "google";
            string? modelId = configuration["GoogleCloud:ModelId"];

            if (string.IsNullOrWhiteSpace(locationId))
                throw new ArgumentNullException(nameof(configuration), "GoogleCloud:LocationId yapılandırması eksik.");
            if (string.IsNullOrWhiteSpace(modelId))
                throw new ArgumentNullException(nameof(configuration), "GoogleCloud:ModelId yapılandırması eksik.");

            _endpointName = EndpointName.FromProjectLocationPublisherModel(_projectId, locationId, publisher, modelId).ToString();

            _logger.LogInformation("GeminiChatbotService başlatıldı. Endpoint: {EndpointName}", _endpointName);
        }

       public async Task<string> GetResponseAsync(string prompt, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Gemini API'ye GenerateContentAsync ile istek gönderiliyor. Prompt: '{Prompt}'", prompt);

            try
            {
                // GenerateContentRequest için içerik yapısını oluştur
                var content = new Content
                {
                    Role = "user", // Rol "user" olmalı
                    Parts = { new Part { Text = prompt } }
                };

                // GenerateContentRequest nesnesini oluştur
                var request = new GenerateContentRequest
                {
                    // Endpoint yerine doğrudan model adı kullanılır
                    Model = _endpointName, // _endpointName constructor'da oluşturulan tam model yolunu tutuyor olmalı
                    Contents = { content }
                    // GenerationConfig ekleyerek temperature, max tokens gibi parametreleri ayarlayabilirsiniz
                    // GenerationConfig = new GenerationConfig { Temperature = 0.7f, MaxOutputTokens = 2048 }
                };

                // PredictAsync yerine GenerateContentAsync çağırılır
                GenerateContentResponse response = await _predictionServiceClient.GenerateContentAsync(request, cancellationToken: cancellationToken);
                _logger.LogInformation("Gemini API'den yanıt alındı (GenerateContentAsync).");

                // Yanıtı parse etme şekli GenerateContentResponse'a göre değişir
                // Genellikle response.Candidates[0].Content.Parts[0].Text şeklinde olur
                var responseText = response.Candidates.FirstOrDefault()? // İlk adayı al
                                        .Content.Parts.FirstOrDefault()? // İçeriğin ilk parçasını al
                                        .Text; // Metni al

                if (responseText != null)
                {
                    _logger.LogInformation("Gemini yanıtı başarıyla parse edildi.");
                    return responseText;
                }
                else
                {
                    // Güvenlik nedeniyle veya başka bir sebeple yanıt gelmemiş olabilir
                    var blockReason = response.Candidates.FirstOrDefault()?.FinishReason;
                    var safetyRatings = response.Candidates.FirstOrDefault()?.SafetyRatings;
                    _logger.LogWarning("Gemini yanıtı metin içermiyor veya boş. BlockReason: {BlockReason}, SafetyRatings: {SafetyRatings}", blockReason, safetyRatings);
                    return $"Modelden geçerli bir metin yanıtı alınamadı. (Sebep: {blockReason})";
                }
            }
            catch (Grpc.Core.RpcException ex)
            {
                _logger.LogError(ex, "Gemini API çağrısı sırasında gRPC hatası oluştu (GenerateContentAsync). Status: {StatusCode}, Project: {ProjectId}, Model: {Model}", ex.StatusCode, _projectId, _endpointName);
                return $"API ile iletişim kurulamadı (Hata Kodu: {ex.StatusCode}). Lütfen tekrar deneyin veya sistem yöneticisine başvurun.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gemini yanıtı işlenirken beklenmedik bir hata oluştu (GenerateContentAsync). Project: {ProjectId}, Model: {Model}", _projectId, _endpointName);
                return "İşlem sırasında beklenmedik bir sunucu hatası oluştu.";
            }
        }
    }
}