using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CountryFeatures.Queries.GetAll
{
    public class GetAllCountriesQuery : IRequest<Result<IEnumerable<GetAllCountriesResponse>>>
    {
    }

    internal class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable<GetAllCountriesResponse>>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCountriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<GetAllCountriesResponse>>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries
                .OrderBy(x => x.Name)
                .Select(x => new GetAllCountriesResponse
                {
                    Id = x.Id,
                    Abbreviation = x.Abbreviation,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            return await Result<IEnumerable<GetAllCountriesResponse>>.SuccessAsync(countries);
        }
    }
}