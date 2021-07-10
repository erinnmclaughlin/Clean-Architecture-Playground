using Application.Interfaces;
using MediatR;
using Shared.Wrapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == command.Id);

                if (product is null)
                {
                    return await Result<int>.FailAsync("Product not found!");
                }
                else
                {
                    product.Name = command.Name ?? product.Name;
                    product.Description = command.Description ?? product.Description;
                    product.Rate = command.Rate == 0 ? product.Rate : command.Rate;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync(cancellationToken);
                    return await Result<int>.SuccessAsync("Product updated.");
                }
            }
        }
    }
}
