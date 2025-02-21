using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Update;

public sealed class UpdateDailyDuaCommandHandler 
    : IRequestHandler<UpdateDailyDuaCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateDailyDuaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateDailyDuaCommand request, CancellationToken cancellationToken)
    {
        var dailyDua = await _context.DailyDuas
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (dailyDua is null)
        {
            return ResponseDto<long>.Error("DailyDua not found");
        }

        dailyDua.DuaText = request.DuaText;
        dailyDua.ArabicText = request.ArabicText;
        dailyDua.Category = request.Category;
        dailyDua.Source = request.Source;
        dailyDua.TimeOfDay = request.TimeOfDay;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(dailyDua.Id, "DailyDua updated successfully");
    }
}