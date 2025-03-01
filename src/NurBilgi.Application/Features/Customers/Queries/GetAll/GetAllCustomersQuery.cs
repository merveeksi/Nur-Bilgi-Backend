using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Queries.GetAll
{
    public sealed record GetAllCustomersQuery : IRequest<List<CustomerGetAllDto>>, ICacheable
    {
        // Arama terimi: UserName, FullName veya Email üzerinde arama yapabiliriz.
        public string SearchTerm { get; set; } = string.Empty;

        public string CacheGroup => "Customers";

        public GetAllCustomersQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}