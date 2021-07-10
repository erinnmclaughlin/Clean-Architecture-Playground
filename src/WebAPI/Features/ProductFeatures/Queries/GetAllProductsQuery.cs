using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Features.ProductFeatures.Responses;

namespace WebAPI.Features.ProductFeatures.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<GetAllProductsResponse>>
    {
        
    }

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetAllProductsResponse>> expression = e => new GetAllProductsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Barcode = e.Barcode,
                Rate = e.Rate
            };

            var data = await _context.Products.Select(expression).ToListAsync();
            return data;
        }
    }
}