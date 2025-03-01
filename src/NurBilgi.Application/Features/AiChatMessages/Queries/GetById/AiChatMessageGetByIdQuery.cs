using MediatR;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

public sealed record AiChatMessageGetByIdQuery(long Id) : IRequest<AiChatMessageGetByIdDto>;