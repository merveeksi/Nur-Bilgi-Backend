using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Infrastructure.Services;

public class CacheInvalidator : ICacheInvalidator
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheInvalidator> _logger;

    public CacheInvalidator(
        IDistributedCache cache,
        ILogger<CacheInvalidator> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task InvalidateAsync(string cacheKey, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(cacheKey, cancellationToken);

        _logger.LogInformation($"[Cache Invalidate] Key: {cacheKey}");
    }

    public async Task InvalidateGroupAsync(string cacheGroup, CancellationToken cancellationToken)
    {
        var groupSetKey = $"Group:{cacheGroup}";
        
    }
}