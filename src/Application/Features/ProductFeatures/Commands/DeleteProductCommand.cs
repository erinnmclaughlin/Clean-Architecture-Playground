using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (product == null)
                return await Result<int>.FailAsync("Product not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync("Product deleted.");
        }
    }
}