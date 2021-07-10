using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CustomerFeatures.Commands
{
    public class DeleteCustomerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var restrictDelete = await _context.PurchaseOrders.AnyAsync(x => x.CustomerId == command.Id, cancellationToken);

            if (restrictDelete)
                return await Result<int>.FailAsync("Deletion not allowed.");

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (customer is null)
                return await Result<int>.FailAsync("Customer not found.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(customer.Id, "Customer deleted.");
        }
    }
}