using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Features.ProductFeatures.Responses;

namespace WebAPI.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>
    {
        public int Id { get; set; }        
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

            if (product is null)
                return await Result<GetProductByIdResponse>.FailAsync("Product not found.");

            var productResponse = new GetProductByIdResponse
            {
                Barcode = product.Barcode,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Rate = product.Rate
            };

            return await Result<GetProductByIdResponse>.SuccessAsync(productResponse);
        }
    }
}