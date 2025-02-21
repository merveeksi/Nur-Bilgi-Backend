using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Create;

public sealed class CreateDailyDuaCommandHandler : IRequestHandler<CreateDailyDuaCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateDailyDuaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(CreateDailyDuaCommand request, CancellationToken cancellationToken)
    {
        var dailyDua = DailyDua.Create(request.DuaText, request.ArabicText, request.Category, request.Source, request.TimeOfDay);

        await _context.DailyDuas.AddAsync(dailyDua, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(dailyDua.Id, "Daily dua created successfully");
    }
}