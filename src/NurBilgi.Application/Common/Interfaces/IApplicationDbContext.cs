using Microsoft.EntityFrameworkCore;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<AiChatMessage> AiChatMessages { get; set; }
    public DbSet<DailyDua> DailyDuas { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<PrayerTime> PrayerTimes { get; set; }
    public DbSet<Surah> Surahs { get; set; }
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}