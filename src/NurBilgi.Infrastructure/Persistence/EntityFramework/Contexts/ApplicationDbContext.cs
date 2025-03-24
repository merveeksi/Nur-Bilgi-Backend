using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.Entities;
using NurBilgi.Domain.Identity;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Contexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }
    
    public DbSet<AiChatMessage> AiChatMessages { get; set; }
    public DbSet<DailyDua> DailyDuas { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<PrayerTime> PrayerTimes { get; set; }
    public DbSet<Surah> Surahs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Catechism> Catechisms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchDomainEventsAsync(cancellationToken);

        return result;
    }
    
    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker
            .Entries<EntityBase<long>>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .ToArray();

        foreach (var entity in domainEvents)
        {
            var events = entity.GetDomainEvents();

            foreach (var domainEvent in events)
                await _publisher.Publish(domainEvent, cancellationToken);

            entity.ClearDomainEvents();
        }

        var identityUserDomainEvents = ChangeTracker
            .Entries<IdentityUserBase<long>>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .ToArray();

        foreach (var entity in identityUserDomainEvents)
        {
            var events = entity.GetDomainEvents();

            foreach (var domainEvent in events)
                await _publisher.Publish(domainEvent, cancellationToken);

            entity.ClearDomainEvents();
        }
    }
}