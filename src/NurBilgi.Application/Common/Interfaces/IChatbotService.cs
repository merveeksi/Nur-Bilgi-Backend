namespace NurBilgi.Application.Common.Interfaces;

public interface IChatbotService
{
    Task<string> GetResponseAsync(string prompt, CancellationToken cancellationToken = default);
}
