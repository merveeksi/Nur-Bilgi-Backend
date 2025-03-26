using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Common.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NurBilgi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
    public class AiChatMessageController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;
        private readonly ILogger<AiChatMessageController> _logger;

        public AiChatMessageController(IChatbotService chatbotService, ILogger<AiChatMessageController> logger)
        {
            _chatbotService = chatbotService;
            _logger = logger;
        }

        // Frontend'den POST isteği ile mesaj almak için bir endpoint (metot)
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] ChatRequest request, CancellationToken cancellationToken)
        {
            // Gelen isteği kontrol edelim
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                _logger.LogWarning("Chat isteği alınamadı veya mesaj boş.");
                // Hatalı istek durumunda 400 Bad Request döndürelim
                return BadRequest(new { Error = "Mesaj içeriği boş olamaz." });
            }

            try
            {
                _logger.LogInformation("Chat isteği alınıyor: '{UserMessage}'", request.Message);

                // IChatbotService üzerinden Gemini'den yanıtı alalım
                string response = await _chatbotService.GetResponseAsync(request.Message, cancellationToken);

                _logger.LogInformation("Chat yanıtı gönderiliyor.");

                // Başarılı yanıtı (200 OK) ve Gemini'nin cevabını JSON olarak döndürelim
                return Ok(new { Reply = response });
            }
            catch (Exception ex)
            {
                // Beklenmedik bir hata olursa loglayalım ve 500 Internal Server Error döndürelim
                _logger.LogError(ex, "Chat isteği işlenirken sunucu hatası oluştu.");
                return StatusCode(500, new { Error = "Mesaj işlenirken bir sunucu hatası oluştu. Lütfen daha sonra tekrar deneyin." });
            }
        }
    }

    // Frontend'den gelecek JSON isteğini temsil eden basit bir sınıf (DTO)
    // Bu sınıfı ayrı bir dosyaya veya projenizde DTO'ları tuttuğunuz yere taşıyabilirsiniz.
    public class ChatRequest
    {
        public string? Message { get; set; }
    }