using Application.Features.CustomerFeatures.Commands;
using Application.Features.CustomerFeatures.Queries.GetAll;
using Application.Features.CustomerFeatures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    public class CustomersController : ApiControllerBase
    {
        public CustomersController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Gets paginated list of customers.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString = "", string orderBy = null)
        {
            var customers = await Mediator.Send(new GetPagedCustomersQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(customers);
        }

        /// <summary>
        /// Gets the customer by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery { Id = id });
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes the customer with the given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCustomerCommand { Id = id }));
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}