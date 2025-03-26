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
                // Kullanıcının mesajını içeren Content nesnesi
                var userContent = new Content
                {
                    Role = "user",
                    Parts = { new Part { Text = prompt } }
                };

                // ---->> YENİ EKLENEN SİSTEM TALİMATI <<----
                var systemInstruction = new Content
                {
                    // Sistem talimatı genellikle özel bir role sahip olmaz veya Parts olarak verilir.
                    // Dokümantasyon SystemInstruction'ın Content tipinde olduğunu belirtiyor.
                    // Sadece text içeren bir Part yeterli olacaktır.
                    Parts = { new Part { Text = "Sen sadece İslami ve dini konularda soruları cevaplayan bir asistansın. Sorular yalnızca bu konularla ilgiliyse cevap ver. Diğer tüm konulardaki sorular için, o konuda bilgin olmadığını veya yardımcı olamayacağını belirt." } }
                };
                // ---->> ------------------------------ <<----


                // GenerateContentRequest nesnesini oluştururken SystemInstruction'ı ekle
                var request = new GenerateContentRequest
                {
                    Model = _endpointName,
                    // Sistem Talimatını buraya ekliyoruz
                    SystemInstruction = systemInstruction,
                    // Kullanıcının mevcut mesajını ekliyoruz
                    Contents = { userContent }
                    // İsteğe bağlı: Önceki mesajları da buraya ekleyerek sohbet geçmişi oluşturulabilir
                    // Contents = { previousMessage1, previousMessage2, userContent }
                    // GenerationConfig = new GenerationConfig { ... }
                };

                // --- Geri kalan kod aynı ---
                GenerateContentResponse response = await _predictionServiceClient.GenerateContentAsync(request, cancellationToken: cancellationToken);
                // ... yanıtı parse etme ve döndürme kısmı ...
                _logger.LogInformation("Gemini API'den yanıt alındı (GenerateContentAsync).");

                var responseText = response.Candidates.FirstOrDefault()?
                                        .Content.Parts.FirstOrDefault()?
                                        .Text;

                if (responseText != null)
                {
                    _logger.LogInformation("Gemini yanıtı başarıyla parse edildi.");
                    return responseText;
                }
                else
                {
                    var blockReason = response.Candidates.FirstOrDefault()?.FinishReason;
                    var safetyRatings = response.Candidates.FirstOrDefault()?.SafetyRatings;
                    _logger.LogWarning("Gemini yanıtı metin içermiyor veya boş. BlockReason: {BlockReason}, SafetyRatings: {SafetyRatings}", blockReason, safetyRatings);
                    // Sistem talimatına uymadığı için boş cevap gelirse de bu loga düşebilir.
                    // Kullanıcıya daha gene bir mesaj vermek daha iyi olabilir.
                    // return $"Modelden geçerli bir metin yanıtı alınamadı. (Sebep: {blockReason})";
                    return "Üzgünüm, şu an yardımcı olamıyorum veya bu konuda bilgim yok.";
                }
            }
            catch (Grpc.Core.RpcException ex) // Hata yakalama aynı
            {
                _logger.LogError(ex, "Gemini API çağrısı sırasında gRPC hatası oluştu (GenerateContentAsync). Status: {StatusCode}, Project: {ProjectId}, Model: {Model}", ex.StatusCode, _projectId, _endpointName);
                return $"API ile iletişim kurulamadı (Hata Kodu: {ex.StatusCode}). Lütfen tekrar deneyin veya sistem yöneticisine başvurun.";
            }
            catch (Exception ex) // Hata yakalama aynı
            {
                _logger.LogError(ex, "Gemini yanıtı işlenirken beklenmedik bir hata oluştu (GenerateContentAsync). Project: {ProjectId}, Model: {Model}", _projectId, _endpointName);
                return "İşlem sırasında beklenmedik bir sunucu hatası oluştu.";
            }
        }
    }
}