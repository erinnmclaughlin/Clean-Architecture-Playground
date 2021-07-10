using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CustomerFeatures.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomerByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Include(x => x.Country)
                .Select(x => new GetCustomerByIdResponse
                {
                    Country = x.Country.Name,
                    CountryId = x.CountryId,
                    Id = x.Id,
                    Location = x.Location,
                    Name = x.Name
                }).FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (customer is null)
                return await Result<GetCustomerByIdResponse>.FailAsync("Customer not found.");

            return await Result<GetCustomerByIdResponse>.SuccessAsync(customer);
        }
    }
}