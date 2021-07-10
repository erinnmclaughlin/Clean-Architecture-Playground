using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
    }

    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (!await _context.Countries.AnyAsync(x => x.Id == command.CountryId, cancellationToken))
            {
                return await Result<int>.FailAsync("Country not found.");
            }

            var customer = new Customer
            {
                CountryId = command.CountryId,
                Location = command.Location,
                Name = command.Name
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(customer.Id, "Customer saved.");
        }
    }
}
