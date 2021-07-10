using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (await _context.Products.AnyAsync(x => x.Barcode == command.Barcode, cancellationToken))
            {
                return await Result<int>.FailAsync("Barcode already exists.");
            }

            var product = new Product
            {
                Barcode = command.Barcode,
                Description = command.Description,
                Name = command.Name,
                Rate = command.Rate
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return await Result<int>.SuccessAsync(product.Id, "Product saved.");
        }
    }
}
