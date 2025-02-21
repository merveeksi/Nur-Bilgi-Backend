using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Delete;

public sealed class DeleteDailyDuaCommandHandler : IRequestHandler<DeleteDailyDuaCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeleteDailyDuaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteDailyDuaCommand request, CancellationToken cancellationToken)
    {
        var dailyDua = await _context.DailyDuas
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        _context.DailyDuas.Remove(dailyDua);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(dailyDua.Id, "Daily dua deleted successfully", true);
    }
} 