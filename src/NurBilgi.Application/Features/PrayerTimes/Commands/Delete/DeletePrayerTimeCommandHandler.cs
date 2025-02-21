using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Delete;

public sealed class DeletePrayerTimeCommandHandler : IRequestHandler<DeletePrayerTimeCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeletePrayerTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeletePrayerTimeCommand request, CancellationToken cancellationToken)
    {
        var prayerTime = await _context.PrayerTimes
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        _context.PrayerTimes.Remove(prayerTime);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(prayerTime.Id, "Prayer time deleted successfully", true);
    }
} 