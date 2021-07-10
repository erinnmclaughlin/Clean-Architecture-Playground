using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CustomerFeatures.Commands
{
    public class UpdateCustomerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
    }

    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (customer is null)
                return await Result<int>.FailAsync("Customer not found.");

            if (command.CountryId != 0 && !await _context.Countries.AnyAsync(x => x.Id == command.CountryId, cancellationToken))
                return await Result<int>.FailAsync("Country not found.");

            customer.CountryId = command.CountryId == 0 ? customer.CountryId : command.CountryId;
            customer.Location = command.Location;
            customer.Name = command.Name ?? customer.Name;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(customer.Id, "Customer updated.");
        }
    }
}