using MediatR;

namespace NurBilgi.Application.Features.Notes.Queries.GetById
{
    public sealed record NoteGetByIdQuery(long Id) : IRequest<NoteGetByIdDto>;
}