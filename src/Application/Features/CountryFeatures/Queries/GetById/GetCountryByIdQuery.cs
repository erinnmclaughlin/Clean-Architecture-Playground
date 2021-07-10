using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Queries.GetById
{
    public class GetCountryByIdQuery : IRequest<Result<GetCountryByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<GetCountryByIdResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCountryByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetCountryByIdResponse>> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == query.Id);

            if (country is null)
                return await Result<GetCountryByIdResponse>.FailAsync("Country not found.");

            var countryResponse = new GetCountryByIdResponse
            {
                Id = country.Id,
                Abbreviation = country.Abbreviation,
                Name = country.Name
            };

            return await Result<GetCountryByIdResponse>.SuccessAsync(countryResponse);
        }
    }
}