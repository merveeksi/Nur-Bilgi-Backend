using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace NurBilgi.WebApi.Hubs
{
    [Authorize] 
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            // Kullanıcı kimliğini almak
            var userId = Context.UserIdentifier;
            
            if (!string.IsNullOrEmpty(userId))
            {
                // Kullanıcıyı özel bir gruba ekleyebilirsiniz (opsiyonel)
                // Örneğin, tüm bildirimleri alacak bir admin grubu vs.
                // await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");

                Console.WriteLine($"--> Kullanıcı bağlandı: {userId}, Bağlantı ID: {Context.ConnectionId}");

                // Opsiyonel: Kullanıcı bağlandığında bekleyen/okunmamış bildirimleri gönderebilirsiniz.
                // await SendPendingNotificationsToCaller(userId);
            }
            else
            {
                // Kimliği doğrulanmamış kullanıcılar için işlem (eğer [Authorize] yoksa)
                // Console.WriteLine($"--> Anonim kullanıcı bağlandı: {Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
             if (!string.IsNullOrEmpty(userId))
            {
                 Console.WriteLine($"--> Kullanıcı ayrıldı: {userId}, Bağlantı ID: {Context.ConnectionId}");
                 // Kullanıcıyı gruptan çıkarma (eğer eklediyseniz)
                 // await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId}");
            }
             else
            {
                 Console.WriteLine($"--> Anonim kullanıcı ayrıldı: {Context.ConnectionId}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Opsiyonel: İstemciden çağrılabilecek bir test metodu
        public async Task SendTestNotificationToAll(string message)
        {
             Console.WriteLine($"--> Test mesajı gönderiliyor: {message}");
            // Bu metot test amacıyla tüm bağlı istemcilere mesaj gönderir.
            await Clients.All.SendAsync("ReceiveTestNotification", $"Sunucudan test mesajı: {message}");
        }

        // --- Yardımcı Metot Örneği (Opsiyonel) ---
        // private async Task SendPendingNotificationsToCaller(string userId)
        // {
        //     // TODO: Veritabanından bu userId için bekleyen bildirimleri çek
        //     // var pendingNotifications = _notificationService.GetPendingNotifications(userId);
        //     // foreach (var notification in pendingNotifications)
        //     // {
        //     //     // Sadece bu bağlantıya (Caller) gönder
        //     //     await Clients.Caller.SendAsync("ReceiveAlarm", notification);
        //     // }
        // }
    }
}