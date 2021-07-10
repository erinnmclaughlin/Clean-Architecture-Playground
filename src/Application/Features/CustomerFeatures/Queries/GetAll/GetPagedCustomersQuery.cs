using Application.Extensions;
using Application.Filters;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CustomerFeatures.Queries.GetAll
{
    public class GetPagedCustomersQuery : IRequest<PaginatedResult<GetAllCustomersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; }

        public GetPagedCustomersQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }

        internal class GetPagedCustomersQueryHandler : IRequestHandler<GetPagedCustomersQuery, PaginatedResult<GetAllCustomersResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetPagedCustomersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PaginatedResult<GetAllCustomersResponse>> Handle(GetPagedCustomersQuery request, CancellationToken cancellationToken)
            {
                var filter = new CustomerFilter(request.SearchString);

                var query = _context.Customers.Filter(filter);

                if (request.OrderBy?.Any() == true)
                {
                    var ordering = string.Join(",", request.OrderBy);
                    query = query.OrderBy(ordering);
                }

                var data = await query.Select(x => new GetAllCustomersResponse
                {
                    Country = x.Country.Abbreviation,
                    CountryId = x.CountryId,
                    Id = x.Id,
                    Location = x.Location,
                    Name = x.Name
                }).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return data;
            }
        }
    }
}